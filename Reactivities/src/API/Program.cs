using API;
using API.Middleware;
using Asp.Versioning;
using Asp.Versioning.Builder;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Context;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddServicesConfigurations(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.UseCors("CorsPolicy");

// API Versioning 
ApiVersionSet apiVersion = app.NewApiVersionSet()
    .HasApiVersion(new ApiVersion(1))
    .ReportApiVersions()
    .Build();

app.MapControllers().WithApiVersionSet(apiVersion);

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

//Swagger Setup
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opt =>
    {
        var descriptions = app.DescribeApiVersions();
        foreach (var description in descriptions)
        {
            string url = $"/swagger/{description.GroupName}/swagger.json";
            string name = description.GroupName.ToUpperInvariant();

            opt.SwaggerEndpoint(url, name);
        }
    });
}


// Initial Seed
await using var scope = app.Services.CreateAsyncScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<CoreDbContext>();
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    await context.Database.MigrateAsync();
    await Seed.SeedData(context, userManager);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "Something went wrong during db migration");
}

app.Run();
