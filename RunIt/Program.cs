using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Linq;
using System.Threading;
using System.Runtime.InteropServices;
using RunIt.Infra.Interpreter;

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
            new InterpreterCommand(newArgs).Execute();
            Console.ReadKey();

        }
    }
}