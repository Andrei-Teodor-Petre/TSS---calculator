using NUnit.Framework;

class CalculatorTests
{
    [Test]
    public void OnePlusOne()
    {
        var calc = new Calculator.Calculator();

        var result = calc.Eval("1 + 1");

        Assert.AreEqual(2, result);
    }

    [Test]
    public void OnePlusTwo()
    {
        var calc = new Calculator.Calculator();

        var result = calc.Eval("1 + 2");

        Assert.AreEqual(3, result);
    }

    [Test]
    public void OnePlusTwoPlusThree()
    {
        var calc = new Calculator.Calculator();

        var result = calc.Eval("1 + 2 + 3");

        Assert.AreEqual(6, result);
    }


    [Test]
    public void FloatPlus()
    {
        var calc = new Calculator.Calculator();

        var result = calc.Eval("1.64 + 2.05");

        Assert.AreEqual(3.69, result);
    }
}
