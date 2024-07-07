using API.OpenApi;
using Application.Handlers;
using Application.Mappings;
using Asp.Versioning;

namespace API;
public static class ModuleConfigurations
{
    public static void AddServicesConfigurations(this IServiceCollection services)
    {
        services.AddCorsConfiguration();
        services.AddMediatorConfiguration();
        services.AddAutoMapper(typeof(MappingProfiles).Assembly);
        services.AddControllers();
        services.AddApiVersioningConfiguration();
        services.AddSwaggerConfiguration();
    }

    private static void AddMediatorConfiguration(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies(typeof(GetActivitiesHandler).Assembly, typeof(GetActivityHandler).Assembly));

    }

    private static void AddCorsConfiguration(this IServiceCollection services)
    {
        services.AddCors(opt =>
        {
            opt.AddPolicy("CorsPolicy", policy =>
            {
                policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000");
            });
        });
    }

    private static void AddApiVersioningConfiguration(this IServiceCollection services)
    {

        services.AddApiVersioning(opt =>
            {
                opt.DefaultApiVersion = new ApiVersion(1);
                opt.ReportApiVersions = true;
                opt.ApiVersionReader = new UrlSegmentApiVersionReader();
            })
            .AddMvc()
            .AddApiExplorer(opt =>
            {
                opt.GroupNameFormat = "'v'V";
                opt.SubstituteApiVersionInUrl = true;
            });
    }

    private static void AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen();
        services.ConfigureOptions<ConfigureSwaggerGenOptions>();
    }
}
