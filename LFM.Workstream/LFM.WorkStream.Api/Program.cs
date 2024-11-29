using FluentValidation;
using LFM.Authorization.AspNetCore;
using LFM.Authorization.AspNetCore.Models;
using LFM.WorkStream.Api.Authorization;
using LFM.WorkStream.Api.Extensions;
using LFM.WorkStream.Application;
using LFM.WorkStream.Core;
using LFM.WorkStream.Core.Messages;
using LFM.WorkStream.Repository;
using Serilog;
using Serilog.Formatting.Compact;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var enableSwagger = builder.Configuration.GetValue<bool>("OpenApi:ShowDocument");
if (enableSwagger)
{
    builder.Services.AddSwagger(builder.Configuration);
}

const string CorsDevelopmentPolicy = "local_development";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: CorsDevelopmentPolicy, policy =>
    {
        policy.WithOrigins("*")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddLfmAuthorization(builder.Configuration).InsertPermissionsOnRole(x =>
{
    x.Role = DefaultRoles.ProjectAdmin;
    x.Permissions =
    [
        new PermissionDto { Name = Permissions.WorkstreamConfigurer, Category = "Workstream" },
        new PermissionDto { Name = Permissions.WorkstreamReader, Category = "Workstream" }
    ];
});

builder.Services.RegisterMasstransit(builder.Configuration, enableQueueListener: false);

builder.Services.AddHttpContextAccessor();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .WriteTo.Console(new RenderedCompactJsonFormatter())
    .CreateLogger();

builder.Services.AddRepositoryModule(builder.Configuration);
builder.Services.AddApplicationModule(builder.Configuration);
builder.Services.AddCoreModule(builder.Configuration);

builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (enableSwagger)
{
    app.UseSwagger().UseAuthentication();
    app.UseSwaggerUI();
    
    app.ApplyMigrations();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors(CorsDevelopmentPolicy);


app.MapControllers();
app.Run();