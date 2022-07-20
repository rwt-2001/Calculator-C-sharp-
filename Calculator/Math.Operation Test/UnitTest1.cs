using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Math.Operations;
namespace Math.Operation_Test
{
    [TestClass]
    public class UnitTest1
    {
        ExpressionEvaluator Calculator = new ExpressionEvaluator();
        [TestMethod]
        public void TestNormalExpression()
        {
            Assert.AreEqual(Calculator.Evaluate("6+59+9*6/3+8*4-2"), 113);
            Assert.AreEqual(Calculator.Evaluate("3! + 2 * 3"),12);
        }

        [TestMethod]
        public void TestExpressionWithBracket()
        {
            Assert.AreEqual(Calculator.Evaluate("((3+5) * (10 -(3 + 1)))"),48);
            Assert.AreEqual(Calculator.Evaluate("((3+5) * (10 - (3 + 1)) )- ((80/10) + 3*2 )"), 34);
        }

        [TestMethod]
        public void TestExceptions()
        {
            Exception exception;

            /* Check for ImproperBracketsException */
            exception = Assert.ThrowsException<ImproperBracketsException>(() => Calculator.Evaluate("(3*2)+2-(4)*69+4)"));

            /* Check for InvalidExpressionException */
            exception = Assert.ThrowsException<InvalidExpressionException>(() => Calculator.Evaluate("4*59-"));

            /* Check for divide by 0 */
            exception = Assert.ThrowsException<DivideByZeroException>(() => Calculator.Evaluate("4/(2-2)"));

            /* Check Undefined Operation */
            exception = Assert.ThrowsException<UndefinedOperationException>(() => Calculator.Evaluate("4//2"));
        }
    }
}