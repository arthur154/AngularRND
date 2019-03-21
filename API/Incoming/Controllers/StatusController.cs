using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;

namespace API.Controllers
{
    [Route("api/status")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            StatusDA da = new StatusDA();
            var doc = await da.GetDoc(id);
            return Ok(doc);
        }
    }

    public class StatusDA
    {
        private DocumentClient _client;
        private string _dataBaseId;
        private string _collectionId;
        private string _endpoint;
        private string _authKey;

        public StatusDA()
        {
            _endpoint = "https://egdrnd.documents.azure.com:443/";
            _authKey = "hmlzE3qX52sJH0U1UWRqOd23urjLIdKfUlDKtCFOEgOcqolddJG4Nzz93WZN19YfnqdDpLbPmzXA7mNA1vV6uw==";
            _client = new DocumentClient(new Uri(_endpoint), _authKey);
            _dataBaseId = "egdrnd";
            _collectionId = "status";
        }

        public async Task<Status> GetDoc(string id)
        {
            var result = await _client.ReadDocumentAsync(UriFactory.CreateDocumentUri(_dataBaseId, _collectionId, id));
            Status response = null;
            response = JsonConvert.DeserializeObject<Status>(result.Resource.ToString());
            return response;
        }
    }

    public class Status
    {
        public string state { get; set; }
        public string correlationid { get; set; }
        public DateTime timestamp { get; set; }
        public string id { get; set; }
        public string link { get; set; }
    }
}
