using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using w4sd.Models;

namespace w4sd.Security
{
    public interface IUserRepositoryService
    {
        UserDto? GetUser(UserModel userModel);
    }
    public class UserRepositoryService : IUserRepositoryService
    {
        private static List<UserDto> Users => new() //intellisense wants it static
        {
            new("Anu Viswan", "anu"),
            new("Jia Anu", "jia"),
            new("Naina Anu", "naina"),
            new("Sreena Anu", "sreena"),
            new("test", "test"),
            new("string", "string"),
        };
        public UserDto? GetUser(UserModel userModel)
        {
            return Users.FirstOrDefault(x => string.Equals(x.UserName, userModel.UserName) && string.Equals(x.Password, userModel.Password));
        }
    }

    public interface ITokenService
    {
        string BuildToken(string key, string issuer, UserDto user);
    }
    public class TokenService : ITokenService
    {
        private readonly TimeSpan ExpiryDuration = new(0, 30, 0);
        public string BuildToken(string key, string issuer, UserDto user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier,
                Guid.NewGuid().ToString())
             };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(issuer, issuer, claims,
                expires: DateTime.Now.Add(ExpiryDuration), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}
