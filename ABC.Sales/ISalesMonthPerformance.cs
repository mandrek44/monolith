namespace ABC.Sales
{
    public interface ISalesMonthPerformance
    {
        double? GetPerformance(int monthIndex);
    }

    public class DefaultSalesMonthPerformance : ISalesMonthPerformance
    {
        public double? GetPerformance(int monthIndex)
        {
            return null;
        }
    }
}