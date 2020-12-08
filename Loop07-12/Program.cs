using System;
using BenchmarkDotNet.Running;

namespace Loop07_12
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<LoopClass>();
        }
    }
}