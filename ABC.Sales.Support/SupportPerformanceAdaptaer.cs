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

        public double? GetPerformance(int monthIndex)
        {
            return _supportPerformanceMonitor.GetCallsCountForMonth(monthIndex);
        }
    }
}
