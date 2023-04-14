using NCalc;

namespace Domain.Entities
{
    public static class Calculator
    {
        public static double Calc(string expression)
        {
            Expression e = new Expression(expression);
            return Convert.ToDouble(Math.Round((double)e.Evaluate(), 2));
        }

        public static double CalcTotalWithPercent(double sum, int percent)
        {
            return sum + percent * 0.01 * sum;
        }
    }
}