public class ProcessHandler
    {
        public static string ExePath { get; private set; } = "";
        public static ProgressReportModel ProgressReport { get; set; } = new ProgressReportModel();
        public static int RunCountToGo { get; set; } = int.MaxValue;
        public static ConsoleOutRedirector? consoleOutRedirector { get; set; }

        public static async void RunProgram(string exePath, List<string> batchList, IProgress<ProgressReportModel> progress)
        {
            ExePath = exePath; 
            ProgressReport.TotalRunCount = batchList.Count;
            RunCountToGo = ProgressReport.TotalRunCount;

            foreach(string files in batchList)
            {
                string[] filenames = files.Split(',');
                string cmd = $"ECEXP \"{filenames[0]}\" \"{filenames[1]}\" {Environment.NewLine}";
                RunAsyncProgress(cmd, progress);
                await Task.Run(() => Thread.Sleep(1000));
            }
        }
        private static async void RunAsyncProgress(string cmd, IProgress<ProgressReportModel> progress)
        {
            if (consoleOutRedirector == null)
            {
                consoleOutRedirector = new ConsoleOutRedirector();
            }
            consoleOutRedirector.ShowConsole();

            ProcessToMonitor PTM = new ProcessToMonitor();

            Process myProgram = new Process();
            myProgram.StartInfo.FileName = ExePath;
            myProgram.StartInfo.WorkingDirectory = Path.GetDirectoryName(ExePath);
            myProgram.StartInfo.UseShellExecute = false;
            myProgram.StartInfo.RedirectStandardInput = true;
            myProgram.StartInfo.RedirectStandardOutput = true;
            myProgram.StartInfo.RedirectStandardError = true;
            myProgram.StartInfo.Arguments = cmd;
            myProgram.Start();

            PTM.PID = myProgram.Id;
            PTM.CommandString = cmd;
            ProgressReport.PTMList.Add(PTM);

            Console.WriteLine($"myProgram.PID: {PTM.PID} Started at {DateTime.Now.ToString("H:mm:ss")}");

            await Task.Run(() => myProgram.WaitForExit()); // UI가 잠기지 않고, myProgram instance도 1초 간격으로 연속해서 생성되므로 병렬 실행의 효과
            DateTime exitTime = myProgram.ExitTime;
            PTM.EndTime = exitTime;

            ProgressReport.FinishedRunCount = ProgressReport.TotalRunCount - RunCountToGo + 1;
            RunCountToGo -= 1;

            string result_msg = myProgram.StandardOutput.ReadToEnd().Replace("\r\n", "");
            PTM.myProgramResultMsg = result_msg;
            string output = $"{ProgressReport.FinishedRunCount}/{ProgressReport.TotalRunCount} {PTM.myProgramResultMsg}";
            Console.WriteLine($"{output} {exitTime.ToString("yyyy-MM-dd H:mm:ss")}");

            if (progress != null)
            {
                progress.Report(ProgressReport);
            }

            if (RunCountToGo == 0)
            {
                Console.WriteLine($"End of Batch at [{DateTime.Now.ToString("H:mm:ss")}]");
                Console.WriteLine();
                ProgressReport = new ProgressReportModel();

                consoleOutRedirector.ShowConsole();
                Thread.Sleep(1000);
                consoleOutRedirector.ShowConsole();
            }
        }
    }
