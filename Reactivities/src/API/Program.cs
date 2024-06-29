using API;
using Asp.Versioning;
using Asp.Versioning.Builder;
using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000");
    });
});

builder.Services.AddControllers();
builder.Services.AddServicesConfigurations();

var app = builder.Build();

app.UseCors("CorsPolicy");

//TODO Implement the following code when the first controller is added
// API Versioning 
//ApiVersionSet apiVersion = app.NewApiVersionSet()
//    .HasApiVersion(new ApiVersion(1))
//    .ReportApiVersions()
//    .Build();

//app.MapControllers().WithApiVersionSet(apiVersion);

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
    var context = services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();
    await Seed.SeedData(context);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "Something went wrong during db migration");
}

app.Run();
