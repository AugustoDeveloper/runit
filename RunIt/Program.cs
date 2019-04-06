using System;
using RunIt.Infra.Interpreter;

namespace RunIt
{
    public class Program
    {

        public static int Main(string[] args)
        {           
            try
            {
                Console.WriteLine("To authenticate we make console window focus, so, don't try change focus.");
                new InterpreterCommand(args).Execute();
            } catch(Exception ex)
            {
                Console.Write(ex.Message);
                return -1;
            }

            return 0;
        }
    }
}