using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;

namespace StatusProcess
{
    public static class StatusProcess
    {
        [FunctionName("StatusProcess")]
        public static void RunTopic([ServiceBusTrigger("status",
                                     "process",
                                     Connection = "ServiceBusConnection")]
                                     string topicMessage,
                                     TraceWriter log)
        {
            log.Info("Initializing ServiceBus Topic Trigger for Incoming Audit");
            log.Info($"C# ServiceBus topic trigger function processing message: {topicMessage}");
            var message = JsonConvert.DeserializeObject<Dictionary<string, object>>(topicMessage);
            StatusProcessDA statusDA = new StatusProcessDA();
            Task task = statusDA.UpsertDoc(message);
            task.Wait();
        }
    }
}