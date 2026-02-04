using AydoganERP.Base.Domain.Common;

namespace AydoganERP.Base.Domain.Modules.SharedModule.Entities;

public class UserActionLog : Entity
{
    // For EF
    public UserActionLog() { }

    private UserActionLog(string ip,
        string userEmail,
        string method,
        string requestPath,
        string request,
        string response,
        string requestBody,
        string responseBody,
        int actionType,
        string? description)
    {
        this.Id = Guid.NewGuid();
        this.Ip = ip;
        this.Method = method;
        this.UserEmail = userEmail;
        this.RequestPath = requestPath;
        this.Request = request;
        this.Response = response;
        this.RequestBody = requestBody;
        this.ResponseBody = responseBody;
        this.Timestamp = DateTime.UtcNow;
        this.ActionType = actionType;
        this.Description = description;
    }
    
    public Guid Id { get; set; }
    public string Ip { get; set; }
    public string UserEmail { get; set; }
    public string Method { get; set; }
    public string RequestPath { get; set; }
    public string Request { get; set; }
    public string Response { get; set; }
    public string RequestBody { get; set; }
    public string ResponseBody { get; set; }
    public DateTime Timestamp { get; set; }
    public int ActionType { get; set; }
    public string? Description { get; set; } 

    public static UserActionLog Create(string ip,
        string userEmail,
        string method,
        string requestPath,
        string request,
        string response,
        string requestBody,
        string responseBody,
        int actionType,
        string? description)
    {
        return new UserActionLog(ip, 
            userEmail,
            method,
            requestPath, 
            request, 
            response, 
            requestBody, 
            responseBody, 
            actionType, 
            description);
    }
}