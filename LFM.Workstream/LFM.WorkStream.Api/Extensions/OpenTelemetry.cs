using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using OpenTelemetry;
using OpenTelemetry.Exporter;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;

namespace LFM.Authorization.Core.Extensions;

public static class OpenTelemetry
{
    public static void RegisterOpenTelementry(this IServiceCollection services, IConfiguration configuration,
        string applicationName)
    {
        var tracingOtpEndpoint = configuration.GetValue<string>("Logging:OpenTelemetry:Endpoint");
        var otel = services.AddOpenTelemetry();

        otel.ConfigureResource(resource => resource.AddService(serviceName: applicationName));
        otel.WithMetrics(metrics =>
        {
            metrics
                .AddAspNetCoreInstrumentation()
                .AddMeter("Microsoft.AspNetCore.Hosting")
                .AddMeter("Microsoft.AspNetCore.Server.Kestrel")
                .AddPrometheusExporter();

            if (tracingOtpEndpoint != null)
            {
                Console.WriteLine("Adding OTLP Exporter");
                metrics.AddOtlpExporter(oltpOptions =>
                {
                    oltpOptions.Endpoint = new Uri(tracingOtpEndpoint);
                    oltpOptions.ExportProcessorType = ExportProcessorType.Batch;
                    oltpOptions.Protocol = OtlpExportProtocol.Grpc;
                });
            }
        });

    otel.WithTracing(tracing =>
        {
            tracing.AddAspNetCoreInstrumentation(options =>
            {
                options.Filter = httpContext => !httpContext.Request.Path.Equals("/metrics") &&
                                                !httpContext.Request.Path.Equals("/health");

                options.EnrichWithHttpRequest = (activity, request) =>
                {
                    activity.SetTag("requestProtocol", request.Protocol);
                };
                
                options.RecordException = true;
                options.EnrichWithException = ((activity, exception) =>
                {   
                    activity.SetTag("exceptionType", exception.GetType().Name);
                    activity.SetTag("stackTrace", exception.StackTrace);
                });
            });
            tracing.AddHttpClientInstrumentation(options =>
            {
                options.FilterHttpRequestMessage = (request) => !request.RequestUri!.Host.Equals("lfm_loki");
            });
            tracing.AddEntityFrameworkCoreInstrumentation(options =>
            {
                options.SetDbStatementForText = true;
                options.SetDbStatementForStoredProcedure = true;
            });
            tracing.AddSource("MassTransit");
            if (tracingOtpEndpoint != null)
            {
                tracing.AddOtlpExporter(oltpOptions =>
                {
                    oltpOptions.Endpoint = new Uri(tracingOtpEndpoint);
                    oltpOptions.ExportProcessorType = ExportProcessorType.Batch;
                    oltpOptions.Protocol = OtlpExportProtocol.Grpc;
                });
            }
            else 
            {
                tracing.AddConsoleExporter();
            }
        });
    }
}