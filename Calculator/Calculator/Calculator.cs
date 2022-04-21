namespace Calculator;


public class Calculator
{
    public Calculator()
    {
    }

    public static char[] _operators = new char[] { '+', '-', '*', '/' };

    public decimal? Eval(string input)
    {

        var root = new Node(input);

        Console.WriteLine('K');

        return RunTree(root);
    }

    private decimal? Multiply(decimal? t1, decimal? t2) => t1 * t2;

    private decimal? Sum(decimal? t1, decimal? t2) => t1 + t2;

    private decimal? Calculate(decimal? t1, decimal? t2, Func<decimal?, decimal?, decimal?> op) => op(t1, t2);

    private decimal? RunTree(Node root)
    {

        if(root.IsOperator)
        {

            return root.Value switch
            {
                "+" =>  Calculate(RunTree(root.LeftChild), RunTree(root.RightChild), Sum),
                "*" => Calculate(RunTree(root.LeftChild), RunTree(root.RightChild), Multiply),

                _ => null
            };
        }
        else
        {
            return root.GetDecimal;
        }

    }




}
