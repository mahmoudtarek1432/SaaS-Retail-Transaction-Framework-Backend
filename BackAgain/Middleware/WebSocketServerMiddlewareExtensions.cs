using BackAgain.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Middleware
{
    public static class WebSocketServerMiddlewareExtensions
    {
        public static IApplicationBuilder UseWebSocketServer(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<WebSocketServerMiddleware>();
        }

        public static IServiceCollection AddWebSocketServerConnectionManager(this IServiceCollection service)
        {
            service.AddSingleton<WebSocketConnectionManager>();
            return service;
        }
    }
}
