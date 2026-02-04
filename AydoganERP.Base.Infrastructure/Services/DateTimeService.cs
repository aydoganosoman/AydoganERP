using AydoganERP.Base.Application.Common.Interfaces;

namespace AydoganERP.Base.Infrastructure.Services;

public class DateTimeService : IDateTimeService
{
    public DateTime Now => DateTime.Now;
}
