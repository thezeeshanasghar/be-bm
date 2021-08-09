using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dotnet.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly Context _db;
        private readonly IJwtAuthenticationManager jwtAuthenticationManager;
        private readonly ITokenRefresher tokenRefresher;

        public AuthenticationController(Context context, IJwtAuthenticationManager jwtAuthenticationManager, ITokenRefresher tokenRefresher)
        {
            _db = context;
            this.jwtAuthenticationManager = jwtAuthenticationManager;
            this.tokenRefresher = tokenRefresher;
        }

        [HttpPost("login")]
        public async Task<AuthenticationResponse<Login>> Login(LoginRequest loginRequest)
        {
            try
            {
                User usersObject = await _db.Users.FirstOrDefaultAsync(x => x.Email == loginRequest.UserName || x.Contact == loginRequest.UserName);
                if (usersObject == null)
                {
                    return new AuthenticationResponse<Login>(false, "Failed: Either email or phone is incorrect.", null, null);
                }
                Login loginObject = await _db.Login.FirstOrDefaultAsync(x => x.UserId == usersObject.Id);
                if (loginObject == null)
                {
                    return new AuthenticationResponse<Login>(false, "Failure: Login data doesn't exist in database.", null, null);
                }
                if (loginObject.Password == loginRequest.Password)
                {
                    TokenResponse token = jwtAuthenticationManager.Authenticate(loginRequest.UserName, loginRequest.Password);
                    if (!token.IsSuccess)
                    {
                        return new AuthenticationResponse<Login>(false, $"{token.Message}", token, null);
                    }
                    return new AuthenticationResponse<Login>(true, $"Success: Login credentials are correct.", token, loginObject);
                }
                return new AuthenticationResponse<Login>(false, "Failure: Entered password is incorrect.", null, null);
            }
            catch (Exception exception)
            {
                return new AuthenticationResponse<Login>(false, "Server Failure: Unable to login. Because " + exception.Message, null, null);
            }
        }

        [HttpPost("refresh")]
        public AuthenticationResponse<Login> Refresh([FromBody] RefreshCred refreshCred)
        {
            var token = tokenRefresher.Refresh(refreshCred);
            if (!token.IsSuccess)
            {
                return new AuthenticationResponse<Login>(false, $"{token.Message}", token, null);
            }
            return new AuthenticationResponse<Login>(true, $"{token.Message}", token, null);
        }
    }
}
