using ApiTemplate.Api.Handlers;
using ApiTemplate.Infrastructure.AutoMapper;
using ApiTemplate.Infrastructure.Extensions;
using ApiTemplate.Infrastructure.Ioc;
using Elastic.CommonSchema;

var builder = WebApplication.CreateBuilder(args);
builder.AddSerilog(builder.Configuration, "API Template");
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterServices();
builder.Services.AddAutoMapper(typeof(MappingProfile));


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseExceptionHandler(exceptionHandlerApp =>
{
    exceptionHandlerApp.Run(async context => await ExceptionHandler.HandleError(context));
});

app.UseAuthorization();

app.MapControllers();

app.UseSerilog();

app.Run();
