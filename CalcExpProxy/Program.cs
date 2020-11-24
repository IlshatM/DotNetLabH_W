using System;
using System.Collections.Concurrent;
using System.Diagnostics;
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
            Console.WriteLine(Calculator.Calculate("(2+3) / 12 * 7 + 8 * 9"));
        }
    }
}