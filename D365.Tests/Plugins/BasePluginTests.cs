using D365.Plugins.Abstracts;
using Microsoft.Xrm.Sdk;
using Moq;
using System;

namespace D365.Tests.Plugins
{
    public abstract class BasePluginTests
    {
        private readonly Mock<ITracingService> _tracingServiceMock;
        protected readonly Mock<BasePluginService> _pluginServiceMock;
        protected readonly Mock<IServiceProvider> _serviceProviderMock;
        protected readonly Mock<IPluginExecutionContext> _executionContext;

        public BasePluginTests()
        {
            _tracingServiceMock = new Mock<ITracingService>();
            _pluginServiceMock = new Mock<BasePluginService>();
            _serviceProviderMock = new Mock<IServiceProvider>();
            _executionContext = new Mock<IPluginExecutionContext>();

            _serviceProviderMock.Setup(m => m.GetService(It.Is<Type>(t => t.Name == typeof(IPluginExecutionContext).Name))).Returns(_executionContext.Object);
            _serviceProviderMock.Setup(m => m.GetService(It.Is<Type>(t => t.Name == typeof(ITracingService).Name))).Returns(_tracingServiceMock.Object);
        }

        protected void InitializePluginExecutionContext(string message, string entityName, int stage)
        {
            _executionContext.Setup(m => m.MessageName).Returns(message);
            _executionContext.Setup(m => m.PrimaryEntityName).Returns(entityName);
            _executionContext.Setup(m => m.Stage).Returns(stage);
        }
    }
}
