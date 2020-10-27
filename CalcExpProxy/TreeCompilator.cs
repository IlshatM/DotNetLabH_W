using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CalcExpProxy
{
    class TreeCompilator : ExpressionVisitor
    {
        protected override Expression VisitBinary(BinaryExpression node)
        {
            return node;
        }
    }
}