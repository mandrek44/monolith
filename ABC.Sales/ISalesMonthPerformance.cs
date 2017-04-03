namespace ABC.Sales
{
    public interface ISalesMonthPerformance
    {
        double? GetPerformance(string month);
    }

    public class DefaultSalesMonthPerformance : ISalesMonthPerformance
    {
        public double? GetPerformance(string month)
        {
            return null;
        }
    }
}