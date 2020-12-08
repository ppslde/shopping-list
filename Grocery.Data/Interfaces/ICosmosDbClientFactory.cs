namespace Grocery.Data.Interfaces {
  public interface ICosmosDbClientFactory {
    ICosmosDbClient GetClient(string collectionName);
  }
}
