using System.Threading.Tasks;
using CalculatorASP.Services;
using Microsoft.AspNetCore.Http;
using Calculator = HW1.Calculator;

namespace CalculatorASP
{
    public class CalculateMiddleware
    {
        private readonly RequestDelegate _next;
            private readonly string variable_name;
            private ICalculator Calculator;

            public CalculateMiddleware(RequestDelegate next, string variableName, ICalculator calc)
            {
                _next = next;
                variable_name = variableName;
                Calculator = calc;
            }
            public async Task InvokeAsync(HttpContext context)
            {
                try
                {
                    var a = Calculator.Calculate(context.Request.Query[variable_name]);
                    _next.Invoke(context);
                }
                catch
                {
                    context.Response.StatusCode = 400;
                }
            }
    }
}