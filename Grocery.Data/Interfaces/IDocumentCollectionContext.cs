using Grocery.Model.Entities;
using Microsoft.Azure.Documents;
using System;

namespace Grocery.Data.Interfaces {
  [Obsolete("", true)]
  public interface IDocumentCollectionContext<in T> where T : Entity {
    string CollectionName { get; }
    string GenerateId(T entity);
    PartitionKey ResolvePartitionKey(string entityId);
  }
}
