namespace ABC.Sales
{
    public class SalesStatsRepoitory
    {
        private readonly ISalesMonthPerformance _performanceService;

        public SalesStatsRepoitory(ISalesMonthPerformance performanceService)
        {
            _performanceService = performanceService;
        }

        public MonthStatistic[] GetStatsData()
        {
            var model = new[]
            {
                new MonthStatistic() {Month = "January", Revenue = 132, Target = 0.71},
                new MonthStatistic() {Month = "February", Revenue = 110, Target = 0.65},
                new MonthStatistic() {Month = "March", Revenue = 191, Target = 1.12},
                new MonthStatistic() {Month = "April", Revenue = 72, Target = 0.51},
                new MonthStatistic() {Month = "May", Revenue = 75, Target = 0.61},
                new MonthStatistic() {Month = "June", Revenue = 98, Target = 0.75},
                new MonthStatistic() {Month = "July", Revenue = 135, Target = 0.91},
                new MonthStatistic() {Month = "August", Revenue = 172, Target = 1.51},
            };

            foreach (var stat in model)
            {
                stat.Performance = _performanceService.GetPerformance(stat.Month);
            }

            return model;
        }
    }
}