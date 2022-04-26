namespace Calculator;


public class Calculator
{
    public Calculator()
    {
    }

    public static Operator[] _operators = new Operator[] { 
        new ('+', neutralValue: 0m, rank: 0),
        new ('-', neutralValue: 0m, rank: 0),
        new ('*', neutralValue: 1m, rank: 1),
        new ('/', neutralValue: 1m, rank: 1),
        new ('p', neutralValue: null, rank: 2),
        new ('r', neutralValue: null, rank: 2)
    };

    public static decimal? Eval(string input, Node? parent = null)
    {
        var root = new Node(input, parent);

        return RunTree(root);
    }

    private static decimal? Multiply(decimal? t1, decimal? t2) => t1 * t2;

    private static decimal? Sum(decimal? t1, decimal? t2) => t1 + t2;

    private static decimal? Minus(decimal? t1, decimal? t2) => t1 - t2;

    private static decimal? Division(decimal? t1, decimal? t2) => t1 / t2;

    private static decimal? Pow(decimal? t1, decimal? t2) => (decimal?)Math.Pow((double)t1!.Value, (double)t2!.Value);

    private static decimal? Root(decimal? t1, decimal? t2) => (decimal?)Math.Pow((double)t1!.Value, 1 / (double)t2!.Value);

    private static decimal? Calculate(decimal? t1, decimal? t2, Func<decimal?, decimal?, decimal?> op) => op(t1, t2);

    private static decimal? RunTree(Node? node)
    {
        _ = node ?? throw new ArgumentNullException(nameof(node));

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
