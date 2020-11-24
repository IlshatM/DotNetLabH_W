using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace CalcExpProxy
{
    public class MyTree
    {
        public double? Value;
        public MyTree Left;
        public MyTree Right;
        public char Operation;
        private char oper_to_write;

        public string displayNode()
        {
            StringBuilder output = new StringBuilder();
            displayNode(output, 0);
            return output.ToString();
        }
 
        private void displayNode(StringBuilder output, int depth)
        {
 
            if (Right != null)
                Right.displayNode(output, depth + 1);
 
            output.Append('\t', depth);
            output.AppendLine(depth.ToString()+"|"+Math.Round(Value.Value,2) + oper_to_write);
 
 
            if (Left != null)
                Left.displayNode(output, depth + 1);
 
        }
        public async Task GetValue(ICalculatorAsync CalculatorAsync)
        {
            if (Value == null)
            {
                var t = CalculatorAsync.CalculateAsync($"{Left.Value}{Operation}{Right.Value}");

                Value = await t.ConfigureAwait(false);
                Operation = 'V';
            }
        }

        public MyTree(Expression expression)
        {
            switch (expression.NodeType)
            {
                case (ExpressionType.Add):
                    Operation = '+';
                    oper_to_write = '+';
                    break;
                case (ExpressionType.Subtract):
                    Operation = '-';
                    oper_to_write = '-';
                    break;
                case (ExpressionType.Multiply):
                    Operation = '*';
                    oper_to_write = '*';
                    break;
                case (ExpressionType.Divide):
                    Operation = '/';
                    oper_to_write = '/';
                    break;
                case(ExpressionType.Constant):
                    Operation = 'V';
                    oper_to_write = ' ';
                    Value = Convert.ToDouble(((ConstantExpression) expression).Value);
                    break;
                default:
                    Operation = '?';
                    break;
            } 
        }
    }
}