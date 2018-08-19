using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SoftUniClone.Models;
using SoftUniClone.ServiceModels.ApiAuthentication;

namespace SoftUniClone.Web.Controllers
{
    [Route("api/Token")]
    [ApiController]
    [IgnoreAntiforgeryToken]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> userManager;

        public AuthController(UserManager<User> userManeger)
        {
            this.userManager = userManeger;
        }

        [AllowAnonymous] // because home page can be reach from everybody!
        [HttpPost("")]
        public async Task<IActionResult> GenerateToken(
            [FromBody]UserBindingModel model)
        {
            var user = await this.userManager.FindByNameAsync(model.Username);
            bool isPasswordValid = await userManager.CheckPasswordAsync(user, model.Password);

            if (!isPasswordValid)
            {
                return Unauthorized();
            }

            var roles = await this.userManager.GetRolesAsync(user);
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("supersecretsupersecretsupersecretsupersecret")); // keep this key NOT HERE!
            var token = new JwtSecurityToken(
                claims: new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, string.Join(", ", roles))
                },
                issuer: "localhost",
                audience: "localhost",
                expires: DateTime.Now + TimeSpan.FromHours(24),
                signingCredentials: new SigningCredentials(key, "HS256"));
            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(new { Token = tokenString });

            // Виж кода на Данчо за this.configuration to replace this: Encoding.UTF8.GetBytes("supersecretsupersecretsupersecretsupersecret")
        }
    }
}