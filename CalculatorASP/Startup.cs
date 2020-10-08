using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using HW1;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CalculatorASP
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        private async Task AddResultInHeaders(HttpContext context, string result, string key)
        {
            context
                .Response
                .Headers
                .Add(key,result);
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseExpression("expression");
            app.UseCalculator("expression");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/calculate", async context =>
                {
                    await AddResultInHeaders(
                        context,
                        Calculator.Calculate(context.Request.Query["expression"]).ToString(),
                        "calculator_result"
                    );
                });

                
            });
        }
    }
}