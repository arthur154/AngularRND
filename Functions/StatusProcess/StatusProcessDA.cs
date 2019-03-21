using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace StatusProcess
{
    public class StatusProcessDA
    {
        private DocumentClient _client;
        private string _dataBaseId;
        private string _collectionId;
        private string _endpoint;
        private string _authKey;

        public StatusProcessDA()
        {
            _endpoint = "https://egdrnd.documents.azure.com:443/";
            _authKey = "hmlzE3qX52sJH0U1UWRqOd23urjLIdKfUlDKtCFOEgOcqolddJG4Nzz93WZN19YfnqdDpLbPmzXA7mNA1vV6uw==";
            _client = new DocumentClient(new Uri(_endpoint), _authKey);
            _dataBaseId = "egdrnd";
            _collectionId = "status";
        }

        public async Task<ResourceResponse<Document>> UpsertDoc(Dictionary<string, object> message)
        {
            if (message.ContainsKey("correlationid"))
            {
                if (!message.ContainsKey("id"))
                {
                    message.Add("id", message["correlationid"]);
                }
            }
            return await _client.UpsertDocumentAsync(UriFactory.CreateDocumentCollectionUri(_dataBaseId, _collectionId), message);
        }
    }
}
