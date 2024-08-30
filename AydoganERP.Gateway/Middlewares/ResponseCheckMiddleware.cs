using Ocelot.Logging;
using Ocelot.Middleware;
using Ocelot.Requester;

namespace AydoganERP.Gateway.Middlewares;

public class ResponseCheckMiddleware : OcelotMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IHttpRequester _requester;
    public ResponseCheckMiddleware(RequestDelegate next,
        IOcelotLoggerFactory loggerFactory,
        IHttpRequester requester)
        : base(loggerFactory.CreateLogger<ResponseCheckMiddleware>())
    {
        _next = next;
        _requester = requester;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);

        var response = context.Response;

        // Yanıt gövdesini kontrol etme
        if (response.StatusCode == 200 && response.ContentType == "application/json")
        {
            // Yanıt gövdesini kontrol edip bir hata durumu varsa uygun bir hata kodu döndürebilirsiniz.
            using var reader = new StreamReader(response.Body);
            var body = await reader.ReadToEndAsync();
            if (body.Contains("error")) // Burada hata mesajını kontrol edin
            {
                response.StatusCode = 500;
                await context.Response.WriteAsync("Error occurred.");
            }
        }
    }
}
