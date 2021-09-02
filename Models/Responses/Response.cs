public class Response<T>
{
    public T Data { get; set; }
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public Response(bool IsSuccess, string Message, T Data)
    {
        this.IsSuccess = IsSuccess;
        this.Message = Message;
        this.Data = Data;
    }
}