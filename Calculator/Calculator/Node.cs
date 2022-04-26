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
		public Node? Operand1 { get; set; }
		public Node? Operand2 { get; set; }
		public Node? Operator { get; set; }

		public Node(string value, Node? parent = null)
		{
			value = ReduceParens(value);

			(var firstOperator, var indexFOp) = GetLowestRankOperator(value);

			Operator = parent;

			if (firstOperator is null)
			{
				Value = string.IsNullOrEmpty(value) ?
					Calculator._operators.Single(op => op.Value == Char.Parse(this.Operator!.Value)).NeutralValue!.ToString()!
					: value;
			}
			else
			{
				Value = firstOperator!.Value.ToString();

				var expr1 = value.Substring(0, indexFOp!.Value).Trim();
				var expr2 = value.Substring(indexFOp!.Value + 1, value.Length - indexFOp!.Value - 1).Trim();

				Operand1 = new Node(Calculator.Eval(expr1, this)?.ToString()!, this);
				Operand2 = new Node(Calculator.Eval(expr2, this)?.ToString()!, this);
			}
		}

		private static string ReduceParens(string input)
        {
			(var openParen, var closeParen) = GetParenIndices(input);

			if (openParen.HasValue && closeParen.HasValue)
            {
				var length = (closeParen.Value - openParen.Value) - 1;

				var evaluatedInput = Calculator.Eval(input.Substring(openParen.Value + 1, length));

				var result = $"{input.Substring(0, openParen.Value)} {evaluatedInput} {input.Substring(closeParen.Value + 1)}";

				return ReduceParens(result);
            }

			return input;
		}

        private static (int?, int?) GetParenIndices(string value)
        {
			var st = new Stack<int>();

			int idx = 0;

            while (idx < value.Length)
            {
				if (value.ElementAt(idx) == '(')
                {
					st.Push(idx);
                }

				if (value.ElementAt(idx) == ')')
                {
					var openIndex = st.Pop();

					if (!st.Any())
					{
						return (openIndex, idx);
					}
				}

				idx++;
            }

			return (null, null);
        }

        private static (char? op, int? idx) GetLowestRankOperator(string input, int offset = 0)
		{
			var op = input
				.Where(chr => Calculator._operators.Select(op => op.Value).Contains(chr))
				.Select(chr => Calculator._operators.Single(op => op.Value == chr))
				.MinBy(op => op.Rank);

			if (op is null)
			{
				return (null, null);
			}

			var opIndex = input.IndexOf(op.Value);
			var opType = GetOperatorType(op.Value, opIndex, input);

			return opType is OperatorTypes.NegativeNumberMinus ?
					GetLowestRankOperator(input[(opIndex + 1)..], opIndex + 1) : (op.Value, opIndex + offset);
		}

		private static OperatorTypes GetOperatorType(char op, int idx, string input)
        {
			return op switch
			{
				'+' => decimal.TryParse(input.ElementAt(idx + 1).ToString(), out var _) ?
						OperatorTypes.PositiveNumberPlus : OperatorTypes.AdditionPlus,
				'*' => OperatorTypes.Multiply,
				'/' => OperatorTypes.Division,
				'-' => decimal.TryParse(input.ElementAt(idx + 1).ToString(), out var _) ?
						OperatorTypes.NegativeNumberMinus : OperatorTypes.DifferenceMinus,
				_ => OperatorTypes.Undefined
			};
        }

		public bool IsOperator => Char.TryParse(Value, out var _) ? Calculator._operators.Select(s => s.Value).Contains(Convert.ToChar(Value)) : false;
		public bool IsDecimal => Decimal.TryParse(Value, out var _);
		public decimal? GetDecimal => this.IsDecimal ? Decimal.Parse(Value) : null;

	}
}

