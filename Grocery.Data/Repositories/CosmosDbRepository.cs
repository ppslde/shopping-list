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
  public abstract class CosmosDbRepository<T> : IRepository<T> where T : Entity {

    private readonly ICosmosDbClient _dbClient;

    protected CosmosDbRepository(ICosmosDbClient dbClient) {
      _dbClient = dbClient;
    }

    protected abstract string CollectionName { get; }
    protected virtual string GenerateId(T entity) => Guid.NewGuid().ToString();
    protected virtual PartitionKey ResolvePartitionKey(string entityId) => null;


    public async Task<T> GetByIdAsync(string id) {
      try {
        var document = await _dbClient.ReadDocumentAsync(CollectionName, id, new RequestOptions {
          PartitionKey = ResolvePartitionKey(id)
        });
        return JsonConvert.DeserializeObject<T>(document.ToString());
      }
      catch (DocumentClientException e) {
        if (e.StatusCode == HttpStatusCode.NotFound) {
          throw new EntityNotFoundException();
        }
        throw;
      }
    }

    public async Task<T> AddAsync(T entity) {

      try {
        entity.Uid = GenerateId(entity);
        var document = await _dbClient.CreateDocumentAsync(CollectionName, entity);
        return JsonConvert.DeserializeObject<T>(document.ToString());
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
        await _dbClient.ReplaceDocumentAsync(CollectionName, entity.Uid, entity);
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
        await _dbClient.DeleteDocumentAsync(CollectionName, entity.Uid, new RequestOptions {
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
