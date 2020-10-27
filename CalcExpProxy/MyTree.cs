using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CalcExpProxy
{
    public class MyTree
    {
        public double Val;
        public MyTree Left;
        public MyTree Right;
        public Func<double, double, double> calc;
    }
}