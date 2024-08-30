namespace AydoganERP.Base.Domain.Common;

public interface IGeneratePasswordUtil
{
    string CreateRandomPassword(int passwordLength);
}
