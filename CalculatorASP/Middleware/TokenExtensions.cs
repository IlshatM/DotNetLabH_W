using CalculatorASP.Services;
using Microsoft.AspNetCore.Builder;

namespace CalculatorASP
{
    public static class TokenExtensions
    {
        public static IApplicationBuilder UseResult(this IApplicationBuilder builder, string pattern,
            ICalculator calculator)
        {
            return builder.UseMiddleware<ResultMiddleware>(pattern, calculator);
        }
        public static IApplicationBuilder UseExpression(this IApplicationBuilder builder, string pattern)
        {
            return builder.UseMiddleware<ExpressionMiddleware>(pattern);
        }
        public static IApplicationBuilder UseCalculator
            (this IApplicationBuilder builder, string pattern, ICalculator calculator)
        {
            return builder.UseMiddleware<CalculateMiddleware>(pattern, calculator);
        }
    }
}