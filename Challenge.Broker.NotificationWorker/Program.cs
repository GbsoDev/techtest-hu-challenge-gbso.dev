using Challenge.Broker.NotificationWorker;
using Challenge.Broker.NotificationWorker.Helpers;
using Challenge.Infrastructure.Extensions;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

//Custom
builder.Services.ConfigureOptions(builder.Configuration, WorkerAssemblyHelper.GetWorkerAssembly);
builder.Services.AddLazyLoadingSupport();
builder.Services.AddAdapters();
//End Custom

var host = builder.Build();
host.Run();
