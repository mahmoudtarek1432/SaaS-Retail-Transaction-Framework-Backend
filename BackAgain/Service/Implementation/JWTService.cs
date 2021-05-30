using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BackAgain.Service
{
    public class JWTService
    {
        private readonly IConfiguration _Configuration;

        public JWTService()
        {
        }

        public static string MakeJwtToken(IConfiguration config, Claim[] claims, int expireyDays)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["AuthSettings:Key"]));

            var Token = new JwtSecurityToken(
                issuer: config["AuthSettings:Issuer"],
                audience: config["AuthSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(expireyDays),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );

            var tokenAsString = new JwtSecurityTokenHandler().WriteToken(Token);

            return tokenAsString;
        }
    }
}
