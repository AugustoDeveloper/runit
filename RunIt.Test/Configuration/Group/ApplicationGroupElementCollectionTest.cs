using RunIt.Infra.Configuration;
using RunIt.Infra.Configuration.Element;
using Xunit;
using System.Linq;

namespace RunIt.Test.Configuration.Group
{
    public class ApplicationGroupElementCollectionTest
    {
        public ApplicationGroupElementCollectionTest() { }

        [Fact]
        public void Count_WhenConfigWasSettedWithOneRow_ShouldReturns_SigleLine()
        {
            var section = EnviromentConfigurationSection.Get();

            Assert.Single(section.Applications);
        }


        [Fact]
        public void CheckElements_WhenConfigWasSettedWithTwoRows_ShouldReturns_SameElements()
        {
            var section = EnviromentConfigurationSection.Get();
            Assert.Collection(section.Applications.Cast<ApplicationElement>(),
                item =>
                {
                    Assert.Equal("sqlm", item.Alias);
                    Assert.Equal("SQL Management Studio", item.Name);
                    Assert.Equal(@"\Binn\sqlmanagement.exe", item.Filename);
                });
        }

    }
}
