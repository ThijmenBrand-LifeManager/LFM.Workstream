using FluentValidation;
using LFM.Authorization.AspNetCore;
using LFM.WorkStream.Api.Extensions;
using LFM.WorkStream.Application;
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
    builder.Services.AddSwagger();
}

builder.Services.AddLfmAuthorization(builder.Configuration);

builder.Services.AddHttpContextAccessor();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .WriteTo.Console(new RenderedCompactJsonFormatter())
    .CreateLogger();

builder.Services.AddRepositoryModule(builder.Configuration);
builder.Services.AddApplicationModule(builder.Configuration);

builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (enableSwagger)
{
    app.UseSwagger().UseAuthentication();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();