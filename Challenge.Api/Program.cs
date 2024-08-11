using Challenge.Application.Helpers;
using Challenge.Domain.Helpers;
using Challenge.EfStorage;
using Challenge.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Custom
builder.Services.ConfigureOptions(builder.Configuration, DomainAssemblyHelper.GetDomainAssembly);
builder.Services.ConfigureOptions(builder.Configuration, ApplicationAssemblyHelper.GetApplicationAssembly);
builder.Services.AddLazyLoadingSupport();
builder.Services.AddEfStorageProvider(builder.Configuration);
builder.Services.AddDomainServices();
builder.Services.AddAdapters();
builder.Services.AddAutoMapper(ApplicationAssemblyHelper.GetApplicationAssembly);
builder.Services.AddMediatR(ApplicationAssemblyHelper.GetApplicationAssembly);
//End Custom

var app = builder.Build();

//Custom
app.Services.MigrateDataBase();
//En Custom

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
