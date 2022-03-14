namespace Calculator;

public class Calculator
{
	public Calculator()
	{
	}

	private char[] operators = new char[] { '+', '-', '*', '/' };


	public decimal? Eval(string input)
    {

		
		var tokens = ValidateInput(input);

		if(tokens is null)
        {
			return null;
        }
		
		var operatorToks = tokens.Where(to => char.TryParse(to, out _) && operators.Contains(char.Parse(to))).ToList();
		var numbers = tokens.Where(op => !operatorToks.Contains(op)).ToList();

		return numbers.Sum(nu => decimal.Parse(nu));
    }

    private List<string>? ValidateInput(string input)
    {

		var tokens = input.Split(operators, StringSplitOptions.TrimEntries).ToList();

		foreach (var tok in tokens)
        {
			if (!(char.TryParse(tok, out _) && operators.Contains(char.Parse(tok))) && !decimal.TryParse(tok, out _))
            {
				return null;
            }
        }

		return tokens;
	}
}
