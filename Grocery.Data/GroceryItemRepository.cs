using Grocery.Model;
using Grocery.Model.Interfaces;
using Microsoft.Azure.Documents;
using System;

namespace Grocery.Data {
  public class GroceryItemRepository : CosmosDbRepository<GroceryItem>, IGroceryItemRepository {
    public GroceryItemRepository(ICosmosDbClientFactory factory) : base(factory) { }

    public override string CollectionName { get; } = "todoItems";
    public override string GenerateId(GroceryItem entity) => $"{entity.Category}:{Guid.NewGuid()}";
    public override PartitionKey ResolvePartitionKey(string entityId) => new PartitionKey(entityId.Split(':')[0]);
  }
}
