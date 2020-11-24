using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CalcExpProxy
{
    class CalculatorInServer:ICalculatorAsync
    {
        public async Task<double?> CalculateAsync(string expression)
        {
            HttpClient client = new HttpClient();
            var result = await client.GetAsync
                ($"https://localhost:5001/calculate?expression={ConvertExpression(expression)}");
            return Convert.ToDouble(result.Headers.GetValues("calculator_result").First());
        }

        private string ConvertExpression(string expression)
        {
            expression = expression.Replace("+", "%2B");
            expression = expression.Replace("*", "%2A");
            expression = expression.Replace("/", "%2F");
            return expression;
        }
    }
}