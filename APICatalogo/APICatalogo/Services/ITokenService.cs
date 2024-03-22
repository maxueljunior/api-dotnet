using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace APICatalogo.Services;

public interface ITokenService
{
    JwtSecurityToken GerenateAccessToken(IEnumerable<Claim> claims,
                                        IConfiguration _config);
    string GenerateRefreshToken();
    ClaimsPrincipal GetPrinciaplFromExpiredToken(string token, IConfiguration _config);
}
