using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace CalcExpProxy
{
    class TreeCompilator : ExpressionVisitor
    {
        public MyTree root;
        protected override Expression VisitBinary(BinaryExpression node)
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
    }
}