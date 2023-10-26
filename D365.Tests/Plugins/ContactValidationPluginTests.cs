using D365.Plugins;
using Microsoft.Xrm.Sdk;
using Moq;
using Xunit;

namespace D365.Tests.Plugins
{
    public class ContactValidationPluginTests : BasePluginTests
    {

        private ContactValidationPlugin _sut;

        public ContactValidationPluginTests()
        {       
            _sut = new ContactValidationPlugin(() => _pluginServiceMock.Object);
        }

        [Theory]
        [InlineData("Create","Contact",(int)EventPipeline.PreValidation)]
        [InlineData("Update", "Contact", (int)EventPipeline.PreValidation)]
        public void Plugin_should_execute_only_for_given_message_entity_stage(string message,string entityName,int stage)
        {
            //Arrange

            InitializePluginExecutionContext(message, entityName, stage);

            //Act
            _sut.Execute(_serviceProviderMock.Object);

            //Assert
            _pluginServiceMock.Verify(m => m.Execute(It.IsAny<IPluginExecutionContext>()), Times.Once);
        }


        //TODO:Add more test cases  [InlineData("<MESSAGE>", "<ENTITY_NAME>", (int)<EventPipeline.<Stage>)]
        [Theory]
        [InlineData("Create", "Account", (int)EventPipeline.PreValidation)]
        [InlineData("Update", "Acount", (int)EventPipeline.PostOperation)]
        public void Plugin_must_not_execute_for_given_message_entity_stage(string message, string entityName, int stage)
        {
            //Arrange

            InitializePluginExecutionContext(message, entityName, stage);

            //Act
            _sut.Execute(_serviceProviderMock.Object);

            //Assert
            _pluginServiceMock.Verify(m => m.Execute(It.IsAny<IPluginExecutionContext>()), Times.Never);
        }
    }
}
