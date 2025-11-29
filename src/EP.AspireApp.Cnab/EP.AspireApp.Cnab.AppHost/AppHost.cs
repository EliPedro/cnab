var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.EP_AspireApp_Cnab_Web>("apiservice")
    .WithHttpHealthCheck("/health");

builder.AddProject<Projects.EP_AspireApp_Cnab_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/health")
    .WithReference(apiService)
    .WaitFor(apiService);


builder.AddProject<Projects.EP_AspireApp_Cnab_ApiService>("ep-aspireapp-cnab-apiservice");


builder.Build().Run();
