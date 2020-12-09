using Grocery.Data.Interfaces;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Grocery.Data {
  public class CosmosDbClient : ICosmosDbClient {
    private readonly CosmosDbOptions _dbOptions;
    private readonly IDocumentClient _docClient;

    public CosmosDbClient(IOptions<CosmosDbOptions> options) {
      _dbOptions = options.Value;
      _docClient = new DocumentClient(_dbOptions.ServiceEndpoint, _dbOptions.AuthKey, new JsonSerializerSettings {
        NullValueHandling = NullValueHandling.Ignore,
        DefaultValueHandling = DefaultValueHandling.Ignore,
        ContractResolver = new CamelCasePropertyNamesContractResolver()
      });
    }

    public async Task<Document> ReadDocumentAsync(string collection, string documentId, RequestOptions options = null, CancellationToken cancellationToken = default) {
      return await _docClient.ReadDocumentAsync(
          UriFactory.CreateDocumentUri(_dbOptions.DatabaseName, collection, documentId), options, cancellationToken);
    }

    public async Task<Document> CreateDocumentAsync(string collection, object document, RequestOptions options = null, bool disableAutomaticIdGeneration = false, CancellationToken cancellationToken = default) {
      return await _docClient.CreateDocumentAsync(
          UriFactory.CreateDocumentCollectionUri(_dbOptions.DatabaseName, collection), document, options,
          disableAutomaticIdGeneration, cancellationToken);
    }

    public async Task<Document> ReplaceDocumentAsync(string collection, string documentId, object document, RequestOptions options = null, CancellationToken cancellationToken = default) {
      return await _docClient.ReplaceDocumentAsync(
          UriFactory.CreateDocumentUri(_dbOptions.DatabaseName, collection, documentId), document, options,
          cancellationToken);
    }

    public async Task<Document> DeleteDocumentAsync(string collection, string documentId, RequestOptions options = null, CancellationToken cancellationToken = default) {
      return await _docClient.DeleteDocumentAsync(
          UriFactory.CreateDocumentUri(_dbOptions.DatabaseName, collection, documentId), options, cancellationToken);
    }

    public async Task QueryDocuments() {
      _docClient.CreateDocumentQuery("", "").AsEnumerable();
    }
  }
}
