using System;
using System.Threading.Tasks;
using Xunit;
using CalcExpProxy;

namespace CalcExpProxyTest
{
    
    public class UnitTest1
    {
        private Solver solver = new Solver(new CalculatorInApp());

        [Fact]
        public void ExpressionTreeCompliedTest()
        {
            Assert.Equal(11.0,solver.CompiledExpressionResult("(2+3)/5*11"));
        }

        [Theory]
        [InlineData("2+3")]
        [InlineData("3-1")]
        [InlineData("11*12")]
        [InlineData("42/2")]
        [InlineData("(1+1)*3")]
        [InlineData("(2+3)/12*7+8*9")]
        [InlineData("(2+3)/12*7+8*9-52/(2*1/1)")]
        [InlineData("1")]
        [InlineData("0/2")]
        [InlineData("1/0")]
        public void Tests(string expression)
        {

            Assert.Equal(solver.CompiledExpressionResult(expression), 
                new Solver(new CalculatorInApp()).SolveAsync(expression).Result);
        }
    }
}