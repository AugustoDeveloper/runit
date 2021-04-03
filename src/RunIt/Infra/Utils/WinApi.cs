using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Logging;
using Microsoft.Win32.SafeHandles;


namespace RunIt.Infra.Utils
{
    static public class WinApi
    {
        public enum LogonFlags
        {
            WithProfile = 1,
            NetCredentialsOnly
        }

        public enum CreationFlags
        {
            DefaultErrorMode = 0x04000000,
            NewConsole = 0x00000010,
            NewProcessGroup = 0x00000200,
            SeparateWOWVDM = 0x00000800,
            Suspended = 0x00000004,
            UnicodeEnvironment = 0x00000400,
            ExtendedStartupInfoPresent = 0x00080000
        }

        public struct ProcessInformation
        {
            public IntPtr hProcess;
            public IntPtr hThread;
            public int ProcessId;
            public int ThreadId;
        }

        public struct StartUpInformation
        {
            public int cb;
            public string lpReserved;
            public string lpDesktop;
            public string lpTitle;
            public uint dwX;
            public uint dwY;
            public uint dwXSize;
            public uint dwYSize;
            public uint dwXCountChars;
            public uint dwYCountChars;
            public uint dwFillAttribute;
            public uint dwFlags;
            public short wShowWindow;
            public short cbReserved2;
            public IntPtr lpReserved2;
            public IntPtr hStdInput;
            public IntPtr hStdOutput;
            public IntPtr hStdError;
        }

        static public void RunAs(string domain, string username, string password, string filename, ILogger logger)
        {
            using (var accessToken = WinApi.GetUserAccessToken(domain, username, password))
            {
                logger.LogTrace("Executing ..."+filename);
                var startupInfo = new StartUpInformation();
                if (!WinApi.CreateProcessWithTokenW(
                        accessToken.DangerousGetHandle(), 
                        LogonFlags.NetCredentialsOnly, 
                        null, 
                        filename, 
                        CreationFlags.DefaultErrorMode,
                        IntPtr.Zero, 
                        AppContext.BaseDirectory, 
                        ref startupInfo, 
                        out var processInfo))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
        }

        static public SafeAccessTokenHandle GetUserAccessToken(string domain, string username, string password)
        {
            const int LOGON32_PROVIDER_DEFAULT = 0;
            const int LOGON32_LOGON_NETONLY = 2;
            const int LOGON32_LOGON_NEW_CREDENTIALS = 9;

            bool isLogonSuccessful = LogonUser(
                username, 
                domain, 
                password, 
                LOGON32_LOGON_NEW_CREDENTIALS, 
                LOGON32_PROVIDER_DEFAULT, 
                out var safeAccessTokenHandle);
            if (!isLogonSuccessful)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            return safeAccessTokenHandle;
        }

        [DllImport("advapi32", SetLastError = true, CharSet = CharSet.Unicode)]
        static public extern bool CreateProcessWithTokenW(
            IntPtr hToken, 
            LogonFlags dwLogonFlags, 
            string lpApplicationName, 
            string lpCommandLine, 
            CreationFlags dwCreationFlags, 
            IntPtr lpEnvironment, 
            string lpCurrentDirectory, 
            [In] ref StartUpInformation lpStartupInfo, 
            out ProcessInformation lpProcessInformation);

        [DllImport("ADVAPI32.DLL", SetLastError = true, CharSet = CharSet.Unicode)]
        static public extern bool LogonUser(
            string lpszUsername,
            string lpszDomain,
            string lpszPassword,
            int dwLogonType,
            int dwLogonProvider,
            out SafeAccessTokenHandle phToken);
    }
}