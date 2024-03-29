﻿namespace Calculator
{
    public class Operator
    {

        public char Value { get; set; }
        public decimal? NeutralValue { get; set; }
        public int Rank { get; set; }

        public Operator(char value, decimal? neutralValue, int rank)
        {
            Value = value;
            NeutralValue = neutralValue;
            Rank = rank;
        }
    }
}
