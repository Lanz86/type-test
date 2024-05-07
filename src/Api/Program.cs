using System.Reflection;
using Api.Entities.Common;
using Api.Infrastructure;
using Api.Infrastructure.Data;
using Api.Infrastructure.Data.Interceptors;
using Api.Services;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => builder.Configuration.Bind(nameof(JwtBearerOptions), options));

builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthorization();

builder.Services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();

builder.Services.AddDbContext<ApplicationDbContext>((sp, opt) =>
{
    opt.UseInMemoryDatabase("type-tests");
    opt.AddInterceptors(sp.GetRequiredService<ISaveChangesInterceptor>());
});
builder.Services.AddTransient<IUser, User>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddFastEndpoints().SwaggerDocument();



var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.MapGet("/", () => "Hello World!");

app.UseFastEndpoints()
    .UseSwaggerGen();

app.Run();
