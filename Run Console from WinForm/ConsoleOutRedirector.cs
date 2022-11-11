public class ConsoleOutRedirector : IDisposable
    {
        public ConsoleOutRedirector() 
        {
            NativeMethods.AllocConsole();
            InitConsole();
        }
        private void InitConsole()
        {
            IntPtr stdHandle = NativeMethods.CreateFile(
                "CONOUT$",
                NativeMethods.GENERIC_WRITE,
                NativeMethods.FILE_SHARE_WRITE,
                0, NativeMethods.OPEN_EXISTING, 0, 0);

            SafeFileHandle safeFileHandle = new SafeFileHandle(stdHandle, true);
            FileStream fileStream = new FileStream(safeFileHandle, FileAccess.Write);
            //Encoding encoding = Encoding.GetEncoding(NativeMethods.MY_CODE_PAGE); // <= For .Net Framework 4.8 or before
            Encoding encoding = Encoding.Default;  // <- For .Net6.0 or later
            StreamWriter standardOutput = new StreamWriter(fileStream, encoding);
            standardOutput.AutoFlush = true;
            Console.SetOut(standardOutput);
        }
        public void Dispose()
        {
            NativeMethods.FreeConsole();
        }
        public void CloseConsole()
        {
            NativeMethods.ShowWindow(NativeMethods.GetConsoleWindow(), NativeMethods.SW_HIDE);
        }
        public void ShowConsole()
        {
            IntPtr hWnd = NativeMethods.GetConsoleWindow();
            NativeMethods.ShowWindow(hWnd, NativeMethods.SW_SHOWNORMAL);
        }
    }
