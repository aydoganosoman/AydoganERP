using AydoganERP.Customer.Application.Common.Models;
using HotChocolate.Data.Filters;

namespace AydoganERP.Customer.Api.GraphQL.Filters;

public class CustomerFilterType : FilterInputType<CustomerDto>
{
    protected override void Configure(IFilterInputTypeDescriptor<CustomerDto> descriptor)
    {
        descriptor.BindFieldsExplicitly();
        descriptor.Field(f => f.Company.Id).Name("companyId");

        base.Configure(descriptor);
    }
}
