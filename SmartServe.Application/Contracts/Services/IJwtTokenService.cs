using SmartServe.Application.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartServe.Application.Contracts.Services
{
    public interface IJwtTokenService
    {
        string GenerateJwtToken(UserModel user);
    }
}
