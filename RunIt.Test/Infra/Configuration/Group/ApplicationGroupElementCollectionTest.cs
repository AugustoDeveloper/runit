using RunIt.Infra.Configuration;
using RunIt.Infra.Configuration.Element;
using Xunit;
using System.Linq;
using System;

namespace RunIt.Test.Infra.Configuration.Group
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
                    Assert.Equal("notepad.exe", item.Filename);
                });
        }

        [Theory]
        [InlineData("sqlm12")]
        [InlineData("sqlm99")]
        [InlineData("vstd12312")]
        public void GetElementByAlias_WhenNotExistsElementWithAlias_ShouldReturns_NullReferenceException(string alias)
        {
            var section = EnviromentConfigurationSection.Get();
            Assert.Throws<NullReferenceException>(() => section.Applications[alias]);
        }

        [Theory]
        [InlineData("sqlm")]        
        public void GetElementByAlias_WhenExistsElementWithAlias_ShouldReturns_NotNullReference(string alias)
        {
            var section = EnviromentConfigurationSection.Get();
            Assert.NotNull(section.Applications[alias]);
        }

        [Theory]
        [InlineData("sqlm")]
        public void GetElementByAlias_WhenExistsElementWithAlias_ShouldReturns_NotNullReferenceAndCompleted(string alias)
        {
            var section = EnviromentConfigurationSection.Get();
            var item = section.Applications[alias];
            Assert.NotNull(item);
            Assert.Equal("sqlm", item.Alias);
            Assert.Equal("SQL Management Studio", item.Name);
            Assert.Equal("notepad.exe", item.Filename);
        }
    }
}
