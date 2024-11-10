using System.Text;
using API.OpenApi;
using API.Services;
using Application.Handlers;
using Application.Interfaces;
using Application.Validators;
using Asp.Versioning;
using Domain.Entities;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Photos;
using Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Persistence.Context;

namespace API;

public static class ModuleConfigurations
{
    public static void AddServicesConfigurations(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContext<CoreDbContext>(opt => { opt.UseSqlite(configuration.GetConnectionString("Default")); });
        services.AddIdentity();
        services.AddAccessor();
        services.AddConfigure(configuration);
        services.AddPhotoService();
        services.AddCorsConfiguration();
        services.AddMediatorConfiguration();
        services.AddFluentValidatorConfiguration();
        services.AddControllers(opt =>
        {
            var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            opt.Filters.Add(new AuthorizeFilter(policy));
        });
        services.AddApiVersioningConfiguration();
        services.AddSwaggerConfiguration();
    }

    private static void AddMediatorConfiguration(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies(typeof(GetActivitiesHandler).Assembly,
                typeof(GetActivityHandler).Assembly));
    }

    private static void AddCorsConfiguration(this IServiceCollection services)
    {
        services.AddCors(opt =>
        {
            opt.AddPolicy("CorsPolicy",
                policy => { policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000"); });
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

    private static void AddFluentValidatorConfiguration(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<ActivityDtoValidator>();
    }

    private static void AddIdentity(this IServiceCollection services)
    {
        services.AddIdentityCore<AppUser>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
                opt.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<CoreDbContext>();

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes("gtnwUbrmmRupujwFGNRhetAVstA6uy2QRvJy78kch65a37E9TJRfJqmwEKctixG8CmJKNjV6igyZ"));
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
        {
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });
        services.AddAuthorization(opt =>
        {
            opt.AddPolicy("IsActivityHost", policy => policy.Requirements.Add(new IsHostRequirement()));
        });
        services.AddTransient<IAuthorizationHandler, IsHostRequirementHandler>();
        services.AddScoped<TokenService>();
    }

    private static void AddAccessor(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<IUserAccessor, UserAccessor>();
    }

    private static void AddConfigure(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.Configure<CloudinarySettings>(configuration.GetSection(nameof(CloudinarySettings)));
    } 
    private static void AddPhotoService(this IServiceCollection services)
    {
        services.AddScoped<IPhotoAccessor, PhotoAccessor>();
    }
}