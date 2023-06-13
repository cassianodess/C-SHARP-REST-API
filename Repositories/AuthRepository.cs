using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Todo.Models;
using System.Text;

namespace Todo.Repositories {

    public interface IAuthRepository {
        UserModel Auth(AuthModel auth);
        string GenerateToken(UserModel user);
    }

    public class AuthRepository : IAuthRepository {

        private readonly DataContext _context;

        public AuthRepository(DataContext _context) {
            this._context = _context;
        }
        public UserModel Auth(AuthModel auth) {
            UserModel? user = this._context.User.SingleOrDefault(_user => _user.Username == auth.Username && _user.Password == auth.Password);

            if (user == null) {
                throw new Exception($"User not found");
            }

            return user;
        }

        public string GenerateToken(UserModel user) {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            string? SECRET = Environment.GetEnvironmentVariable("HOME");
            byte[] secretBytes = Encoding.ASCII.GetBytes(SECRET);

            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]{
                    new Claim("userId", user.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(secretBytes),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            SecurityToken token = tokenHandler.CreateToken(descriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
