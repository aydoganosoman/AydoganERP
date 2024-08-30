using AydoganERP.Base.Application.Interfaces;

namespace AydoganERP.Customer.Infrastructure.Services;

public class DateTimeService : IDateTimeService
{
    public DateTime Now => DateTime.Now;
}
