using Xunit;
using RunIt.Infra.Configuration;
using RunIt.Infra.Configuration.Element;
using System.Linq;
using System;

namespace RunIt.Test.Infra.Configuration.Group
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


        [Theory]
        [InlineData("123desenv")]
        [InlineData("stg99")]
        [InlineData("hom314")]
        public void GetElementByAlias_WhenNotExistsElementWithName_ShouldReturns_NullReferenceException(string name)
        {
            var section = EnviromentConfigurationSection.Get();
            Assert.Throws<NullReferenceException>(() => section.Credentials[name]);
        }

        [Theory]
        [InlineData("dev")]
        [InlineData("np")]
        public void GetElementByAlias_WhenExistsElementWithName_ShouldReturns_NotNullReference(string name)
        {
            var section = EnviromentConfigurationSection.Get();
            Assert.NotNull(section.Credentials[name]);
        }

        [Theory]
        [InlineData("dev")]
        [InlineData("np")]
        public void GetElementByAlias_WhenExistsElementWithName_ShouldReturns_NotNullReferenceAndCompleted(string name)
        {
            var section = EnviromentConfigurationSection.Get();
            var item = section.Credentials[name];
            Assert.NotNull(item);

            if (name.Equals("np"))
            {
                Assert.Equal("np", item.Name);
                Assert.Equal("123.123", item.Password);
                Assert.Equal("augusto.mesquita", item.Username);
                Assert.Equal("buy4np", item.Domain);
            }
            else
            {
                Assert.Equal("dev", item.Name);
                Assert.Equal("123.123", item.Password);
                Assert.Equal("augusto.mesquita", item.Username);
                Assert.Equal("buy4dev", item.Domain);
            }
            
        }
    }
}
