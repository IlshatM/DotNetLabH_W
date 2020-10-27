using System;
using System.Threading.Tasks;
using CalculatorASP.Services;
using Microsoft.AspNetCore.Http;

namespace CalculatorASP
{
    public class ResultMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string variable_name;
        private ICalculator Calculator;

        public ResultMiddleware(RequestDelegate next, string variableName, ICalculator calculator)
        {
            _next = next;
            variable_name = variableName;
            Calculator = calculator;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var res = Calculator.Calculate(context.Request.Query["expression"]);
            await AddResultInHeaders(context, Convert.ToString(res), "calculator_result");
        }
        private async Task AddResultInHeaders(HttpContext context, string result, string key)
        {
            context
                .Response
                .Headers
                .Add(key,result);
        }
    }
}