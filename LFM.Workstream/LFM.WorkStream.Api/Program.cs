using Carter;
using LFM.WorkStream.Application;
using LFM.WorkStream.Core;
using LFM.WorkStream.Repository;
using NetCore.AutoRegisterDi;
using Serilog;
using Serilog.Formatting.Compact;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .WriteTo.Console(new RenderedCompactJsonFormatter())
    .CreateLogger();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterAssemblyPublicNonGenericClasses();

builder.Services.AddRepositoryModule(builder.Configuration);
builder.Services.AddApplicationModule(builder.Configuration);

builder.Services.AddCarter();
builder.Host.UseSerilog();

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILogger<Program>>();
var enableInitialization = app.Configuration.GetValue<bool>("LFM:EnableInitialization");
if (enableInitialization)
{
    var initializers = app.Services.GetServices<IInitializer>().ToList();
    foreach (var initializer in initializers)
    {
        try
        {
            await initializer.Initialize();
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Failure while running initializer");
        }
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapCarter();
app.Run();