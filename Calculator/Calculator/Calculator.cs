namespace Calculator;


public class Calculator
{
    public Calculator()
    {
    }

    public static (char oper,decimal eVal)[] _operators = new [] { new ('+', 0m),
        new ValueTuple<char, decimal>('-', 0m),
        new ValueTuple<char, decimal>('*', 1m),
        new ValueTuple<char, decimal>('/', 1m)
    };

    public decimal? Eval(string input)
    {
        var root = new Node(input);

        return RunTree(root);
    }

    private decimal? Multiply(decimal? t1, decimal? t2) => t1 * t2;

    private decimal? Sum(decimal? t1, decimal? t2) => t1 + t2;

    private decimal? Minus(decimal? t1, decimal? t2) => t1 - t2;

    private decimal? Division(decimal? t1, decimal? t2) => t1 / t2;

    private decimal? Calculate(decimal? t1, decimal? t2, Func<decimal?, decimal?, decimal?> op) => op(t1, t2);

    private decimal? RunTree(Node root)
    {

        if(root.IsOperator)
        {

            return root.Value switch
            {
                "+" =>  Calculate(RunTree(root.LeftChild), RunTree(root.RightChild), Sum),
                "*" => Calculate(RunTree(root.LeftChild), RunTree(root.RightChild), Multiply),
                "/" => Calculate(RunTree(root.LeftChild), RunTree(root.RightChild), Division),
                "-" => Calculate(RunTree(root.LeftChild), RunTree(root.RightChild), Minus),
                _ => null
            };
        }
        else
        {
            return root.GetDecimal;
        }

    }
}
