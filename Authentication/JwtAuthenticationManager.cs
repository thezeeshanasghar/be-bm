using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using dotnet.Models;
using Microsoft.IdentityModel.Tokens;

namespace dotnet.Authentication
{
    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {
        private readonly string tokenKey;
        private readonly IRefreshTokenGenerator refreshTokenGenerator;
        public IDictionary<string, string> UsersRefreshTokens { get; set; }

        public JwtAuthenticationManager(string tokenKey, IRefreshTokenGenerator refreshTokenGenerator)
        {
            this.tokenKey = tokenKey;
            this.refreshTokenGenerator = refreshTokenGenerator;
            UsersRefreshTokens = new Dictionary<string, string>();
        }

        public TokenResponse Authenticate(string username, string password)
        {
            try
            {
                DateTime tokenCreatedDate = DateTime.UtcNow;

                var token = GenerateTokenString(username, tokenCreatedDate);
                var refreshToken = refreshTokenGenerator.GenerateToken();

                if (token == null || refreshToken == null)
                {
                    return new TokenResponse
                    {
                        IsSuccess = false,
                        Message = "Failure: Unable to generate token.",
                    };
                }

                if (UsersRefreshTokens.ContainsKey(username))
                {
                    UsersRefreshTokens[username] = refreshToken;
                }
                else
                {
                    UsersRefreshTokens.Add(username, refreshToken);
                }

                return new TokenResponse
                {
                    JwtToken = token,
                    RefreshToken = refreshToken,
                    CreatedDate = tokenCreatedDate,
                    ExpiryDate = tokenCreatedDate.AddMinutes(2),
                    IsSuccess = true,
                    Message = "Success: Token generated.",
                };
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

        public TokenResponse Authenticate(string username, Claim[] claims)
        {
            try
            {
                DateTime tokenCreatedDate = DateTime.UtcNow;

                var token = GenerateTokenString(username, tokenCreatedDate, claims);
                var refreshToken = refreshTokenGenerator.GenerateToken();

                if (token == null || refreshToken == null)
                {
                    return new TokenResponse
                    {
                        IsSuccess = false,
                        Message = "Failure: Unable to generate token.",
                    };
                }

                if (UsersRefreshTokens.ContainsKey(username))
                {
                    UsersRefreshTokens[username] = refreshToken;
                }
                else
                {
                    UsersRefreshTokens.Add(username, refreshToken);
                }

                return new TokenResponse
                {
                    JwtToken = token,
                    RefreshToken = refreshToken,
                    CreatedDate = tokenCreatedDate,
                    ExpiryDate = tokenCreatedDate.AddMinutes(2),
                    IsSuccess = true,
                    Message = "Success: Token generated.",
                };
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

        string GenerateTokenString(string username, DateTime expires, Claim[] claims = null)
        {
            Console.WriteLine(expires);
            DateTime samp = expires;
            Console.WriteLine(samp.AddMinutes(2));

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(tokenKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                 claims ?? new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = expires.AddMinutes(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature),
            };

            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }
    }
}
