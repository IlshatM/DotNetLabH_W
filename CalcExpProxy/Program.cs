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
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CalcExpProxy
{
    class Program
    {
        
        static async Task Main(string[] args)
        {
            var serviceProvider = ServiceProvider();
            var calculator = serviceProvider.GetService<ICalculatorAsync>();    
            Solver solver = new Solver(calculator);
            Console.WriteLine(await solver.SolveAsync("(2+3)/12*7+8*9"));
        }

        public static ServiceProvider ServiceProvider()
        {
            var services = new ServiceCollection();
            services.AddSingleton<ICalculatorAsync, CalculatorInServer>();
            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }
    }
}