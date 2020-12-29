using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CalcExpProxy
{
    public class MyVisitor : MyExpressionVisitor
    {
        public override Expression RealVisit(ConstantExpression node)
        {
            if (node.Type == typeof(string))
            {
                if(ContainsOutSide(Convert.ToString(node.Value),'+'))
                {
                    var str = Split(Convert.ToString(node.Value), '+');
                    var left = this.Visit(Expression.Constant(str[0], typeof(string)));
                    var right = this.Visit(Expression.Constant(str[1], typeof(string)));
                    var res = Expression.Add(left, right);
                    return res;
                }
                if (ContainsOutSide(Convert.ToString(node.Value),'-'))
                {
                    var str = Split(Convert.ToString(node.Value), '-');
                    var left = this.Visit(Expression.Constant(str[0], typeof(string)));
                    var right = this.Visit(Expression.Constant(str[1], typeof(string)));
                    var res = Expression.Subtract(left, right);
                    return res;
                }
                if(ContainsOutSide(Convert.ToString(node.Value),'*'))
                {
                    var str = Split(Convert.ToString(node.Value), '*');
                    var left = this.Visit(Expression.Constant(str[0], typeof(string)));
                    var right = this.Visit(Expression.Constant(str[1], typeof(string)));
                    var res = Expression.Multiply(left, right);
                    return res;
                }

                if (ContainsOutSide(Convert.ToString(node.Value),'/'))
                {
                    var str = Split(Convert.ToString(node.Value), '/');
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
        public MyTree root;
        public override Expression RealVisit(BinaryExpression node)
        {
            var temp = root;
            if (node.Left != null)
            {
                root.Left = new MyTree(node.Left);
                root = root.Left;
                Visit(node.Left);
            }
            root = temp;
            if (node.Right != null)
            {
                
                root.Right = new MyTree(node.Right);
                root = root.Right;
                Visit(node.Right);
                
            }

            return node;
        }

        public static bool ContainsOutSide(string expr, char r)
        {
            bool inside = false;
            foreach (var i in expr)
            {
                if (i == '(') inside = true;
                else if (i == ')') inside = false;
                if (i == r && !inside) return true;
            }

            return false;
        }
        public static string[] Split(string expression, char r)
        {
            StringBuilder sb = new StringBuilder();
            string[] str = new string[2];
            bool inside = false;
            bool gotFirst = false;
            foreach (var i in expression)
            {
                if (!gotFirst)
                {
                    if (i == '(')
                    {
                        inside = true;
                        sb.Append(i);
                    }
                    else if (i == ')')
                    {
                        inside = false;
                        sb.Append(i);
                    }
                    else if (i == r && !inside)
                    {
                        str[0] = sb.ToString();
                        gotFirst = true;
                        sb.Clear();
                    }
                    else
                    {
                        sb.Append(i);
                    }
                }
                else
                {
                    sb.Append(i);
                }
            }
            str[1] = sb.ToString();
            return str;
        }
    }
}