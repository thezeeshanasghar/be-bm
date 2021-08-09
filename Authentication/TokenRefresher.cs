using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using dotnet.Models;
using Microsoft.IdentityModel.Tokens;

namespace dotnet.Authentication
{
    public class TokenRefresher : ITokenRefresher
    {
        private readonly byte[] key;
        private readonly IJwtAuthenticationManager jWTAuthenticationManager;
        IDictionary<string, string> UsersRefreshTokens { get; set; }

        public TokenRefresher(byte[] key, IJwtAuthenticationManager jWTAuthenticationManager)
        {
            this.key = key;
            this.jWTAuthenticationManager = jWTAuthenticationManager;
        }

        public TokenResponse Refresh(RefreshCred refreshCred)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                SecurityToken validatedToken;
                var pricipal = tokenHandler.ValidateToken(refreshCred.JwtToken,
                    new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = false,
                    }, out validatedToken);
                var jwtToken = validatedToken as JwtSecurityToken;
                if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    return new TokenResponse
                    {
                        IsSuccess = false,
                        Message = "Failure: Invalid token passed. Therefore, unable to refresh token.",
                    };
                }

                var userName = pricipal.Identity.Name;
                if (refreshCred.RefreshToken != jWTAuthenticationManager.UsersRefreshTokens[userName])
                {
                    return new TokenResponse
                    {
                        IsSuccess = false,
                        Message = "Failure: Invalid token passed. Therefore, unable to refresh token.",
                    };
                }

                return jWTAuthenticationManager.Authenticate(userName, pricipal.Claims.ToArray());
            }
            catch (Exception exception)
            {
                return new TokenResponse
                {
                    IsSuccess = false,
                    Message = $"Server Failure: {exception.Message}",
                };
            }
        }

    }
}
