using Application.Auth.Dto;
using Application.Auth.Services.Interfaces;
using Application.Exceptions;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Auth.Services
{
    public class JwtServices : IJwtServices
    {

        private readonly IConfiguration _configuration;

        public JwtServices(IConfiguration configuration) { 
        
            _configuration = configuration;
        }

        public string HashPassword(string userName, string hashedPassword)
        {
            PasswordHasher<string> passwordHasher = new PasswordHasher<string>();
            return passwordHasher.HashPassword(userName, hashedPassword);
        }


        public bool VerifyHashedPassword(string userName, string hashedPassword, string providedPassword)
        {
            PasswordHasher<string> passwordHasher = new PasswordHasher<string>();
            PasswordVerificationResult result = passwordHasher.VerifyHashedPassword(userName, hashedPassword, providedPassword);
            if (result == PasswordVerificationResult.Success) return true;
            return false;

        }

        public OperationResult<string> ValidToken (string token)
        {
            var result = new OperationResult<string>();

            try
            {
                if (token == null) throw new NotFoundCoreException("Debe de colocar el token para validar");

                string jwtSecretKey = _configuration.GetValue<string>("security:JwtSecretKey");

                var tokenHandler = new JwtSecurityTokenHandler();

                byte[] key = Encoding.ASCII.GetBytes(jwtSecretKey);

                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

                result.Data = token;
                result.Message = "Token Valido";
                result.Success = true;
            }
            catch
            {
                result.Data = token;
                result.Message = "Token Invalido";
                result.Success = false;
            }

            return result;
        }



        public JwtDto JwtSecurity(string JwtSecurityKey)
        {
            DateTime utcNow = DateTime.UtcNow;

            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,utcNow.ToString())
            };
            DateTime expireDateTime = utcNow.AddDays(1);

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            //key

            byte[] key = Encoding.ASCII.GetBytes(JwtSecurityKey);
            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(key);
            SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                claims: claims,
                expires: expireDateTime,
                notBefore: utcNow,
                signingCredentials: signingCredentials
            );

            string token = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);
            return new JwtDto()
            {
                Token = token,
                //AccessToken = token,
                //ExpireOn = expireDateTime
            };

        }
    }
}
