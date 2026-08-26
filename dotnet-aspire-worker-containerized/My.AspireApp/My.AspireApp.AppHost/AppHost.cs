var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.My_AspireApp_ContainerImage>("my-aspire-worker");

builder.Build().Run();
