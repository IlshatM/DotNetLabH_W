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

        public static double FinalCalculate(List<double> mas, List<char> act)
        {
            var res = mas[0];
            
            for(int i = 1;i<mas.Count;i++)
            {
                if (act[i - 1] == '+')
                {
                    res = GetReq($"{res}+{mas[i]}");
                }else if (act[i - 1] == '-')
                {
                    res = GetReq($"{res}-{mas[i]}");
                }
            }

            return res;
        }
        public static List<double> SimpleExpressionsCalculate(List<string> expr)
        {
            List<double> res = new List<double>();
            foreach (var i in expr)
            {
                res.Add(SimpExpr(i));
            }

            return res;
        }

        public static double SimpExpr(string expr)
        {
            StringBuilder num = new StringBuilder();
            int i = 0;
            while (Char.IsDigit(expr[i]))
            {
                num.Append(expr[i]);
                
                i++;
                if(i>=expr.Length) break;
            }

            double res = Convert.ToDouble(num.ToString());
            num.Clear();
            if (i < expr.Length)
            {
                num.Append(expr[i]);

                expr += ' ';
                for (int j = i + 1; j < expr.Length; j++)
                {
                    if (!Char.IsDigit(expr[j]))
                    {
                        res = GetReq($"{res}{num}");
                        num.Clear();
                    }

                    num.Append(expr[j]);
                }
            }

            return res;
        }

        public static List<char> GetActs(string expression)
        {
            List<char> actions = new List<char>();
            
            foreach (var i in expression)
            {
                if (i == '+' || i == '-')
                {
                    actions.Add(i);
                }
            }

            return actions;
        }
        public static List<string> Split(string expression)
        {
            List<string> expressions = new List<string>();

            expressions = expression.Split('+', '-').ToList();
            return expressions;
        }
        
        private static string ConvertExpression(string expression)
        {
            expression = expression.Replace("+", "%2B");
            expression = expression.Replace("*", "%2A");
            expression = expression.Replace("/", "%2F");
            return expression;
        }

        public static Expression expr = Expression.Parameter(typeof(string), "expr");
        public static Expression splt = Expression.Call(typeof(Calculator).GetMethod("Split"), expr);
        public static Expression getactions = Expression.Call(typeof(Calculator).GetMethod("GetActs"),expr);
        public static Expression simple_expression = 
            Expression.Call(typeof(Calculator).GetMethod("SimpleExpressionsCalculate"), splt);
        public static Expression final_calculation = 
            Expression.Call(typeof(Calculator).GetMethod("FinalCalculate"), simple_expression, getactions);
    }
    class Program
    {
        static void Main(string[] args)
        {
            
        }
    }
}