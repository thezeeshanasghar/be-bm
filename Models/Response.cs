
using dotnet.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Response<T>
{

    public T Data { get; set; }
    public bool IsSuccess {get;set;}
    public string Message{get;set;}

    public Response(bool status, string message, T data) {
        IsSuccess = status;
        Message = message;
        Data =  data;
    }
}