using NCalc;

namespace Domain.Entities
{
    public static class Calculator
    {
        public static decimal Calc(string expression)
        {
            Expression e = new Expression(expression);
            return Convert.ToDecimal(Math.Round((decimal)e.Evaluate(), 2));
        }

        public static double CalcTotalWithPercent(double sum, int percent)
        {
            return sum + percent * 0.01 * sum;
        }
    }
}