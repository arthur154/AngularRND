using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;

namespace API.Controllers
{

    [Route("api/incoming")]
    [ApiController]
    public class IncomingController : ControllerBase
    {
        [HttpPost("insert")]
        public async Task<IActionResult> Insert([FromBody] Dictionary<string, object> request)
        {
            Repository repo = new Repository();
            await repo.Insert(request);
            return Ok(new { correlationid = request["correlationid"] });
        }

        [HttpPost("bulkinsert")]
        public dynamic BulkInsert([FromBody] List<dynamic> request)
        {
            return request;
        }
    }

    public class Repository : IDisposable
    {
        const string ServiceBusConnectionString = "Endpoint=sb://egdbus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=A30bD92KqMJ9FjWvFJHR7dT4LiJsqbVW91r/7hpI1Xg=";
        const string TopicName = "incoming";
        const string StatusTopicName = "status";
        ITopicClient _topicClient;
        ITopicClient _statusClient;

        public Repository()
        {
            _topicClient = new TopicClient(ServiceBusConnectionString, TopicName);
            _statusClient = new TopicClient(ServiceBusConnectionString, StatusTopicName);
        }
        public async Task Insert(Dictionary<string,object> document)
        { 
            if (document.ContainsKey("correlationid")==false)
            {
                document.Add("correlationid" ,Guid.NewGuid());
            }
            var message = new Message(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(document)));
            message.PartitionKey = "incoming";
            var statusMessage = new Message(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new { state = "new", correlationid = document["correlationid"], timestamp = DateTime.Now.ToUniversalTime() })));
            statusMessage.PartitionKey = "status";

            List<Task> tasks = new List<Task>
            {
                _topicClient.SendAsync(message),
                _statusClient.SendAsync(statusMessage)
            };

            await Task.WhenAll(tasks.ToArray());
            //TODO:  determine if we need to check if the task failed, resend?
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (_topicClient != null)
                    {
                        _topicClient = null;
                    }
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~Repository() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
