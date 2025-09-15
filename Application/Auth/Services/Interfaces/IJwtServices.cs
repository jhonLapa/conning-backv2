using Application.Auth.Dto;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Auth.Services.Interfaces
{
    public interface IJwtServices
    {
        string HashPassword(string userName, string hashedPassword);

        bool VerifyHashedPassword(string userName, string hashedPassword, string providerPassword);

        JwtDto JwtSecurity(string JwtSecurityKey);

        OperationResult<string> ValidToken(string token);

    }
}
