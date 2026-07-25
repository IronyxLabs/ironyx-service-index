
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSerilog((_, configuration) => configuration.ReadFrom.Configuration(builder.Configuration));

builder.UseKernel()
    .AddCommandSender(new Uri("http://localhost:5100/"));

var app = builder.Build();

app.MapKernel();

app.Run();
