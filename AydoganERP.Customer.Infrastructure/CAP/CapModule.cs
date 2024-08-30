using AydoganERP.Base.Application.Interfaces;
using AydoganERP.Customer.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AydoganERP.Customer.Infrastructure.CAP;

public static class CapModule
{
    public static IServiceCollection LoadCAP(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IProducerService, ProducerService>();

        services.AddCap(options =>
        {
            options.UseDashboard(o => o.PathMatch = "/cap-dashboard");
            //options.DefaultGroupName = "Customer";
            options.UseEntityFramework<ApplicationDbContext>();
            //options.UsePostgreSql(configuration.GetConnectionString("DefaultConnection"));
            
            options.UseRabbitMQ(conf =>
            {
                //conf.ExchangeName = "CustomerEventBus";
                //conf.VirtualHost = "AydoganCRM-Customer";
                conf.HostName = configuration.GetSection("RabbitMq:Host").Value;
                conf.Port = Convert.ToInt32(configuration.GetSection("RabbitMq:Port").Value);
                conf.UserName = configuration.GetSection("RabbitMq:UserName").Value;
                conf.Password = configuration.GetSection("RabbitMq:Password").Value;
                //conf.ConnectionFactoryOptions = x =>
                //{
                //    x.Ssl.Enabled = false;
                //    x.HostName = configuration.GetSection("RabbitMq:Host").Value;
                //    x.UserName = configuration.GetSection("RabbitMq:UserName").Value;
                //    x.Password = configuration.GetSection("RabbitMq:Password").Value;
                //    x.Port = Convert.ToInt32(configuration.GetSection("RabbitMq:Port").Value);
                //};
            });
        });

        return services;
    }
}
