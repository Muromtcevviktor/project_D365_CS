using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D365.Plugins.Abstracts
{
    public abstract class PluginBase : IPlugin
    {

        public PluginBase()
        {
            RegisteredEvents = new List<PluginEvent>();
        }

        protected ITracingService TracingService { get; set; }

        protected IPluginExecutionContext ExecutionContext { get; set; }

        protected List<PluginEvent> RegisteredEvents { get; set; }

        protected Lazy<IOrganizationServiceFactory> OrganizationServiceFactory { get; set; }


        public void Execute(IServiceProvider serviceProvider)
        {
            TracingService =
(ITracingService)serviceProvider.GetService(typeof(ITracingService));

            ExecutionContext = (IPluginExecutionContext)
    serviceProvider.GetService(typeof(IPluginExecutionContext));

            OrganizationServiceFactory = new Lazy<IOrganizationServiceFactory>(() => (IOrganizationServiceFactory)
  serviceProvider.GetService(typeof(IOrganizationServiceFactory)));



            string message = ExecutionContext.MessageName;
            int stage = ExecutionContext.Stage;

            TracingService.Trace($"{this.GetType().FullName} is getting triggered on {message} and {((EventPipeline)stage).ToString()}");

            var eventsToHandle = RegisteredEvents.Where(e => e.Message == message && stage == (int)e.Stage).ToList();

            if (!eventsToHandle.Any())
            {
                TracingService.Trace($"{this.GetType().FullName} could not match any registered plugin events for given {message} - {stage}");
            }

            foreach (var @event in eventsToHandle)
            {

                if (@event.Message == message
                    && (int)@event.Stage == stage)
                {
                    try
                    {
                        TracingService.Trace($"{this.GetType().FullName} executing on {message} and {((EventPipeline)stage).ToString()}");

                        @event.ToExecute();

                        TracingService.Trace($"{this.GetType().FullName} executed on {message} and {((EventPipeline)stage).ToString()}");
                    }
                    catch (InvalidPluginExecutionException)
                    {
                        throw;
                    }
                    catch (Exception ex)
                    {
                        TracingService.Trace($"{this.GetType().FullName} error occured on {message} and {((EventPipeline)stage).ToString()}");
                        TracingService.Trace(ex.Message + ex.StackTrace);
                    }
                }
            }
        }
    }
}
