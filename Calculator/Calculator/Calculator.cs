namespace Calculator;


public class Calculator
{
    public Calculator()
    {
    }

    public static Operator[] _operators = new Operator[] { new ('+', 0m, 0),
        new ('-', 0m, 0),
        new ('*', 1m, 1),
        new ('/', 1m, 1)
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
                "+" => Calculate(RunTree(root.LeftChild), RunTree(root.RightChild), Sum),
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
