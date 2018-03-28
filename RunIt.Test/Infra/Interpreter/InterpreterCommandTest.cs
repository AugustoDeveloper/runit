using RunIt.Infra.Interpreter;
using System;
using Xunit;


namespace RunIt.Test.Infra.Interpreter
{
    public class InterpreterCommandTest
    {
        public InterpreterCommandTest() { }

        [Fact]
        public void New_WhenPassNullArguments_ShouldReturns_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new InterpreterCommand(null));
        }

        [Fact]
        public void New_WhenPassNoArguments_ShouldReturns_ArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new InterpreterCommand(new string[] { }));
        }

        [Fact]
        public void New_WhenPassOneArgument_ShouldReturns_ArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new InterpreterCommand(new string[] { "-e" }));
        }

        [Fact]
        public void New_WhenPassTwoArgument_ShouldReturns_ArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new InterpreterCommand(new string[] { "-e", "dev" }));
        }

        [Fact]
        public void New_WhenPassFourArgument_ShouldReturns_ArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new InterpreterCommand(new string[] { "-e", "sqlm", "dev", "1" }));
        }

        [Theory]
        [InlineData("-1")]
        [InlineData("a")]
        [InlineData("-a")]
        public void New_WhenPassAInvalidFirstArgument_ShouldReturns_ArgumentException(string firstArgument)
        {
            Assert.Throws<ArgumentException>(() => new InterpreterCommand(new string[] { firstArgument, "sqlm", "dev" }));
        }

        [Theory]
        [InlineData("-1")]
        [InlineData("-a")]
        [InlineData("-xa")]
        public void New_WhenPassAInvalidSecondArgument_ShouldReturns_ArgumentException(string secondArgument)
        {
            Assert.Throws<ArgumentException>(() => new InterpreterCommand(new string[] { "-e", secondArgument, "dev" }));
        }

        [Theory]
        [InlineData("-1")]
        [InlineData("-a")]
        [InlineData("-xa")]
        public void New_WhenPassAInvalidThirdArgument_ShouldReturns_ArgumentException(string thirdArgument)
        {
            Assert.Throws<ArgumentException>(() => new InterpreterCommand(new string[] { "-e", "sqlm", thirdArgument }));
        }

        [Theory]
        [InlineData("slqm")]
        [InlineData("msqlm")]
        [InlineData("mssql")]
        public void Execute_WhenPassNotExistsAliasApplication_ShouldReturns_NullReferenceException(string alias)
        {
            Assert.Throws<NullReferenceException>(() => new InterpreterCommand(new string[] { "-e", alias, "dev" }).Execute());
        }

        [Theory]
        [InlineData("devd")]
        [InlineData("stsg")]
        [InlineData("npm")]
        public void Execute_WhenPassNotExistsCredential_ShouldReturns_NullReferenceException(string credentialName)
        {
            Assert.Throws<NullReferenceException>(() => new InterpreterCommand(new string[] { "-e", "sqlm", credentialName }).Execute());
        }

        [Theory]
        [InlineData("-e", "sqlm", "np")]
        public void Execute_WhenPassValidArgument_ShouldReturns_Nothing(string argument, string alias, string credentialName)
        {
            new InterpreterCommand(new string[] { argument, alias, credentialName }).Execute();
        }

    }
}
