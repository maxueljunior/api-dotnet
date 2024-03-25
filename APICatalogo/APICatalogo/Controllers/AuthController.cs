using APICatalogo.DTOs;
using APICatalogo.Models;
using APICatalogo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthController> _logger;

    public AuthController(ITokenService tokenService, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, ILogger<AuthController> logger)
    {
        _tokenService = tokenService;
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
        _logger = logger;
    }

    /// <summary>
    /// Verifica as credenciais de um usuário
    /// </summary>
    /// <param name="model">Um objeto do tipo UsuarioDTO</param>
    /// <returns>Status 200 e o token para o cliente</returns>
    /// <remarks>Retorna o Status 200 e o Token</remarks>
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var user = await _userManager.FindByNameAsync(model.Username!);

        if(user is not null && await _userManager.CheckPasswordAsync(user, model.Password!))
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim("id", user.UserName!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = _tokenService.GerenateAccessToken(authClaims, _configuration);

            var refreshToken = _tokenService.GenerateRefreshToken();

            _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInMinutes"], out int refreshTokenValidityInMinutes);

            user.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(refreshTokenValidityInMinutes);
            user.RefreshToken = refreshToken;
            await _userManager.UpdateAsync(user);

            return Ok(new
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken,
                Expiration = token.ValidTo
            });
        }

        return Unauthorized();
    }

    /// <summary>
    /// Registra um novo usuario
    /// </summary>
    /// <param name="model">Um objeto UsuarioDTO</param>
    /// <returns>Status 200</returns>
    /// <remarks>retorna o status 200</remarks>
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        var userExists = await _userManager.FindByNameAsync(model.Username!);
        
        if(userExists != null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new Response
            {
                Status = "Error",
                Message = "User already exists!"
            });
        }

        ApplicationUser user = new()
        {
            Email = model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = model.Username
        };

        var result = await _userManager.CreateAsync(user, model.Password!);

        if (!result.Succeeded) 
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new Response
            {
                Status = "Error",
                Message = "User creation failed."
            });
        }

        return Ok(new Response { Status = "Success", Message = "User created successfully!" });
    }

    [HttpPost]
    [Route("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] TokenModel model)
    {
        if(model is null)
        {
            return BadRequest("Invalid client request");
        }

        string? accessToken = model.AccessToken ?? throw new ArgumentNullException(nameof(model));
        string? refreshToken = model.RefreshToken ?? throw new ArgumentNullException(nameof(model));

        var principal = _tokenService.GetPrinciaplFromExpiredToken(accessToken!, _configuration);

        if(principal == null)
        {
            return BadRequest("Invalid access token/refresh token");
        }

        string username = principal.Identity.Name;

        var user = await _userManager.FindByNameAsync(username);

        if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
        {
            return BadRequest("Invalid access token/refresh token");
        }

        var newAccessToken = _tokenService.GerenateAccessToken(principal.Claims.ToList(), _configuration);

        var newRefreshToken = _tokenService.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        await _userManager.UpdateAsync(user);

        return new ObjectResult(new
        {
            accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
            refreshToken = newRefreshToken,
        });
    }

    [Authorize(Policy = "ExclusiveOnly")]
    [HttpPost]
    [Route("revoke/{username}")]
    public async Task<IActionResult> Revoke(string username)
    {
        var user = await _userManager.FindByNameAsync(username);

        if (user == null)
        {
            return BadRequest("Username invalid!");
        }

        user.RefreshToken = null;
        await _userManager.UpdateAsync(user);
        return NoContent();
    }

    [HttpPost]
    [Authorize(Policy = "SuperAdminOnly")]
    [Route("CreateRole")]
    public async Task<IActionResult> CreateRole(string rolename)
    {
        var roleExist = await _roleManager.RoleExistsAsync(rolename);

        if (!roleExist)
        {
            var roleResult = await _roleManager.CreateAsync(new IdentityRole(rolename));

            if(roleResult.Succeeded)
            {
                _logger.LogInformation(1, "Roles Added");
                return StatusCode(StatusCodes.Status200OK, new Response
                {
                    Status = "Success",
                    Message = $"Role {rolename} added successfully"
                });
            }
            else
            {
                _logger.LogInformation(2, "Error");
                return StatusCode(StatusCodes.Status400BadRequest, new Response
                {
                    Status = "Error",
                    Message = $"Issue adding the new {rolename} role"
                });
            }
        }

        return StatusCode(StatusCodes.Status400BadRequest, new Response
        {
            Status = "Error",
            Message = $"Role already exist."
        });
    }

    [HttpPost]
    [Authorize(Policy = "SuperAdminOnly")]
    [Route("AddUserToRole")]
    public async Task<IActionResult> AddUserToRole(string email, string rolename)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if(user != null)
        {
            var result = await _userManager.AddToRoleAsync(user, rolename);
            if (result.Succeeded)
            {
                _logger.LogInformation(1, $"User {user.Email} added to the {rolename} role");
                return StatusCode(StatusCodes.Status200OK, new Response
                {
                    Status = "Success",
                    Message = $"User {user.Email} added to the {rolename} role"
                });
            }
            else
            {
                _logger.LogInformation(1, $"Error: Unable to add user {user.Email} to the {rolename} role");
                return StatusCode(StatusCodes.Status400BadRequest, new Response
                {
                    Status = "Error",
                    Message = $"Error: Unable to add user {user.Email} to the {rolename} role"
                });
            }
        }

        return StatusCode(StatusCodes.Status400BadRequest, new Response
        {
            Status = "Error",
            Message = "Unable to find user"
        });
    }
}
