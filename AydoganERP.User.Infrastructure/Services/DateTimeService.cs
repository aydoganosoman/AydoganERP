using AydoganERP.Base.Application.Interfaces;

namespace AydoganERP.User.Infrastructure.Services;

public class DateTimeService : IDateTimeService
{
    public DateTime Now => DateTime.Now;
}
