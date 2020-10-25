using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Reflection;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Threading;
using System.Threading.Tasks;

namespace CalcExpProxy
{
    class TreeCompilator : ExpressionVisitor
    {
        protected override Expression VisitNewArray(NewArrayExpression node)
        {
            List<object> ls = new List<object>();
            List<Task<object>> tasks = new List<Task<object>>();
            foreach (var i in node.Expressions)
            {
                tasks.Add(Task.Run(() => ((ConstantExpression) this.Visit(i)).Value));
            }

            Task.WhenAll(tasks).Wait();
            foreach (var i in tasks)
            {
                ls.Add(i.Result);
            }
            return Expression.Constant(ls, typeof(List<object>));
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            return Expression.Constant(new object[] {node.Value}, typeof(object[]));
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            var args = this.Visit(node.Arguments.First());
            List<object> param = new List<object>();
            foreach (var i in (IEnumerable)((ConstantExpression)args).Value)
            {
                param.Add(i);
            }

            object res;
            if (param[0].GetType() == typeof(string))
            {
                res = node.Method.Invoke(null, param.ToArray());
            }
            else
            {
                List<double> p = new List<double>();
                for(int i = 0;i<param.Count;i++)
                {
                    p.Add((double)param[i]);
                }
                res = node.Method.Invoke(null, new[] {p.ToArray()});
            }

            return Expression.Constant(res, typeof(double));
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var res = Calculator.CreateTree("12+(3-6)*2");
            Console.WriteLine(res);
            TreeCompilator tc =new TreeCompilator();
            
            var a = tc.Visit(res);
            Console.WriteLine(a);
        }
    }
}