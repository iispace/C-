public class ProcessToMonitor
    {
        public int PID { get; set; }
        public string CommandString { get; set; } = string.Empty;
        public string myProgramResultMsg { get; set; } = string.Empty;
        public DateTime EndTime { get; set; }
    }
