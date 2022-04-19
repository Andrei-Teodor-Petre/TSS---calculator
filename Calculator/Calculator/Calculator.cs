namespace Calculator;

public class Calculator
{
    public Calculator()
    {
    }

    private char[] _operators = new char[] { '+', '-', '*', '/' };

    public decimal? Eval(string input)
    {
        (var tokens, var operators) = Tokenize(input);

        if (tokens is null)
        {
            return null;
        }

        decimal? result = null;
        try
        {
            decimal? map1(string x) => decimal.Parse(x);
            decimal? map2(string x) => decimal.Parse(x);

            result = operators!.Single() switch
            {
                "+" => decimal.Parse(tokens!.Aggregate((x, y) => Calculate(x, y, map1, map2, Sum))),

                "*" => decimal.Parse(tokens!.Aggregate((x, y) => Calculate(x, y, map1, map2, Multiply))),

                _ => null
            };
        }
        catch (Exception)
        {
            return null;
        }

        return result;
    }

    private decimal? Multiply(decimal? t1, decimal? t2) => t1 * t2;

    private decimal? Sum(decimal? t1, decimal? t2) => t1 + t2;

    private string Calculate<T1, T2, TResult>(string t1, string t2, Func<string, T1?> map1, Func<string, T2?> map2, Func<T1, T2, TResult> op)
    {
        var t1_mapepd = map1(t1) ?? throw new ArgumentException();
        var t2_mapped = map2(t2) ?? throw new ArgumentException();

        var result = op(t1_mapepd, t2_mapped);

        return result.ToString();
    }

    private (List<string>? tokens, List<string>? operators) Tokenize(string input)
    {
        var tokens = input.Split(_operators, StringSplitOptions.TrimEntries).ToList();

        var operators = input.Split(tokens.ToArray(), StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToList();

        foreach (var tok in tokens)
        {
            if (!IsValidToken(tok))
            {
                return (null, null);
            }
        }

        return (tokens, operators);
    }

    private static bool IsValidToken(string tok)
    {
        return decimal.TryParse(tok, out _);
    }
}
