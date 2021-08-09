using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace dotnet.Models
{
    public interface IJwtAuthenticationManager
    {
        TokenResponse Authenticate(string username, string password);
        IDictionary<string, string> UsersRefreshTokens { get; set; }
        TokenResponse Authenticate(string username, Claim[] claims);
    }
}
