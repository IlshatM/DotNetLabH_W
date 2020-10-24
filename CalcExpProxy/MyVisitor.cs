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
                    List<Expression> args = new List<Expression>();
                    foreach (var i in  Calculator.Split(Convert.ToString(node.Value),'+'))
                    {
                        args.Add(Expression.Constant(i,typeof(string)));
                    }

                    for (int i = 0; i < args.Count; i++)
                    {
                        args[i] = this.Visit(args[i]);
                    }
                    var res = 
                        Expression.Call(typeof(Calculator).GetMethod(nameof(Calculator.Root)),Expression.NewArrayInit(typeof(double),args));
                    
                    return res;
                }
                if (Calculator.ContainsOutSide(Convert.ToString(node.Value),'-'))
                {
                    List<Expression> args = new List<Expression>();
                    foreach (var i in Calculator.Split(Convert.ToString(node.Value),'-'))
                    {
                        args.Add(Expression.Constant(i,typeof(string)));
                    }

                    for (int i = 0; i < args.Count; i++)
                    {
                        args[i] = this.Visit(args[i]);
                    }
                    var res = 
                        Expression.Call(typeof(Calculator).GetMethod(nameof(Calculator.MinusRoot)),Expression.NewArrayInit(typeof(double),args));
                    return res;
                }
                if(Calculator.ContainsOutSide(Convert.ToString(node.Value),'*'))
                {
                    List<Expression> args = new List<Expression>();
                    foreach (var i in Convert.ToString(node.Value).Split('*'))
                    {
                        args.Add(Expression.Constant(i,typeof(string)));
                    }

                    for (int i = 0; i < args.Count; i++)
                    {
                        args[i] = this.Visit(args[i]);
                    }
                    var res = 
                        Expression.Call(typeof(Calculator).GetMethod(nameof(Calculator.MultiRoot)),Expression.NewArrayInit(typeof(double),args));
                    return res;
                }

                if (Calculator.ContainsOutSide(Convert.ToString(node.Value),'/'))
                {
                    List<Expression> args = new List<Expression>();
                    foreach (var i in Convert.ToString(node.Value).Split('/'))
                    {
                        args.Add(Expression.Constant(i,typeof(string)));
                    }

                    for (int i = 0; i < args.Count; i++)
                    {
                        args[i] = this.Visit(args[i]);
                    }
                    var res = 
                        Expression.Call(typeof(Calculator).GetMethod(nameof(Calculator.DevRoot)),Expression.NewArrayInit(typeof(double),args));
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
                    return Expression.Call(typeof(Calculator).GetMethod(nameof(Calculator.GetReq)),
                        Expression.Constant(Convert.ToString(node.Value),
                            typeof(string)
                        )
                    );
                }

            }
            return node;
        }
        
    }
}