using Microsoft.Xrm.Sdk;
using System;

namespace D365.Plugins
{
    //How to not develop plugins (dirty approach)
    public class DemoPlugin : IPlugin
    {
        ITracingService _tracingService;


        public void Execute(IServiceProvider serviceProvider)
        {
            try
            {
                 _tracingService =
(ITracingService)serviceProvider.GetService(typeof(ITracingService));

                IPluginExecutionContext context = (IPluginExecutionContext)
        serviceProvider.GetService(typeof(IPluginExecutionContext));


                if (context.InputParameters.ContainsKey("Target"))
                {
                    Entity contact = (Entity)context.InputParameters["Target"];

                    foreach (var attr in contact.Attributes)
                    {
                        _tracingService.Trace($"attr {attr.Key}:{attr.Value}");
                    }
                }
                else
                {
                    _tracingService.Trace("Target does not exist");
                }
                _tracingService.Trace("DemoPlugin się wykonał");
            }
            catch(Exception ex)
            {
                _tracingService.Trace($"Wystąpił błąd {ex.Message} + {ex.StackTrace}");
            }
        }
    }
}