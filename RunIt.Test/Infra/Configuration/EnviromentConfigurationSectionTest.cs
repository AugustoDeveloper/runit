using RunIt.Infra.Configuration;
using Xunit;

namespace RunIt.Test.Infra.Configuration
{
    public class EnviromentConfigurationSectionTest
    {
        public EnviromentConfigurationSectionTest()
        {
        }

        [Fact]
        public void Get_WhenSettedConfFileWithTag_ShouldReturns_NotNullSection()
        {
            var section = EnviromentConfigurationSection.Get();
            Assert.NotNull(section);
            Assert.NotNull(section.Applications);
            Assert.NotNull(section.Credentials);
        }
    }
}
