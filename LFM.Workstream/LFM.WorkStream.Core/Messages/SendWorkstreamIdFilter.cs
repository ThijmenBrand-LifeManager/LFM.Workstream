using MassTransit;
using MassTransit.Serialization;
using Microsoft.AspNetCore.Http;

namespace LFM.Authorization.Core.Messages;

public class SendWorkstreamIdFilter<T> : IFilter<SendContext<T>> where T : class
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SendWorkstreamIdFilter(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void Probe(ProbeContext context)
    {
        context.CreateFilterScope("send-workstream-id-header");
    }

    public Task Send(SendContext<T> context, IPipe<SendContext<T>> next)
    {
        if (_httpContextAccessor.HttpContext != null)
        {
            var workstreamId = _httpContextAccessor.HttpContext.Request.RouteValues["workstreamId"] as string;
            context.Headers.Set("workstreamId", workstreamId);
        }

        return next.Send(context);
    }
}