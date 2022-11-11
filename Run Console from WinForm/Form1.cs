...
private void btnRunBatch_Click(object sender, EventArgs e)
        {
            if (BatchResults != null)
            {
                BatchResults = null;
            }

            string myProgramExePath = txtExePath.Text.Trim();

            if (!File.Exists(myProgramExePath))
            {
                MessageBox.Show("myPrograme 실행파일 경로를 확인하세요.", "실행파일 경로 오류");
                return;
            }

            List<string>? BatchListToRun = BatchListToShow;
            if (BatchListToRun is null) return;

            progress = new Progress<ProgressReportModel>();
            progress.ProgressChanged += ReportProgress;  // Attach Event Handler

            string s_time = DateTime.Now.ToString("H:mm:ss");
            lblStartTime.Text = s_time;
            lblEndTime.Text = "H:mm:ss";

            ProcessHandler.RunProgram(ExePath, BatchListToRun, progress);

            ReportProgress(this, new ProgressReportModel() { FinishedRunCount = 0, TotalRunCount = BatchListToRun.Count });
        }
