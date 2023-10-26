using Microsoft.Xrm.Sdk;
using System;

namespace D365.Plugins.Abstracts
{
    public abstract class BasePluginService
    {
        public BasePluginService() { }

        public BasePluginService(Lazy<IOrganizationServiceFactory> organizationService, ITracingService tracingService)
        {
            this.OrganizationServiceFactory = organizationService;
            TracingService = tracingService;
        }

        protected Lazy<IOrganizationServiceFactory> OrganizationServiceFactory { get; set; }

        protected ITracingService TracingService { get; set; }

        public abstract void Execute(IPluginExecutionContext executionContext);


    }
}
