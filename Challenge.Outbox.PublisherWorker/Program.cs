using Challenge.Application.Helpers;
using Challenge.Domain.Helpers;
using Challenge.Infrastructure.Extensions;
using Challenge.Outbox.PublisherWorker;
using Challenge.Outbox.PublisherWorker.Helpers;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

//Custom
builder.Services.ConfigureOptions(builder.Configuration, DomainAssemblyHelper.GetDomainAssembly);
builder.Services.ConfigureOptions(builder.Configuration, WorkerAssemblyHelper.GetWorkerAssembly);
builder.Services.AddLazyLoadingSupport();
builder.Services.AddEfStorageProvider(builder.Configuration);
builder.Services.AddDomainServices();
builder.Services.AddAdapters();
builder.Services.AddAutoMapper(ApplicationAssemblyHelper.GetApplicationAssembly);
builder.Services.AddMediatR(ApplicationAssemblyHelper.GetApplicationAssembly);
//End Custom


var host = builder.Build();
host.Run();
