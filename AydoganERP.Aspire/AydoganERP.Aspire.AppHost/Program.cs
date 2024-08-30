var builder = DistributedApplication.CreateBuilder(args);


builder.AddProject<Projects.AydoganERP_Gateway>("gateway");


builder.AddProject<Projects.AydoganERP_User_Api>("aydoganerp-user-api");


builder.AddProject<Projects.AydoganERP_Customer_Api>("aydoganerp-customer-api");


builder.Build().Run();
