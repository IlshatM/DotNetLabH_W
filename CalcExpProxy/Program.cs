using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CalcExpProxy
{
    class MyVisitor : ExpressionVisitor
    {
        protected override Expression VisitConstant(ConstantExpression node)
        {
            if (node.Type == typeof(string))
            {
                if(Convert.ToString(node.Value).Contains('+'))
                {
                    List<Expression> args = new List<Expression>();
                    foreach (var i in Convert.ToString(node.Value).Split('+'))
                    {
                        args.Add(Expression.Constant(i,typeof(string)));
                    }

                    for (int i = 0; i < args.Count; i++)
                    {
                        args[i] = this.Visit(args[i]);
                    }
                    var res = 
                        Expression.Call(typeof(Calculator).GetMethod(nameof(Calculator.Root)),Expression.NewArrayInit(typeof(double),args));
                    
                    return res;
                }
                if (Convert.ToString(node.Value).Contains('-'))
                {
                    List<Expression> args = new List<Expression>();
                    foreach (var i in Convert.ToString(node.Value).Split('-'))
                    {
                        args.Add(Expression.Constant(i,typeof(string)));
                    }

                    for (int i = 0; i < args.Count; i++)
                    {
                        args[i] = this.Visit(args[i]);
                    }
                    var res = 
                        Expression.Call(typeof(Calculator).GetMethod(nameof(Calculator.MinusRoot)),Expression.NewArrayInit(typeof(double),args));
                    return res;
                }
                else
                {
                    return Expression.Call(typeof(Calculator).GetMethod(nameof(Calculator.GetReq)),
                        Expression.Constant(Convert.ToString(node.Value),
                            typeof(string)
                        )
                    );
                }

            }
            return node;
        }
        
    }
    
    static class Calculator
    {
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
        
        public static Expression CreateTree(string expr)
        {
            Expression start = Expression.Constant(expr,typeof(string));
            MyVisitor mv = new MyVisitor();
            var res = mv.Visit(start);
            return res;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Calculator.CreateTree("2+3*4-1+11-4/2"));
        }
    }
}