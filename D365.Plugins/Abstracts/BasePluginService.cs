using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D365.Plugins.Abstracts
{
    public abstract class BasePluginService
    {
        public BasePluginService(IOrganizationService organizationService, ITracingService tracingService)
        {
            this.OrganizationService = organizationService;
            TracingService = tracingService;
        }

        protected IOrganizationService OrganizationService { get; set; }

        protected ITracingService TracingService { get; set; }

        public abstract void Execute(IPluginExecutionContext executionContext);
    }
}
