﻿namespace CategoriasMvc.Models;

public class TokenViewModel
{
    public string? Token { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime Expiration {  get; set; }
}
