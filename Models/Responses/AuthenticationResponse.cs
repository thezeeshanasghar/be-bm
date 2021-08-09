
using dotnet.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public class AuthenticationResponse<T>
{
    public T Data { get; set; }
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public TokenResponse Token;
    public AuthenticationResponse(bool status, string message, TokenResponse Token, T data)
    {
        this.IsSuccess = status;
        this.Message = message;
        this.Token = Token;
        this.Data = data;
    }
}