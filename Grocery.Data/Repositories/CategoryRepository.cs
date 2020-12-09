using Grocery.Data.Interfaces;
using Grocery.Model.Entities;
using Microsoft.Azure.Documents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grocery.Data.Repositories {
  public class CategoryRepository : CosmosDbRepository<Category>, ICategoryRepository {
    public CategoryRepository(ICosmosDbClient dbClient) : base(dbClient) { }
    protected override string CollectionName => "categories";
    protected override PartitionKey ResolvePartitionKey(Category entity) => new PartitionKey(entity.GroceryId);
  }
}