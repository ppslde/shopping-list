using Grocery.Model;
using Microsoft.Azure.Documents;

namespace Grocery.Data {
  public interface IDocumentCollectionContext<in T> where T : Entity {
    string CollectionName { get; }
    string GenerateId(T entity);
    PartitionKey ResolvePartitionKey(string entityId);
  }
}
