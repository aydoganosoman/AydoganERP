namespace AydoganERP.Base.Application.Models;

public class UserAuthModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Title { get; set; }
    public string EMail { get; set; }
    public string ApiKey { get; set; }
    public string ProfilePicture { get; set; }
    public bool Verification { get; set; }
    public bool IsEnable { get; set; }
    public TokenModel Token { get; set; }
    public List<CompanyAuthModel> Companies { get; set; } = new List<CompanyAuthModel>();
}

public class CompanyAuthModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<CompanyUserAuthModel> CompanyUserAuthorizations { get; set; } = new List<CompanyUserAuthModel>();
}

public class CompanyUserAuthModel
{
    public int Module { get; set; }
    public int AuthorizationTransactionType { get; set; }
}
