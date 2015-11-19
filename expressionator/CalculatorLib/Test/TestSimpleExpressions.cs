using System;
using System.Linq;
using Com.Rtwsq.Thom.Calculator.Config;
using Com.Rtwsq.Thom.Calculator.Validation;
using Xunit;

namespace Com.Rtwsq.Thom.Calculator.Test
{
    public class TestSimpleExpressions
    {
        public TestSimpleExpressions()
        {
            ArithmeticExpression.SetExpressionType(ExpressionType.IntegerBasic);    
        }

        [Fact]
        public void CanComputeNoParentheses()
        {
            Assert.Equal(int.Parse(new ArithmeticExpression("1+2-3*4-5+6").ItsValue), -8);
            Assert.Equal(int.Parse(new ArithmeticExpression("-1+2-3*4-5+6").ItsValue), -10);
        }

        [Fact]
        public void CanComputeWithParentheses()
        {
            Assert.Equal(int.Parse(new ArithmeticExpression("(1+2-3*4-5+6)").ItsValue), -8);
            Assert.Equal(int.Parse(new ArithmeticExpression("((1+2)-3*4-5+6)").ItsValue), -8);
            Assert.Equal(int.Parse(new ArithmeticExpression("((1+2-3)*4-5+6)").ItsValue), 1);

            Assert.Equal(int.Parse(new ArithmeticExpression("(1+2-3*(4-5+6))").ItsValue), -12);
            Assert.Equal(int.Parse(new ArithmeticExpression("(1+2-(3*(4-5+6)))").ItsValue), -12);
        }

        [Fact]
        public void CanComputeWithDeeplyStackedParentheses()
        {
            Assert.Equal(int.Parse(new ArithmeticExpression("(((((1+2)-3)*4)-5)+6)").ItsValue), 1);
            Assert.Equal(int.Parse(new ArithmeticExpression("(1+(2-(3*(4-(5+6)))))").ItsValue), 24);
        }

        [Fact]
        public void CanComputeWithDeeplyNestedParentheses()
        {
            Assert.Equal(int.Parse(new ArithmeticExpression("(((1+(2*(3+(4*5)+6)+7)*8)+9)-10)+11").ItsValue), 531);
            Assert.Equal(int.Parse(new ArithmeticExpression("(((2*3)+(4*5))*((6+7)*(8+9)+(10*11))+((12+13)+(14*15)+(16+17)*(18+19)))").ItsValue), 10062);
            Assert.Equal(int.Parse(new ArithmeticExpression("((-(2*3)+(4*5))*-((6+7)*(8+9)+(10*11))+(-(12+13)+(14*15)+-(16+17)*(18+19)))").ItsValue), -5670);
        }
        [Fact]
        public void CanApplyUnaryNegative()
        {
            Assert.Equal(int.Parse(new ArithmeticExpression("-((-((-(1 + 2) - 3) * 4) - 5) + 6)").ItsValue), -25);
            Assert.Equal(int.Parse(new ArithmeticExpression("1 + 2 * 3 - 4 + -5 * -6").ItsValue), 33);
            Assert.Equal(int.Parse(new ArithmeticExpression("-((3+(4*5)+6)+(3+(4*5)+6)*(3+(4*5)+6))").ItsValue), -870);
            Assert.Equal(int.Parse(new ArithmeticExpression("(-(-1 + -2)) * -(-3 - -4) + -(-5 * 6)").ItsValue), 27);
            Assert.Equal(int.Parse(new ArithmeticExpression("((-1 + -2)) * (-3 - -4) + (-5 * 6)").ItsValue), -33);
        }

        [Fact]
        public void RejectsDoubleUnary()
        {
            Assert.Null(new ArithmeticExpression("--1+2-3*4-5+6").ItsValue);
        }

        [Fact]
        public void RejectsMissingOperator()
        {
            Assert.Null(new ArithmeticExpression("-1 1+2-3*4-5+6").ItsValue);
        }
        [Fact]
        public void RejectsMissingParenthesis()
        {
            Assert.Null(new ArithmeticExpression("((1+2)-((3*4)-(5+6))").ItsValue);
        }
    }
}
