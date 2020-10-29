using System;
using System.Collections.Concurrent;
using System.Diagnostics;
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
    class Program
    {
        static async Task Main(string[] args)
        {
           Console.WriteLine(await Calculator.CalculateAsync("(2+3)/12*7+8*9"));
        }
    }
}