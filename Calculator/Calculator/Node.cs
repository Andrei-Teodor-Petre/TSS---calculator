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
				//0 1 2 3 

			}
            else
            {
				Value = value;
            }


		}

		public bool IsOperator => Calculator._operators.Contains(Convert.ToChar(Value));
		public bool IsDecimal => Decimal.TryParse(Value, out var _);

	}
}

