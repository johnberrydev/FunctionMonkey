using FunctionMonkey.Abstractions.Builders.Model;

namespace FunctionMonkey.Model.OutputBindings
{
    internal class ServiceBusQueueOutputBinding : AbstractConnectionStringOutputBinding
    {
        public string QueueName { get; set; }
    }
}