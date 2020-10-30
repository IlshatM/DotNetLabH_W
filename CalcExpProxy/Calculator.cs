using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Extensions.DependencyInjection;

namespace CalcExpProxy
{ 
    class Calculator:ICalculatorAsync
    {
        public MyTree CreateTree(Expression expression)
        {
            TreeCompilator treeCompilator = new TreeCompilator();
            treeCompilator.root=new MyTree(expression);
            MyTree toReturn = treeCompilator.root;
            treeCompilator.Visit(expression);
            return toReturn;
        }

        public async Task<double> CalculateAsync(string expression)
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
            if (tree.Left.Operation != 'V')
            {
                t1 = ProcessInParallelAsync(tree.Left);
            }

            if (tree.Right.Operation != 'V')
            {
                t2 = ProcessInParallelAsync(tree.Right);
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

            await tree.GetValue();
        }
        public async Task<double?> GetReqAsync(string expression)
        {
            HttpClient client = new HttpClient();
            var result = await client.GetAsync
                ($"https://localhost:5001/calculate?expression={ConvertExpression(expression)}");
            return Convert.ToDouble(result.Headers.GetValues("calculator_result").First());
        }
        
        private string ConvertExpression(string expression)
        {
            expression = expression.Replace("+", "%2B");
            expression = expression.Replace("*", "%2A");
            expression = expression.Replace("/", "%2F");
            return expression;
        }
        
        
    }
    }
