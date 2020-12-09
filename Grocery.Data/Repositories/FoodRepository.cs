using Grocery.Data.Interfaces;
using Grocery.Model.Entities;
using Microsoft.Azure.Documents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grocery.Data.Repositories {
  public class FoodRepository : CosmosDbRepository<Food>, IFoodRepository {
    public FoodRepository(ICosmosDbClient dbClient) : base(dbClient) { }
    protected override string CollectionName => "foods";
    protected override PartitionKey ResolvePartitionKey(Food entity) => new PartitionKey(entity.GroceryId);
  }
}
