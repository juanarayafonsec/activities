using API.OpenApi;
using Asp.Versioning;

namespace API;
public static class ModuleConfigurations
{
    public static void AddServicesConfigurations(this IServiceCollection services)
    {
        services.AddApiVersioningConfiguration();
        services.AddSwaggerConfiguration();
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
