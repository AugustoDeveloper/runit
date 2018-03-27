using System;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Net;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using System.Runtime.InteropServices;
using RunIt.Infra.Interpreter;
using RunIt.Infra.Repository.LiteDb;
using RunIt.Entity;

namespace RunIt
{
    class Program
    {

        const string runAsProgram = @"C:\Windows\System32\runas.exe";
        const string sqlManagementProgram = @"C:\Program Files (x86)\Microsoft SQL Server\140\Tools\Binn\ManagementStudio\Ssms.exe";
        const string password = "'uV`w4ts=K";

        static void Main(string[] args)
        {
            var newArgs = string.Join(" ", args);
            if (newArgs.Contains("load-database"))
                Load();
            
            new InterpreterCommand(newArgs).Execute();
            Console.ReadKey();

        }

        static void Load()
        {
            var creRepo = new CredentialLiteDbRepository();
            if (creRepo.GetByName("dev") == null)
                creRepo.Add(new Credential { Domain = "buy4dev", Name = "dev", Password = "'uV`w4ts=K", Username = "augusto.mesquita" });
            if (creRepo.GetByName("np") == null)
                creRepo.Add(new Credential { Domain = "buy4np", Name = "np", Password = "!G4?BZ^gr9", Username = "augusto.mesquita" });
            var appRepo = new ApplicationLiteDbRepository();
            if (appRepo.GetByAliasOrName("sqlm") == null)
                new ApplicationLiteDbRepository().Add(new Entity.Application { Name = "Sql Management Studio", Alias = "sqlm", FileName = "C:\\Program Files (x86)\\Microsoft SQL Server\\140\\Tools\\Binn\\ManagementStudio\\Ssms.exe"});
        }

        static void ExecuteWindowsRunAs()
        {
            var runAsCommand = $"/netonly /user:buy4dev\\augusto.mesquita \"{sqlManagementProgram}\"";

            var sqlManagementProcess = new ProcessStartInfo
            /*{
                CreateNoWindow = false,
                FileName = "notepad.exe",
                UseShellExecute = true
            };*/
            {
                CreateNoWindow = false,
                FileName = runAsProgram,
                UseShellExecute = true,
                Arguments = runAsCommand,

            };



            var process = new Process
            {
                StartInfo = sqlManagementProcess,
            };
            
            process.Start();

            Thread.Sleep(1 * 1000);

            var handle = process.MainWindowHandle;

            SetForegroundWindow(handle);
            password.ToCharArray().ToList().ForEach(c => GetPassChar(c).ToList().ForEach(s => SendKeys.SendWait(s)));
            SendKeys.SendWait("{ENTER}");
            SendKeys.Flush();


            process.WaitForExit();
            Console.WriteLine("\nFinalizado...");            
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