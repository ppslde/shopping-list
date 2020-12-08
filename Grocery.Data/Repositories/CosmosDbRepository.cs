using Grocery.Data.Interfaces;
using Grocery.Model.Entities;
using Grocery.Model.Exceptions;
using Grocery.Model.Interfaces;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Grocery.Data.Repositories {
  public abstract class CosmosDbRepository<T> : IRepository<T>/*, IDocumentCollectionContext<T>*/ where T : Entity {

    //private readonly ICosmosDbClientFactory _cosmosDbClientFactory;
    private readonly CosmosDbOptions _dbOptions;
    private readonly DocumentClient _docClient;

    protected CosmosDbRepository(IOptions<CosmosDbOptions> options, ICosmosDbClientFactory cosmosDbClientFactory) {
      //_cosmosDbClientFactory = cosmosDbClientFactory;
      _dbOptions = options.Value;
      _docClient = new DocumentClient(_dbOptions.ServiceEndpoint, _dbOptions.AuthKey);
    }

    protected abstract string CollectionName { get; }
    protected virtual string GenerateId(T entity) => Guid.NewGuid().ToString();
    protected virtual PartitionKey ResolvePartitionKey(string entityId) => null;

    public async Task<T> GetByIdAsync(string id, CancellationToken ct) {
      try {
        //var cosmosDbClient = _cosmosDbClientFactory.GetClient(CollectionName);

        return await _docClient.ReadDocumentAsync<T>(
          UriFactory.CreateDocumentUri(_dbOptions.DatabaseName, CollectionName, id),
          new RequestOptions { PartitionKey = ResolvePartitionKey(id) },
          ct);
        //var document = await cosmosDbClient.ReadDocumentAsync(id, );
        //return JsonConvert.DeserializeObject<T>(document.ToString());
      }
      catch (DocumentClientException e) {
        if (e.StatusCode == HttpStatusCode.NotFound) {
          throw new EntityNotFoundException();
        }

        throw;
      }
    }

    public async Task<bool> AddAsync(T entity, CancellationToken ct) {
      try {
        entity.Uid = GenerateId(entity);
        var response = await _docClient.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(_dbOptions.DatabaseName, CollectionName), ct);

        if (response.StatusCode == HttpStatusCode.Created) {
          return true;
        }
        return false;
      }
      catch (DocumentClientException e) {
        if (e.StatusCode == HttpStatusCode.Conflict) {
          throw new EntityAlreadyExistsException();
        }

        throw;
      }
    }

    public async Task UpdateAsync(T entity) {
      try {
        var cosmosDbClient = _cosmosDbClientFactory.GetClient(CollectionName);
        await cosmosDbClient.ReplaceDocumentAsync(entity.Uid, entity);
      }
      catch (DocumentClientException e) {
        if (e.StatusCode == HttpStatusCode.NotFound) {
          throw new EntityNotFoundException();
        }

        throw;
      }
    }

    public async Task DeleteAsync(T entity) {
      try {
        var cosmosDbClient = _cosmosDbClientFactory.GetClient(CollectionName);
        await cosmosDbClient.DeleteDocumentAsync(entity.Uid, new RequestOptions {
          PartitionKey = ResolvePartitionKey(entity.Uid)
        });
      }
      catch (DocumentClientException e) {
        if (e.StatusCode == HttpStatusCode.NotFound) {
          throw new EntityNotFoundException();
        }

        throw;
      }
    }
  }
}
