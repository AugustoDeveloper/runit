using System;
using System.Runtime.InteropServices;

namespace RunIt.Infra.Helper
{
    public static class Util
    {
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        public static string[] GetConsoleChar(char passChar)
        {
            switch (passChar)
            {
                case '\'':
                    return new string[] { "'", "'", "{BS}" };
                case '`':
                    return new string[] { "`", "`", "{BS}" };
                default:
                    return new string[] { passChar.ToString() };
            }
        }
    }
}
