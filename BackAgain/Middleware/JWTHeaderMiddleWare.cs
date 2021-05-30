using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Middleware
{
    public class JWTHeaderMiddleWare
    {
        private readonly RequestDelegate _Next;

        public JWTHeaderMiddleWare(RequestDelegate rd)
        {
            _Next = rd;
        }

        public async Task Invoke(HttpContext context)
        {
            string cookie = context.Request.Cookies["AccessToken"];

            if(cookie != null)
            {
                context.Request.Headers.Append("Authorization", $"Bearer {cookie}");
            }
            await _Next.Invoke(context);
        }
    }
}
