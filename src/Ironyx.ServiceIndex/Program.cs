using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSerilog((_, configuration) =>
configuration.ReadFrom.Configuration(builder.Configuration));

var app = builder.Build();


app.Run();
