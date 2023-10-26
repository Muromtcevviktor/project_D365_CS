using D365.Plugins.Abstracts;
using D365.Plugins.Services;
using System;

namespace D365.Plugins
{
    //How to develop plugins - clean code
    public class ContactValidationPlugin : PluginBase
    {
        public ContactValidationPlugin(Func<BasePluginService> baseServiceFactory)
            :this()
        {
           
            ValidationServiceFactory = baseServiceFactory;
        }

        public ContactValidationPlugin()
        {
            RegisteredEvents.Add(new PluginEvent()
            {
                Message = "Update",
                EntityName = "Contact",
                Stage = EventPipeline.PreValidation,
                ToExecute = OnUpdate
            });

            RegisteredEvents.Add(new PluginEvent()
            {
                Message = "Create",
                EntityName = "Contact",
                Stage = EventPipeline.PreValidation,
                ToExecute = OnCreate
            });
        }

        public void OnUpdate()
        {
            ValidationServiceFactory().Execute(ExecutionContext);
        }

        public void OnCreate()
        {
            ValidationServiceFactory().Execute(ExecutionContext);
        }

        protected override void InitializeDependencies()
        {
            ValidationServiceFactory = ValidationServiceFactory ?? new Func<BasePluginService>(() => new ContactValidationService(OrganizationServiceFactory, TracingService));
        }
    }
}
