using Challenge.Application.Helpers;
using Challenge.Broker.NotificationWorker;
using Challenge.Domain.Helpers;
using Challenge.Infrastructure.Extensions;
using Challenge.Infrastructure.Helpers;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

//Custom
builder.Services.ConfigureOptions(builder.Configuration, DomainAssemblyHelper.GetDomainAssembly);
builder.Services.ConfigureOptions(builder.Configuration, InfrastructureAssemblyHelper.GetInfrastructureAssembly);
builder.Services.AddLazyLoadingSupport();
builder.Services.AddMediatR(ApplicationAssemblyHelper.GetApplicationAssembly);
builder.Services.AddDomainServices();
builder.Services.AddAdapters();
//End Custom

var host = builder.Build();
host.Run();
