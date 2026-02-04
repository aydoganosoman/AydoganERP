namespace AydoganERP.Base.Domain.Modules.SharedModule.Enums;

public static class UserActionTypeEnum
{
    public const int Login = 0;
    public const int Logout = 1;
    public const int Create = 2;
    public const int Read = 3;
    public const int Update = 4;
    public const int Delete = 5;
    public const int Export = 6;
    public const int Error = 7;
    public const int Other = 8;

    public static string GetName(int value)
    {
        return value switch
        {
            UserActionTypeEnum.Login => "Login",
            UserActionTypeEnum.Logout => "Logout",
            UserActionTypeEnum.Create => "Create",
            UserActionTypeEnum.Read => "Read",
            UserActionTypeEnum.Update => "Update",
            UserActionTypeEnum.Delete => "Delete",
            UserActionTypeEnum.Export => "Export",
            UserActionTypeEnum.Error => "Error",
            UserActionTypeEnum.Other => "Other",
            _ => "No case availabe"
        };
    }
}