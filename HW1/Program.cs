using System;
using System.Collections.Generic;
namespace HW1
{
    public class Program
    {
        public static double Res;
        public static void Main(string[] args)
        {
            Res = Calculator.Calculate("2+3-1");
            Console.WriteLine(Res);
        }
    }
}
