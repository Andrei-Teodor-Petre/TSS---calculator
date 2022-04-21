using System;
namespace Calculator
{
	public class Node
	{
		public string Value { get; set; }
		public Node LeftChild { get; set; }
		public Node RightChild { get; set; }

		public Node(string value)
		{
			
			var firstOperator = value.FirstOrDefault(c => Calculator._operators.Contains(c), '§');

			if (firstOperator != '§')
			{
				var indexFOp = value.IndexOf(firstOperator);

				LeftChild = new Node(value.Substring(0, indexFOp).Trim());
				Value = firstOperator.ToString();
				RightChild = new Node(value.Substring(indexFOp + 1, value.Length - indexFOp - 1).Trim());

			}
            else
            {
				Value = value;
            }


		}

		public bool IsOperator => Char.TryParse(Value, out var _) ? Calculator._operators.Contains(Convert.ToChar(Value)) : false;
		public bool IsDecimal => Decimal.TryParse(Value, out var _);
		public decimal? GetDecimal => this.IsDecimal ? Decimal.Parse(Value) : null;

	}
}

