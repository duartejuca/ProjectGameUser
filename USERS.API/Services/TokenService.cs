using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using USERS.API.Models;


namespace USERS.API.Services;

public class TokenService
{
    private readonly SymmetricSecurityKey key;

    public TokenService(string secretKey)
    {
        byte[] keybytes = Encoding.UTF8.GetBytes(secretKey);
        if (keybytes.Length < 8)
        {
            throw new Exception("A chave é muito curta");
        }

        key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

    }

    public Token CreateToken(string username, string role)
    {
        Claim[] claims = new[]
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, role)
        };

        JwtSecurityToken token = new JwtSecurityToken(issuer: null,
            audience: null,
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

        string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return new Token
        {
            AccessToken = tokenString,
            Expires = token.ValidTo
        };
    }
}
