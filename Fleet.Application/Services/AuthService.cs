using Fleet.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Fleet.Application.Services;

public interface IAuthService
{
    void CreatePasswordHash(string password, out byte[] hash, out byte[] salt);
    bool VerifyPasswordHash(string password, byte[] hash, byte[] salt);
    string CreateToken(AppUser user);
}

public class AuthSettings
{
    public string Key { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
}

public class AuthService : IAuthService
{
    private readonly AuthSettings _authSettings;

    public AuthService(IOptions<AuthSettings> authSettings)
    {
        _authSettings = authSettings.Value;
    }

    public void CreatePasswordHash(string password, out byte[] hash, out byte[] salt)
    {
        using var hmac = new HMACSHA512();
        salt = hmac.Key;
        hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }

    public bool VerifyPasswordHash(string password, byte[] hash, byte[] salt)
    {
        using var hmac = new HMACSHA512(salt);
        var computed = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return computed.SequenceEqual(hash);
    }

    public string CreateToken(AppUser user)
    {
        var claims = new[]
            {
                new Claim(ClaimTypes.Name,  user.Username),
                new Claim(ClaimTypes.Role,  user.Role!.Value.ToString())
            };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_authSettings.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _authSettings.Issuer,
            audience: _authSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
