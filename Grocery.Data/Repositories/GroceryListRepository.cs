using Grocery.Data.Interfaces;
using Grocery.Model.Entities;
using Microsoft.Azure.Documents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grocery.Data.Repositories {
  class GroceryListRepository : CosmosDbRepository<GroceryList>, IGroceryListRepository {
    public GroceryListRepository(ICosmosDbClient dbClient) : base(dbClient) { }
    protected override string CollectionName => "lists";
    protected override PartitionKey ResolvePartitionKey(GroceryList entity) => new PartitionKey(entity.Id);
  }
}
