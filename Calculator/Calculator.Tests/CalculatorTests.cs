using NUnit.Framework;

class CalculatorTests
{
    [Test]
    public void TwoEvalsToTwo()
    {
        var result = Calculator.Calculator.Eval("2");
        Assert.AreEqual(2, result);
    }

    [Test]
    public void OnePlusOne()
    {
        var result = Calculator.Calculator.Eval("1 + 1");
        Assert.AreEqual(2, result);
    }


    [Test]
    public void OnePlusOneNoSpace()
    {
        var result = Calculator.Calculator.Eval("1+1");
        Assert.AreEqual(2, result);
    }



    [Test]
    public void OnePlusTwoPlusThree()
    {
        var result = Calculator.Calculator.Eval("1 + 2 + 3");
        Assert.AreEqual(6, result);
    }


    [Test]
    public void DecimalPlus()
    {
        var result = Calculator.Calculator.Eval("1.64 + 2.05");
        Assert.AreEqual(3.69d, result);
    }


    [Test]
    public void ZeroPlus()
    {
        var result = Calculator.Calculator.Eval("0 + .05");
        Assert.AreEqual(0.05d, result);
    }


    [Test]
    public void OneMultiply()
    {
        var result = Calculator.Calculator.Eval("1 * .05");
        Assert.AreEqual(0.05d, result);
    }


    [Test]
    public void Division()
    {
        var result = Calculator.Calculator.Eval("5 / 2");
        Assert.AreEqual(2.5d, result);
    }

    [Test]
    public void TwoMinusOne()
    {
        var result = Calculator.Calculator.Eval("2 - 1");
        Assert.AreEqual(1d, result);
    }

    [Test]
    public void TwoMinusFive()
    {
        var result = Calculator.Calculator.Eval("2 - 5");
        Assert.AreEqual(-3d, result);
    }
    
    [Test]
    public void MinusTwoPlusOne()
    {
        var result = Calculator.Calculator.Eval("-2 + 1");
        Assert.AreEqual(-1d, result);
    }

    [Test]
    public void UnaryPlusOperator()
    {
        var result = Calculator.Calculator.Eval("+1");
        Assert.AreEqual(1, result);
    }

    [Test]
    public void TwoMinusMinusFive()
    {
        var result = Calculator.Calculator.Eval("2 --5");
        Assert.AreEqual(7d, result);
    }

    [Test]
    public void OnePlusTwoMultiplyThree()
    {
        var result = Calculator.Calculator.Eval("1 + 2 * 3");
        Assert.AreEqual(7d, result);
    }

    [Test]
    public void OneMultiplyTwoPlusThree()
    {
        var result = Calculator.Calculator.Eval("1 * 2 + 3");
        Assert.AreEqual(5d, result);
    }
    
    [Test]
    public void ComplexInputOrderOfOperations()
    {
        var result = Calculator.Calculator.Eval("2 * 3 + 5 * 7 + 8 / 4 - 1");
        Assert.AreEqual(42d, result);
    }

    [Test]
    public void TwoPowFive()
    {
        var result = Calculator.Calculator.Eval("2p5");
        Assert.AreEqual(32d, result);
    }

    [Test]
    public void TwoPowTen()
    {
        var result = Calculator.Calculator.Eval("2 p 10");
        Assert.AreEqual(1024d, result);
    }

    [Test]
    public void ThirdRootOfOne()
    {
        var result = Calculator.Calculator.Eval("1r3");
        Assert.AreEqual(1d, result);
    }

    [Test]
    public void ThirdRootOfEight()
    {
        var result = Calculator.Calculator.Eval("8 r 3");
        Assert.AreEqual(2d, result);
    }

    [Test]
    public void MultipleParenthesesTuples()
    {
        var result = Calculator.Calculator.Eval("(10 - 2) / (9 - 5)");
        Assert.AreEqual(2d, result);
    }

    [Test]
    public void ParenthesesOrderOperations()
    {
        var result = Calculator.Calculator.Eval("1 + (-2 - (-3))");
        Assert.AreEqual(2d, result);
    }

    [Test]
    public void VeryComplexInput()
    {
        var result = Calculator.Calculator.Eval("((2 + 5 * (4p2 - 1)) r 2) / (9 - 5)");
        Assert.AreEqual(2.19374109684803m, result);
    }

    [Test]
    public void InvalidInput()
    {
        var result = Calculator.Calculator.Eval("sadasd");
        Assert.IsNull(result);
    }

    [Test]
    public void UndefinedOperator()
    {
        var result = Calculator.Calculator.Eval("2@3");
        Assert.IsNull(result);
    }
}

