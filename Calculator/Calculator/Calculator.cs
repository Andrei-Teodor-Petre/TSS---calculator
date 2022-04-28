namespace Calculator;


public class Calculator
{
    public static Operator[] _operators = new Operator[] { 
        new ('+', neutralValue: 0m, rank: 0),
        new ('-', neutralValue: 0m, rank: 0),
        new ('*', neutralValue: 1m, rank: 1),
        new ('/', neutralValue: 1m, rank: 1),
        new ('p', neutralValue: null, rank: 2),
        new ('r', neutralValue: null, rank: 2),
        new ('@', neutralValue: null, rank: 1),
    };

    public static decimal? Eval(string input, Node? parent = null)
    {
        var root = new Node(ReduceParens(input), parent);

        return RunTree(root);
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

    private static decimal? Multiply(decimal? t1, decimal? t2) => t1!.Value * t2!.Value;

    public static decimal? Sum(decimal? t1, decimal? t2) => t1!.Value + t2!.Value;
    private static decimal? Minus(decimal? t1, decimal? t2) => t1!.Value - t2!.Value;

    private static decimal? Division(decimal? t1, decimal? t2) => t1!.Value / t2!.Value;

    private static decimal? Pow(decimal? t1, decimal? t2) => (decimal?)Math.Pow((double)t1!.Value, (double)t2!.Value);

    private static decimal? Root(decimal? t1, decimal? t2) => (decimal?)Math.Pow((double)t1!.Value, 1 / (double)t2!.Value);

    private static decimal? Calculate(decimal? t1, decimal? t2, Func<decimal?, decimal?, decimal?> op) => op(t1, t2);

    private static decimal? RunTree(Node? node)
    {
        _ = node!.Value;// ?? throw new ArgumentNullException(nameof(node!));

        if(node.IsOperator)
        {
            return node.Value switch
            {
                "+" => Calculate(RunTree(node.Operand1), RunTree(node.Operand2), Sum),
                "*" => Calculate(RunTree(node.Operand1), RunTree(node.Operand2), Multiply),
                "/" => Calculate(RunTree(node.Operand1), RunTree(node.Operand2), Division),
                "-" => Calculate(RunTree(node.Operand1), RunTree(node.Operand2), Minus),
                "p" => Calculate(RunTree(node.Operand1), RunTree(node.Operand2), Pow),
                "r" => Calculate(RunTree(node.Operand1), RunTree(node.Operand2), Root),
                _ => null
            };
        }
        else
        {
            return node.GetDecimal;
        }
    }
}
