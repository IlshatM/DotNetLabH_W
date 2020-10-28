using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace CalcExpProxy
{
    public class MyTree
    {
        public double? Value;
        public MyTree Left;
        public MyTree Right;
        public char Operation;
        public Func<string, Task<double>> Calculate = Calculator.GetReqAsync;

        public MyTree(Expression expression)
        {
            switch (expression.NodeType)
            {
                case (ExpressionType.Add):
                    Operation = '+';
                    break;
                case (ExpressionType.Subtract):
                    Operation = '-';
                    break;
                case (ExpressionType.Multiply):
                    Operation = '*';
                    break;
                case (ExpressionType.Divide):
                    Operation = '/';
                    break;
                case(ExpressionType.Constant):
                    Operation = 'V';
                    Value = Convert.ToDouble(((ConstantExpression) expression).Value);
                    break;
                default:
                    Operation = '?';
                    break;
            } 
        }

        public override string ToString()
        {
            return $"{Operation}";
        }
    }
}