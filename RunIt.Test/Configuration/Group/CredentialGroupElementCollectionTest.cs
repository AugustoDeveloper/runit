using Xunit;
using RunIt.Infra.Configuration;
using RunIt.Infra.Configuration.Element;
using System.Linq;

namespace RunIt.Test.Configuration.Group
{
    public class CredentialGroupElementCollectionTest
    {
        public CredentialGroupElementCollectionTest() { }

        [Fact]
        public void Count_WhenConfigWasSettedWithTwoRows_ShouldReturns_TwoRows()
        {
            var section = EnviromentConfigurationSection.Get();
            Assert.Equal(2, section.Credentials.Count);
        }

        [Fact]
        public void CheckElements_WhenConfigWasSettedWithTwoRows_ShouldReturns_SameElements()
        {
            var section = EnviromentConfigurationSection.Get();
            Assert.Collection(section.Credentials.Cast<CredentialElement>(),
                item => 
                {
                    Assert.Equal("dev", item.Name);
                    Assert.Equal("123.123", item.Password);
                    Assert.Equal("augusto.mesquita", item.Username);
                    Assert.Equal("buy4dev", item.Domain);
                },
                item => 
                {
                    Assert.Equal("np", item.Name);
                    Assert.Equal("123.123", item.Password);
                    Assert.Equal("augusto.mesquita", item.Username);
                    Assert.Equal("buy4np", item.Domain);
                });
        }
    }
}
