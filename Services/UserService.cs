using license_back.APIModels;
using license_back.DB.Data;
using license_back.Services.ServiceInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace license_back.Services
{
    public class UserService: IUserService
    {
        private readonly IConfiguration _configuration;
        private LicenceContext _context;

        public UserService(IConfiguration configuration, LicenceContext licenseContext)
        {
            _context = licenseContext;
            _configuration = configuration;
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model)
        {
            var user = await _context.User.FirstOrDefaultAsync(x => x.Name == model.Username && x.Password == model.Password);
            if (user == null)
            {
                return null;
            }
            var token = generateJwtToken(user);
            return new AuthenticateResponse(new User { Username = user.Name }, token);
        }

        public async Task<AuthenticateResponse> CreateUser(CreateUserRequest model)
        {

            var user = _context.User.Add(new DB.Entity.User { Name = model.Username, Password = model.Password });
            await _context.SaveChangesAsync();
            var token = generateJwtToken(user.Entity);
            return new AuthenticateResponse(new User { Username = user.Entity.Name }, token);
        }

        public async Task<UserExistsResponse> UserExists(string name)
        {
            bool exists = _context.User.Any(e => e.Name == name);
            return new UserExistsResponse { Exist = exists };
        }

        private string generateJwtToken(DB.Entity.User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes($"{_configuration["Auth:Secret"]}");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
