using BlogPlatform.Domain.ApplicationUserAggregate;
using BlogPlatform.Infrastructure;
using BlogPlatform.Infrastructure.DI;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

AddAppServices(builder.Services, builder.Configuration);
ConfigureAutoMapper(builder.Services);
ConfigureSwagger(builder.Services);

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<BlogPlatformContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
await app.SeedRolesAsync();

app.Run();


static void AddAppServices(IServiceCollection services, IConfiguration configuration)
{
    services.AddInfrastructureServices(configuration);
}

static void ConfigureAutoMapper(IServiceCollection services)
{
    services.AddAutoMapper(typeof(Program));
}

static void ConfigureSwagger(IServiceCollection services)
{
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
}



