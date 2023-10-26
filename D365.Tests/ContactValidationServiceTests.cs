using D365.Plugins.Services;
using Microsoft.Xrm.Sdk;
using Moq;
using System;
using Xunit;

namespace D365.Tests
{
    public class ContactValidationServiceTests
    {
        private ContactValidationService _sut;

        public ContactValidationServiceTests()
        {
            Mock<IOrganizationServiceFactory> orgServiceMock = new Mock<IOrganizationServiceFactory>();
            Mock<ITracingService> tracingServiceMock = new Mock<ITracingService>();
            _sut = new ContactValidationService(new Lazy<IOrganizationServiceFactory>(()=>orgServiceMock.Object), tracingServiceMock.Object);
        }

        [Fact]
        public void Should_throw_exception_when_firstname_is_invalid()
        {
            Entity contactEntity = new Entity("contact", Guid.NewGuid());

            contactEntity["firstname"] = "John 2"; // first name contains digits !
           

            ParameterCollection inputParameters = new ParameterCollection();

            inputParameters.Add("Target", contactEntity);

            Mock<IPluginExecutionContext> pluginExecution = new Mock<IPluginExecutionContext>();


            pluginExecution.Setup(m => m.InputParameters).Returns(inputParameters);


            Assert.Throws<InvalidPluginExecutionException>(() => _sut.Execute(pluginExecution.Object));
        }

        [Fact]
        public void Should_throw_exception_when_lastname_is_invalid()
        {
            Entity contactEntity = new Entity("contact", Guid.NewGuid());

            contactEntity["lastname"] = "Smith 2"; //last name contains digits !
            
            ParameterCollection inputParameters = new ParameterCollection();

            inputParameters.Add("Target", contactEntity);

            Mock<IPluginExecutionContext> pluginExecution = new Mock<IPluginExecutionContext>();


            pluginExecution.Setup(m => m.InputParameters).Returns(inputParameters);

            Assert.Throws<InvalidPluginExecutionException>(() => _sut.Execute(pluginExecution.Object));
        }

        [Fact]
        public void Should_throw_exception_when_businessphone_is_invalid()
        {
            Entity contactEntity = new Entity("contact", Guid.NewGuid());

            contactEntity["telephone1"] = "123 456 789"; //missing country code prefix !

            ParameterCollection inputParameters = new ParameterCollection();

            inputParameters.Add("Target", contactEntity);

            Mock<IPluginExecutionContext> pluginExecution = new Mock<IPluginExecutionContext>();

            pluginExecution.Setup(m => m.InputParameters).Returns(inputParameters);

            Assert.Throws<InvalidPluginExecutionException>(() => _sut.Execute(pluginExecution.Object));
        }

        [Fact]
        public void Should_not_throw_exception_when_businessphone_is_valid()
        {
            Entity contactEntity = new Entity("contact", Guid.NewGuid());

            contactEntity["telephone1"] = "+48 123 456 789"; //missing country code prefix !

            ParameterCollection inputParameters = new ParameterCollection();

            inputParameters.Add("Target", contactEntity);

            Mock<IPluginExecutionContext> pluginExecution = new Mock<IPluginExecutionContext>();

            pluginExecution.Setup(m => m.InputParameters).Returns(inputParameters);

            _sut.Execute(pluginExecution.Object);
        }
    }
}