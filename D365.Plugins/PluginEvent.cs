using System;
namespace D365.Plugins
{
    public class PluginEvent
    {
        public string Message { get; set; }

        public EventPipeline Stage { get; set; }

        public Action ToExecute { get; set; }
    }
}
