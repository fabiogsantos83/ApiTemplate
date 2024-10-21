using ApiTemplate.Api.Handlers;
using ApiTemplate.Application.Models;
using ApiTemplate.Infrastructure.AutoMapper;
using ApiTemplate.Infrastructure.BuilderServices;
using ApiTemplate.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.AddSerilog(builder.Configuration, "API Template");
// Add services to the container.

var jwtOptions = builder.Configuration
    .GetSection("JwtOptions")
    .Get<JwtOptions>();

builder.Services.AddControllers()
    .AddJsonOptions(opt => {
        opt.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.SnakeCaseLower;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => Swagger.AddSwaggerGen(c));
builder.Services.RegisterServices(jwtOptions);
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(opts =>
{
    //convert the string signing key to byte array
    byte[] signingKeyBytes = Encoding.UTF8
        .GetBytes(jwtOptions.SigningKey);

    opts.RequireHttpsMetadata = false;
    opts.SaveToken = true;

    opts.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = jwtOptions.Issuer.IsNullOrEmpty() ? false : true,
        ValidateAudience = jwtOptions.Audience.IsNullOrEmpty() ? false : true,
        ValidIssuer = jwtOptions.Issuer,
        ValidAudience = jwtOptions.Audience,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(signingKeyBytes)
    };
});

// 👇 Configuring the Authorization Service
builder.Services.AddAuthorization();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseExceptionHandler(exceptionHandlerApp =>
{
    exceptionHandlerApp.Run(async context => await ExceptionHandler.HandleError(context));
});

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseSerilog();

app.Run();
