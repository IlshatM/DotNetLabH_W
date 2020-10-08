using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CalculatorASP
{
    public class ExpressionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string variable_name;

        public ExpressionMiddleware(RequestDelegate next, string variableName)
        {
            _next = next;
            variable_name = variableName;
        }

        

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Query.ContainsKey(variable_name))
            {
                _next.Invoke(context);
            }
            else
            {
                context.Response.StatusCode = 404;
            }
            
        }
    }
}