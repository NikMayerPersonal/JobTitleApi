using Application.Jobs.Query.GetJobById;
using Common.General;
using FluentValidation;
using FluentValidation.AspNetCore;
using Jobs.Api.Controllers.v1.Jobs.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;

namespace Jobs.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebApi(this IServiceCollection services, IConfiguration configuration)
        {
            var appOptions = configuration.GetSection(nameof(AppOptions)).Get<AppOptions>();
            services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
            });
            services.AddSwaggerGen(options =>
            {
                options.EnableAnnotations();

                options.DocInclusionPredicate((docName, apiDesc) =>
                {
                    if (!apiDesc.TryGetMethodInfo(out MethodInfo methodInfo)) return false;

                    var versions = methodInfo.DeclaringType
                        .GetCustomAttributes<ApiVersionAttribute>(true)
                        .SelectMany(attr => attr.Versions);

                    return versions.Any(v => $"v{v}" == docName);
                });
            });
            services.AddHttpContextAccessor();
            services.AddCors();
            services.AddFluentValidationAutoValidation();
            services.AddTransient<IValidator<GetJobByIdQuery>, GetJobByIdRequestValidator>();
            return services;
        }

        public static IApplicationBuilder UseWebApi(this IApplicationBuilder app, IConfiguration configuration,
            IWebHostEnvironment env)
        {
            app.UseCors(builder =>
            {
                builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Job API v1");

                options.DocExpansion(DocExpansion.None);
            });
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                if (env.IsDevelopment() || env.IsStaging())
                {
                    endpoints.MapControllers().AllowAnonymous();
                }
                else
                {
                    endpoints.MapControllers();
                }
            });

            return app;
        }
    }
}
