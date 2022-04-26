namespace Calculator
{
    public enum OperatorTypes : int
    {
		AdditionPlus,
		Multiply,
		Division,
		NegativeNumberMinus,
		DifferenceMinus,
		PositiveNumberPlus,
		Undefined
	}

	public class Node
	{
		public string Value { get; set; }
		public Node? LeftChild { get; set; }
		public Node? RightChild { get; set; }
		public Node? Parent{ get; set; }

		public Node(string value, Node? parent = null)
		{
			var firstOperator = GetFirstOperator(value);

			Parent = parent;

			if (firstOperator is null)
            {
				Value = string.IsNullOrEmpty(value) ? Calculator._operators.Single(op => op.oper == Char.Parse(this.Parent!.Value)).eVal.ToString() : value ;
            }
            else
            {
				var indexFOp = value.IndexOf(firstOperator.Value);

				Value = firstOperator!.Value.ToString();
				LeftChild = new Node(value.Substring(0, indexFOp).Trim(), this);
				RightChild = new Node(value.Substring(indexFOp + 1, value.Length - indexFOp - 1).Trim(), this);
				
			}
		}

		private static char? GetFirstOperator(string input)
        {
			var op = input.FirstOrDefault(c => Calculator._operators.Select(s => s.oper).Contains(c), '§');

			if (op is '§')
            {
				return null;
            }

			var opIndex = input.IndexOf(op);
			var opType = GetOperatorType(op, opIndex, input);

			return opType is OperatorTypes.NegativeNumberMinus ?
					GetFirstOperator(input[(opIndex + 1)..]) : op;
		}

		private static OperatorTypes GetOperatorType(char op, int idx, string input)
        {
			return op switch
			{
				'+' => decimal.TryParse(input.ElementAt(idx + 1).ToString(), out var _) ?
						OperatorTypes.PositiveNumberPlus : OperatorTypes.AdditionPlus,//OperatorTypes.Plus,
				'*' => OperatorTypes.Multiply,
				'/' => OperatorTypes.Division,
				'-' => decimal.TryParse(input.ElementAt(idx + 1).ToString(), out var _) ?
						OperatorTypes.NegativeNumberMinus : OperatorTypes.DifferenceMinus,
				_ => OperatorTypes.Undefined
			};
        }

		public bool IsOperator => Char.TryParse(Value, out var _) ? Calculator._operators.Select(s => s.oper).Contains(Convert.ToChar(Value)) : false;
		public bool IsDecimal => Decimal.TryParse(Value, out var _);
		public decimal? GetDecimal => this.IsDecimal ? Decimal.Parse(Value) : null;

	}
}

