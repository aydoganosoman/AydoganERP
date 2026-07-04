namespace AydoganERP.EInvoice.MySoft.Models;

/// <summary>
/// HTTP istek sonuç modeli
/// </summary>
/// <typeparam name="T">Veri tipi</typeparam>
public class HttpClientResult<T>
{
    public T? Data { get; set; }
    public bool Succeed { get; set; }
    public string Message { get; set; }
    public string ErrorCode { get; set; }
    public int AfterValue { get; set; }

    public static HttpClientResult<T> Success(T data, string? message = null)
    {
        return new HttpClientResult<T>
        {
            Succeed = true,
            Data = data,
            Message = message
        };
    }

    public static HttpClientResult<T> Error(string message, string? errorCode = null)
    {
        return new HttpClientResult<T>
        {
            Succeed = false,
            Message = message,
            ErrorCode = errorCode
        };
    }
}
