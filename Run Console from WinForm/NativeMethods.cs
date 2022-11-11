public class NativeMethods
    {
        //internal const int MY_CODE_PAGE = 437;
        internal const int MY_CODE_PAGE = 1252;
        internal const uint GENERIC_WRITE = 0x40000000;
        internal const uint FILE_SHARE_WRITE = 0x2;
        internal const uint OPEN_EXISTING = 0x3;

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern IntPtr CreateFile(
            string lpFileName,
            uint dwDesiredAccess,
            uint dwShareMode, uint
            lpSecurityAttributes,
            uint dwCreationDisposition,
            uint dwFlagsAndAttributes,
            uint hTemplateFile);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool AllocConsole();

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool FreeConsole();

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        internal static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        internal static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        internal static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        internal static extern bool AllowSetForegroundWindow(int dwProcessId);

        /* Windows Native ShowWindow function
        * https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-showwindow
        */
        public const int SW_HIDE = 0;          //Hides the window and activates another window.
        public const int SW_SHOW = 5;          //Activates the window and displays it in its current size and position.
        public const int SW_RESTORE = 9;       //Activates and displays the window.If the window is minimized or maximized, the system restores it to its original size and position.An application should specify this flag when restoring a minimized window.
        public const int SW_SHOWNORMAL = 1;    //Activates and displays a window. If the window is minimized or maximized, the system restores it to its original size and position.
        public const int SW_SHOWMINIMIZED = 2; //Activates the window and displays it as a minimized window.
        public const int SW_SHOWMAXIMIZED = 3; //Activates the window and displays it as a maximized window.
    }
    public enum StdHandle
    {
        Stdin = -10,
        Stdout = -11,
        Stderr = -12
    }
