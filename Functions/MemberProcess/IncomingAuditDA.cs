using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace IncomingAudit
{
    public class IncomingAuditDA
    {
        private DocumentClient _client;
        private string _dataBaseId;
        private string _collectionId;
        private string _endpoint;
        private string _authKey;

        public IncomingAuditDA()
        {
            _endpoint = "";
            _authKey = "";
            _client = new DocumentClient(new Uri(_endpoint), _authKey);
            _dataBaseId = "";
            _collectionId = "audit";
        }

        public async Task<ResourceResponse<Document>> UpsertDoc(Dictionary<string, object> message)
        {
            return await _client.UpsertDocumentAsync(UriFactory.CreateDocumentCollectionUri(_dataBaseId, _collectionId), message);
        }
    }
}
