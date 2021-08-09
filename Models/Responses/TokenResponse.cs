using System;

namespace dotnet.Models
{
    public class TokenResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
