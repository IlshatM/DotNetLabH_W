using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Extensions.DependencyInjection;

namespace CalcExpProxy
{
    public class Solver
    {
        private ICalculatorAsync CalculatorAsync;
        public Solver(ICalculatorAsync calculatorAsync)
        {
            CalculatorAsync = calculatorAsync;
        }
        public MyTree CreateTree(Expression expression)
        {
            TreeCompilator treeCompilator = new TreeCompilator();
            treeCompilator.root=new MyTree(expression);
            MyTree toReturn = treeCompilator.root;
            treeCompilator.Visit(expression);
            return toReturn;
        }

        public double CompiledExpressionResult(string expression)
        {
            MyVisitor mv = new MyVisitor();
            var input = Expression.Constant(expression, typeof(string));
            var exprtree = mv.Visit(input);
            return Expression.Lambda<Func<double>>(exprtree).Compile()();
        }

        public async Task<double> SolveAsync(string expression)
        {
            MyVisitor mv = new MyVisitor();
            var input = Expression.Constant(expression, typeof(string));
            var tree = CreateTree(mv.Visit(input));
            await ProcessInParallelAsync(tree);
            Console.WriteLine(tree.displayNode());
            return (double) tree.Value;
        }
        public async Task ProcessInParallelAsync(MyTree tree)
        {
            Task t1 = null;
            Task t2 = null;
            if (tree.Left != null)
            {
                if (tree.Left.Operation != 'V')
                {
                    t1 = ProcessInParallelAsync(tree.Left);
                }
            }

            if (tree.Right != null)
            {
                if (tree.Right.Operation != 'V')
                {
                    t2 = ProcessInParallelAsync(tree.Right);
                }
            }

            if (t1 != null && t2 != null)
            {
                await Task.WhenAll(t1, t2);
                
            }
            else if (t1 == null && t2 != null) 
            {
                await t2;
            }
            else if (t1 != null) 
            {
                await t1;
            }

            await tree.GetValue(CalculatorAsync);
        }
    }
    }
