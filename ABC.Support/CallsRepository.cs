namespace ABC.Support
{
    public class CallsStatistic
    {
        public string Month { get; set; }

        public int CallsCount { get; set; }

        public double AverageSatisfaction { get; set; }
    }

    public class CallsRepository
    {
        public CallsStatistic[] GetCallsStatistics()
        {
            return new[]
            {
                new CallsStatistic() {Month = "January", CallsCount = 145, AverageSatisfaction = 0.73},
                new CallsStatistic() {Month = "February", CallsCount = 116, AverageSatisfaction = 0.71},
                new CallsStatistic() {Month = "March", CallsCount = 182, AverageSatisfaction = 0.58},
                new CallsStatistic() {Month = "April", CallsCount = 221, AverageSatisfaction = 0.45},
                new CallsStatistic() {Month = "May", CallsCount = 134, AverageSatisfaction = 0.72},
                new CallsStatistic() {Month = "June", CallsCount = 90, AverageSatisfaction = 0.81},
                new CallsStatistic() {Month = "July", CallsCount = 101, AverageSatisfaction = 0.75},
            };
        }
    }
}