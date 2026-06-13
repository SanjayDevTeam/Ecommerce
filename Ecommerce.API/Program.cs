using Ecommerce.API.Data;
using Microsoft.EntityFrameworkCore;
using Ecommerce.API.Interfaces;
using Ecommerce.API.Repositories;
using Ecommerce.API.Services;
using Ecommerce.API.Mappings;
using FluentValidation;
using Ecommerce.API.Validators;
using Serilog;
using Ecommerce.API.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;
using Microsoft.Extensions.Logging;

Log.Logger = new LoggerConfiguration()
    .WriteTo.File(
        "logs/log.txt",
        rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<EcommerceDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAuthentication(
JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters =
        new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],

            ValidAudience = builder.Configuration["Jwt:Audience"],

            IssuerSigningKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        builder.Configuration["Jwt:Key"]!))
        };
});

//builder.Services.AddMemoryCache();
builder.Services.AddResponseCompression();
builder.Services.AddHealthChecks();

builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddAutoMapper(typeof(ProductProfile));

builder.Services.AddValidatorsFromAssemblyContaining<ProductCreateValidator>();

builder.Services.AddScoped<JwtService>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter(
        "fixed",
        limiterOptions =>
        {
            limiterOptions.PermitLimit = 100;

            limiterOptions.Window =
                TimeSpan.FromMinutes(1);

            limiterOptions.QueueLimit = 0;
        });
});

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379";
});

builder.Services
    .AddHostedService<InventoryBackgroundService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseMiddleware<RequestTimingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseSerilogRequestLogging();

app.UseRateLimiter();

app.MapControllers();

app.MapHealthChecks("/health");


app.Run();