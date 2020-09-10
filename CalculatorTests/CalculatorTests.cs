using HW1;
using NUnit.Framework;

namespace CalculatorTests
{
    [TestFixture]
    public class CalculatorTestCase
    {
        [TestCase]
        public void Calculate_Plus_2_3_Returned5()
        {
            Assert.AreEqual(5, Calculator.Calculate("2+3"));
        }
        [TestCase]
        public void Calculate_Minus_2_3_ReturnedMinus1()
        {
            Assert.AreEqual(-1, Calculator.Calculate("2-3"));
        }
        [TestCase]
        public void Calculate_Multiply_2_3_Returned6()
        {
            Assert.AreEqual(6, Calculator.Calculate("2*3"));
        }
        [TestCase]
        public void Calculate_Devide_10_5_Returned2()
        {
            Assert.AreEqual(2, Calculator.Calculate("10/5"));
        }
    }
}