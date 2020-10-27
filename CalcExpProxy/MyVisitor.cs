using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CalcExpProxy
{
    class MyVisitor : ExpressionVisitor
    {
        protected override Expression VisitConstant(ConstantExpression node)
        {
            if (node.Type == typeof(string))
            {
                if(Calculator.ContainsOutSide(Convert.ToString(node.Value),'+'))
                {
                    var str = Calculator.Split(Convert.ToString(node.Value), '+');
                    var left = this.Visit(Expression.Constant(str[0], typeof(string)));
                    var right = this.Visit(Expression.Constant(str[1], typeof(string)));
                    var res = Expression.Add(left, right);
                    return res;
                }
                if (Calculator.ContainsOutSide(Convert.ToString(node.Value),'-'))
                {
                    var str = Calculator.Split(Convert.ToString(node.Value), '-');
                    var left = this.Visit(Expression.Constant(str[0], typeof(string)));
                    var right = this.Visit(Expression.Constant(str[1], typeof(string)));
                    var res = Expression.Subtract(left, right);
                    return res;
                }
                if(Calculator.ContainsOutSide(Convert.ToString(node.Value),'*'))
                {
                    var str = Calculator.Split(Convert.ToString(node.Value), '*');
                    var left = this.Visit(Expression.Constant(str[0], typeof(string)));
                    var right = this.Visit(Expression.Constant(str[1], typeof(string)));
                    var res = Expression.Multiply(left, right);
                    return res;
                }

                if (Calculator.ContainsOutSide(Convert.ToString(node.Value),'/'))
                {
                    var str = Calculator.Split(Convert.ToString(node.Value), '/');
                    var left = this.Visit(Expression.Constant(str[0], typeof(string)));
                    var right = this.Visit(Expression.Constant(str[1], typeof(string)));
                    var res = Expression.Divide(left, right);
                    return res;
                }

                if (Convert.ToString(node.Value)[0] == '(' && 
                    Convert.ToString(node.Value)[Convert.ToString(node.Value).Length-1] == ')')
                {
                    string s = Convert.ToString(node.Value).Remove(0, 1);
                    s = s.Remove(s.Length - 1, 1);
                    return this.Visit(Expression.Constant(s, typeof(string)));
                }
                else
                {
                    return Expression.Constant(Convert.ToDouble(node.Value), typeof(double));
                }

            }
            return node;
        }
        
    }
}