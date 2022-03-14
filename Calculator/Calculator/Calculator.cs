namespace Calculator;

public class Calculator
{
	public Calculator()
	{
	}

	private List<String> operators = new List<String> { "+", "-", "*", "/" };


	public int? Eval(string input)
    {

		
		bool valid = ValidateInput(input);

		if(!valid)
        {
			return null;
        }

		var tokens = input.Split(" ").ToList();
		var operatorToks = tokens.Where(to => operators.Contains(to)).ToList();
		var numbers = tokens.Where(op => !operatorToks.Contains(op)).ToList();

		return numbers.Sum(nu => Int32.Parse(nu));
    }

    private bool ValidateInput(string input)
    {

		var tokens = input.Split(" ").ToList();

		foreach(var tok in tokens)
        {
			if (!operators.Contains(tok) && !Int32.TryParse(tok, out _))
            {
				return false;
            }
        }

		return true;
	}
}
