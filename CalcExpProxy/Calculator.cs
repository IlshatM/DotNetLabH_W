using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CalcExpProxy
{
    static class Calculator
    {
        public static bool ContainsOutSide(string expr, char r)
        {
            bool inside = false;
            foreach (var i in expr)
            {
                if (i == '(') inside = true;
                else if (i == ')') inside = false;
                if (i == r && !inside) return true;
            }

            return false;
        }
        public static string[] Split(string expression, char r)
        {
            StringBuilder sb = new StringBuilder();
            string[] str = new string[2];
            bool inside = false;
            bool got_first = false;
            foreach (var i in expression)
            {
                if (!got_first)
                {
                    if (i == '(')
                    {
                        inside = true;
                        sb.Append(i);
                    }
                    else if (i == ')')
                    {
                        inside = false;
                        sb.Append(i);
                    }
                    else if (i == r && !inside)
                    {
                        str[0] = sb.ToString();
                        got_first = true;
                        sb.Clear();
                    }
                    else
                    {
                        sb.Append(i);
                    }
                }
                else
                {
                    sb.Append(i);
                }


            }

            str[1] = sb.ToString();
            return str;
        }

        public static async Task<double> GetReqAsync(string expression)
        {
            HttpClient client = new HttpClient();
            var result = await client.GetAsync($"https://localhost:5001/calculate?expression={ConvertExpression(expression)}");
            return Convert.ToDouble(result.Headers.GetValues("calculator_result").First());
        }
        private static string ConvertExpression(string expression)
        {
            expression = expression.Replace("+", "%2B");
            expression = expression.Replace("*", "%2A");
            expression = expression.Replace("/", "%2F");
            return expression;
        }
        
    }
    }
