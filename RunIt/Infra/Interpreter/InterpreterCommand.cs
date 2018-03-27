using RunIt.Infra.Repository.LiteDb;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Utility.CommandLine;

namespace RunIt.Infra.Interpreter
{
    public class InterpreterCommand : ICommand
    {
        
        [Argument('e', "exec")]
        public static string ApplicationExecute { get; set; }

        public InterpreterCommand(string args)
        {
            var arguments = Arguments.Parse(args);
            Arguments.Populate(this.GetType(), arguments);
        }

        [Operands]
        public static List<string> Operands { get; set; }

        public void Execute()
        {
            ExecuteApplication(Operands[0]);
        }

        private void ExecuteApplication(string credentialName)
        {
            const string runAsProgram = @"C:\Windows\System32\runas.exe";
            var repository = new CredentialLiteDbRepository();
            var appRepository = new ApplicationLiteDbRepository();
            var credential = repository.GetByName(credentialName);
            var application = appRepository.GetByAliasOrName(ApplicationExecute);

            var runAsCommand = $"/netonly /user:{credential.Domain}\\{credential.Username} \"{application.FileName}\"";
            var infoProcess = new ProcessStartInfo            
            {
                CreateNoWindow = false,
                FileName = runAsProgram,
                UseShellExecute = true,
                Arguments = runAsCommand,

            };

            var process = new Process
            {
                StartInfo = infoProcess,
            };

            process.Start();

            Thread.Sleep(1 * 1000);

            var handle = process.MainWindowHandle;

            SetForegroundWindow(handle);
            credential.Password.ToCharArray().ToList().ForEach(c => GetPassChar(c).ToList().ForEach(s => SendKeys.SendWait(s)));
            SendKeys.SendWait("{ENTER}");
            SendKeys.Flush();


            process.WaitForExit();
        }

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        static string[] GetPassChar(char passChar)
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
