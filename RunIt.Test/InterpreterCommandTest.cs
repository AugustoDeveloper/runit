using RunIt.Infra.Interpreter;
using Utility.CommandLine;
using Xunit;

namespace RunIt.Test
{
    public class InterpreterCommandTest
    {
        public InterpreterCommandTest() { }

        [Fact]
        public void Parse_WhenPassValidArgs_ShouldReturns_FillProperties()
        {
            Arguments.Populate(typeof(InterpreterCommand), Arguments.Parse("-a credential \"-n dev -u augusto.mesquita -p t3st3 -d buy4dev\"'"));

            Assert.Equal("credential", InterpreterCommand.Add);
        }
    }
}
