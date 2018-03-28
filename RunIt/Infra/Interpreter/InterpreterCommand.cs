using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using RunIt.Infra.Configuration;
using Utility.CommandLine;

namespace RunIt.Infra.Interpreter
{
    public class InterpreterCommand : ICommand
    {
        private EnviromentConfigurationSection _section;

        [Argument('e', "exec")]
        public static string ApplicationExecute { get; set; }

        [Operands]
        public static List<string> Operands { get; set; }

        public InterpreterCommand(string[] args)
        {
            Validate(args);
            var newArgs = string.Join(" ", args);

            _section = EnviromentConfigurationSection.Get();
            var arguments = Arguments.Parse(newArgs);
            
            Arguments.Populate(GetType(), arguments);
        }

        private void Validate(string[] args)
        {
            if (args == null) throw new ArgumentNullException("args", "O parametro de argumento deve estar preenchido com valores válidos");
            if (args.Length != 3) throw new ArgumentOutOfRangeException("Para execução correta deve ser passado três parametros.");
            if (args[0] != "-e") throw new ArgumentException("O primeiro argumento deve ser -e, seguido da aplicacao e ambiente de credenciamento");
            if (args[2].Contains("-") || args[1].Contains("-")) throw new ArgumentException("O segundo/terceiro argumento não deve conter hifen");
        }

        public void Execute()
        {
            ExecuteApplication(Operands[0]);
        }

        private void ExecuteApplication(string credentialName)
        {
            const string runAsProgram = @"C:\Windows\System32\runas.exe";

            var credential = _section.Credentials[credentialName];
            var application = _section.Applications[ApplicationExecute];

            var runAsCommand = $"/netonly /user:{credential.Domain}\\{credential.Username} \"{application.Filename}\"";
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
            credential.Password.ToCharArray().ToList().ForEach(c => GetPassChar(c).ToList().ForEach(SendKeys.SendWait));
            SendKeys.SendWait("{ENTER}");
            SendKeys.Flush();


            process.WaitForExit();
        }

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        private static string[] GetPassChar(char passChar)
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
