using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;

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
        public static double DevRoot(params double[] mas)
        {
            var res = mas[0]*mas[0];
            foreach (var i in mas)
            {
                res = GetReq($"{res}/{i}");
            }

            return res;
        }
        public static string[] Split(string expression, char r)
        {
            StringBuilder sb = new StringBuilder();
            bool inside = false;
            List<string> result = new List<string>();
            foreach (var i in expression)
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
                else if (!inside && i == r)
                {
                    result.Add(sb.ToString());
                    sb.Clear();
                }
                else
                {
                    sb.Append(i);
                }
            }
            result.Add(sb.ToString());
            return result.ToArray();
        }
        public static double MultiRoot(params double[] mas)
        {
            var res = 1.0;
            foreach (var i in mas)
            {
                res = GetReq($"{res}*{i}");
            }

            return res;
        }
        public static double Root(params double[] mas)
        {
            var res = 0.0;
            foreach (var i in mas)
            {
                res = GetReq($"{res}+{i}");
            }
            return res;
        }
        public static double MinusRoot(params double[] mas)
        {
            var res = mas[0] * 2;
            foreach (var i in mas)
            {
                res = GetReq($"{res}-{i}");
            }
            
            return res;
        }
        
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
        
        private static Expression CreateTree(string expr)
        {
            Expression start = Expression.Constant(expr,typeof(string));
            MyVisitor mv = new MyVisitor();
            var res = mv.Visit(start);
            return res;
        }

        private static Expression CalculateTree(Expression tree)
        {
            TreeCompilator tc = new TreeCompilator(); 
            return tc.Visit(tree);
        }

        public static double Calculate(string expr)
        { 
            StringBuilder pure_expr = new StringBuilder();
            char[] suitable_symbols = new[] {'+', '-', '*', '/', '(', ')'};
            foreach (var i in expr)
            {
                if (suitable_symbols.Contains(i) || Char.IsDigit(i))
                {
                    pure_expr.Append(i);
                }
            }
            return (double)((ConstantExpression)CalculateTree(CreateTree(pure_expr.ToString()))).Value;
        }
    }
}