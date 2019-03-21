using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;

namespace IncomingAudit
{
    //public static class IncomingAudit
    //{
    //    public static class IncomingAuditTrigger
    //    {
    //        [FunctionName("IncomingAudit")]
    //        public static void RunTopic([ServiceBusTrigger("incoming",
    //                                 "audit",
    //                                 AccessRights.Manage,
    //                                 Connection = "ServiceBusConnection")]
    //                                 string topicMessage,
    //                                 TraceWriter log)
    //        {
    //            log.Info("Initializing ServiceBus Topic Trigger for Incoming Audit");
    //            log.Info($"C# ServiceBus topic trigger function processing message: {topicMessage}");
    //            var message = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(topicMessage);
    //            IncomingAuditDA auditDA = new IncomingAuditDA();
    //            Task task = auditDA.UpsertDoc(message);
    //            task.Wait();
    //            log.Info("Successfully wrote message to DocDb.");
    //        }
    //    }

    //}

}
