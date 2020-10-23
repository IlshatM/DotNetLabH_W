using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HW1;

namespace CalcExpProxy
{
    static class Calculator
    {
        public static double GetReq(string expression)
        {
            WebRequest myReq =
                WebRequest.Create($"https://localhost:5001/calculate?expression={ConvertExpression(expression)}");
            return Convert.ToDouble(myReq.GetResponse().Headers["calculator_result"]);
        }

        private static string ConvertExpression(string expression)
        {
            expression = expression.Replace("+", "%2B");
            expression = expression.Replace("*", "%2A");
            expression = expression.Replace("/", "%2F");
            return expression;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
        

        }
    }
}