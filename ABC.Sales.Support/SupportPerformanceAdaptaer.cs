using ABC.Support;

namespace ABC.Sales.Support
{
    public class SupportPerformanceAdaptaer : ISalesMonthPerformance
    {
        private readonly SupportPerformanceMonitor _supportPerformanceMonitor;

        public SupportPerformanceAdaptaer(SupportPerformanceMonitor supportPerformanceMonitor)
        {
            _supportPerformanceMonitor = supportPerformanceMonitor;
        }

        public double? GetPerformance(string month)
        {
            return _supportPerformanceMonitor.GetCallsCountForMonth(month);
        }
    }
}
