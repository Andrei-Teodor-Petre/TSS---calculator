namespace Calculator;

public class Calculator
{
	public Calculator()
	{
	}

	public int? Eval(string input)
    {
		var tokens = input.Split(" ").ToList();

		return Convert.ToInt32(tokens[0]) + Convert.ToInt32(tokens[2]);
    }
}
