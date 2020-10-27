using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Net.Http;
using System.Reflection;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Threading;

namespace CalcExpProxy
{
    class Program
    {
        static void Main(string[] args)
        {
            MyVisitor mv = new MyVisitor();
            var input = Expression.Constant("(2+3)/12*7+8*9", typeof(string));
            var res = Expression.Lambda<Func<double>>(mv.Visit(input)).Compile()();
            Console.WriteLine(res);
            Console.WriteLine(Calculator.GetReqAsync("2+3").Result);
        }
    }
}