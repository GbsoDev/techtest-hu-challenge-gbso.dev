using Challenge.Infrastructure.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Gbso.Finanzas.Infrastructure.Extensiones
{
	internal static class WebApiProvider
	{

		internal static IServiceCollection AddWebApiCorsPolicies(this IServiceCollection services, WebApiOptions appSettings)
		{

			//services.AddCors(options =>
			//{
			//	foreach (var corPolicy in appSettings.AllowCors)
			//	{
			//		options.AddPolicy(corPolicy.Origin, builder =>
			//		{
			//			var policy = builder.WithOrigins(corPolicy.Origin)
			//			.AllowAnyMethod()
			//			.AllowAnyHeader()
			//			.AllowCredentials();

			//			if (corPolicy.Methods?.Any() ?? false)
			//			{
			//				policy.WithMethods(corPolicy.Methods);
			//			}
			//		});
			//	}
			//});
			return services;
		}

		internal static IApplicationBuilder AddPosBuildWebApiCorsPolicies(this IApplicationBuilder app, WebApiOptions appSettings)
		{
			//app.UseCors(builder =>
			//{
			//	builder.AllowAnyOrigin()
			//	.AllowAnyMethod()
			//	.AllowAnyHeader();
			//});

			//foreach (var corPolicy in appSettings.AllowCors)
			//{
			//	app.UseCors(corPolicy.Name);
			//}
			return app;
		}
	}
}
