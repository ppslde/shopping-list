using Grocery.Data.Interfaces;
using Grocery.Model;
using Grocery.Model.Interfaces;
using Microsoft.Azure.Documents;
using System;

namespace Grocery.Data.Repositories {
  public class GroceryItemRepository : CosmosDbRepository<GroceryItem>, IGroceryItemRepository {
    public GroceryItemRepository(ICosmosDbClient dbClient) : base(dbClient) { }
    protected override string CollectionName { get; } = "grocery-items";
    protected override string GenerateId(GroceryItem entity) => $"{entity.Category}:{Guid.NewGuid()}";
    protected override PartitionKey ResolvePartitionKey(string entityId) => new PartitionKey(entityId.Split(':')[0]);
  }
}
