using D365.Plugins.Abstracts;
using System;

namespace D365.Plugins.Services
{
    //How to develop plugins - clean code
    public class ContactValidationPlugin : PluginBase
    {
        private Func<BasePluginService> ValidationServiceFactory;
        
        public ContactValidationPlugin()
        {
            RegisteredEvents.Add(new PluginEvent()
            {
                Message = "Update",
                Stage = EventPipeline.PreValidation,
                ToExecute = OnUpdate
            });

            RegisteredEvents.Add(new PluginEvent()
            {
                Message = "Create",
                Stage = EventPipeline.PreValidation,
                ToExecute = OnCreate
            });

            ValidationServiceFactory = () => new ContactValidationService(OrganizationServiceFactory, TracingService);
        }

        public void OnUpdate()
        {
            ValidationServiceFactory().Execute(ExecutionContext);
        }

        public void OnCreate()
        {
            ValidationServiceFactory().Execute(ExecutionContext);
        }
    }
}
