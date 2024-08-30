using AydoganERP.Customer.Domain.Entities;

namespace AydoganERP.Customer.Application.Common.Models;
public class CustomerSegmentationDto
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public double DiscountRate { get; private set; }
    public int RiskLevel { get; private set; }

    public static explicit operator CustomerSegmentationDto(CustomerSegmentation customerSegmentation)
    {
        return new CustomerSegmentationDto()
        {
            Id = customerSegmentation.Id,
            Name = customerSegmentation.Name,
            Description = customerSegmentation.Description,
            DiscountRate = customerSegmentation.DiscountRate,
            RiskLevel = (int)customerSegmentation.RiskLevel
        };
    }
}
