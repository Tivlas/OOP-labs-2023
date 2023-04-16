using NCalc;

namespace Domain.Entities
{
    public static class Calculator
    {
        public static decimal Calc(string expression)
        {
            Expression e = new Expression(expression);
            var value = Convert.ToDecimal(e.Evaluate());
            return Math.Round(value, 2);
        }
    }
}