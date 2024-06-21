using DeerCoffeeShop.Application.Employees;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DeerCoffeeShop.API.Services
{
    public class JwtService
    {
        public class Token
        {
            public required string AccessToken { get; set; }
            public required string RefreshToken { get; set; }
            public EmployeeDto? EmployeeDto { get; set; } = null;
        }
        public Token CreateToken(string ID, string roles, string refreshToken, string? RestaurantID)
        {
            var claims = new List<Claim>
            {

                new(JwtRegisteredClaimNames.Sub, ID.ToString()),
                new(ClaimTypes.Role, roles.ToString()),
                new("RoleName",roles.ToString()),
                new("RestaurantID",RestaurantID==null?"":RestaurantID.ToString())
            };



            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Deer Coffee Shop @PI 123abc456 anh iu em"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                 issuer: "https://deercoffeesystem.azurewebsites.net/",
                 audience: "api",
                claims: claims,
                expires: DateTime.Now.AddYears(1),
                signingCredentials: creds);
            var re = new Token
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken
            };
            return re;
        }
    }
}
