using Application.Auth.Services;
using Application.Auth.Services.Interfaces;
using Application.Context;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Infraestructure.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using System.Text;
using DinsidesBack.Filters;
using DinsidesBack.Middlewares;

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
    .WriteTo.Console(LogEventLevel.Information)
    .WriteTo.File(".." + Path.DirectorySeparatorChar + "logapi.log",
    LogEventLevel.Warning,
    rollingInterval: RollingInterval.Day
    )
    .CreateLogger();

builder.Logging.AddSerilog(logger);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ValidationFilter());
    AuthorizationPolicy authorizationPolicyBuilder = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();

    options.Filters.Add(new AuthorizeFilter());


});
//Route options
builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Hasher
builder.Services.Configure<PasswordHasherOptions>(options =>
{
    options.CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV3;
});
//ISecurityService
builder.Services.AddTransient<IJwtServices, JwtServices>();
//JWT
string jwtSecretKey = builder.Configuration.GetSection("Security:JwtSecretKey").Get<string>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
{
    byte[] key = Encoding.ASCII.GetBytes(jwtSecretKey);
    options.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateLifetime = true,
        ValidIssuer = "",
        ValidAudience = "",
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true
    };
});
//AuthorizeOperationFilter
builder.Services.AddSwaggerGen(options =>
{
    options.OperationFilter<AuthorizeOperationFilter>();
    string schemeName = "Bearer";
    options.AddSecurityDefinition(schemeName, new OpenApiSecurityScheme()
    {
        Name = schemeName,
        BearerFormat = "JWT",
        Scheme = schemeName,
        Description = "Add token.",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
    });
});
//Infrastructure
builder.Services.addInfrastructureServices(builder.Configuration);

//Applications
builder.Services.AddApplicationServices();

//Autofac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(options =>
    {
        options.RegisterModule(new InfrastructureAutofacModule());
        options.RegisterModule(new ApplicationAutofacModule());
    });

//API

builder.Services.AddTransient<ExceptionMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionMiddleware>();
app.UseCors(options =>
{
    options.AllowAnyHeader()
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .Build();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
