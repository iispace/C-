public class ProgressReportModel
    {
        public int FinishedRunCount { get; set; } = 0;
        public int TotalRunCount { get; set; }  = int.MaxValue;
        public List<ProcessToMonitor> PTMList { get; set; }  = new List<ProcessToMonitor>();
    }
