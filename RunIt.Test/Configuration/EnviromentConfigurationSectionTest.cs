using RunIt.Infra.Configuration;
using Xunit;

namespace RunIt.Test.Configuration
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
            //Assert.NotNull(section.Credentials);
            //Assert.Equal(1, section.Applications.Count);
            //Assert.Equal(2, section.Credentials.Count);
        }
    }
}
