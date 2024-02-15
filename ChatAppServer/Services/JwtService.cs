using ChatAppServer.Models;
using ChatAppServer.Services.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ChatAppServer.Services
{
    public class JwtService : IJwtService
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IConfiguration configuration;

        public JwtService(UserManager<AppUser> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }


        public async Task<string> CreateJwt(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim("email", user.Email!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            var roles = await userManager.GetRolesAsync(user);
            var releClaims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();
            claims.AddRange(releClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtConfig:SecretKey"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            DateTime expires = DateTime.Now.AddDays(1);

            var token = new JwtSecurityToken(issuer: configuration["JwtConfig:ValidIssuer"],
                audience: configuration["JwtConfig:ValidAudience"],
                expires: expires,
                claims: claims,
                signingCredentials: creds);
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return jwtToken;
        }
    }
}
