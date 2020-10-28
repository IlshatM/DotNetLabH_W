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
        public static void PrintTree(MyTree mt)
        {
            Console.WriteLine(mt);
            PrintTree(mt.Left);
            PrintTree(mt.Right);
        }
        static void Main(string[] args)
        {
            MyVisitor mv = new MyVisitor();
            var input = Expression.Constant("(2+3)/12*7+8*9", typeof(string));
            var res = Expression.Lambda<Func<double>>(mv.Visit(input)).Compile()();
            var tree = Calculator.CreateTree(mv.Visit(input));
        }
    }
}