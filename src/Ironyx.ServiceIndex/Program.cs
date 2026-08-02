
using Ironyx.ServiceIndex;
using Ironyx.ServiceIndex.Application;
using Ironyx.ServiceIndex.Infrastructure;
using Ironyx.ServiceIndex.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSerilog((_, configuration) => configuration.ReadFrom.Configuration(builder.Configuration));

builder.Services.AddDbContext<ServiceRegistryDbContext>(contextBuilder => contextBuilder.UseNpgsql(builder.Configuration.GetConnectionString("ServiceIndex")));

builder.UseKernel()
    .AddGrpc(5900)

    .AddCommand<RegisterCommand, RegisterCommandHandler>()

    .AddQuery<GetRegistrationsQuery, IEnumerable<GetRegistrationsQuery.Result>, GetRegistrationsQueryHandler>()

    .AddCommandSender(new Uri("http://localhost:5100/"));

builder.Services.AddTransient<IServiceRegistryRepository, ServiceRegistryRepository>();

var app = builder.Build();

app.MapKernel();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();

    var db = scope.ServiceProvider
        .GetRequiredService<ServiceRegistryDbContext>();

    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();
}

app.Run();
