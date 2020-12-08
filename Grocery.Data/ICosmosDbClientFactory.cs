namespace Grocery.Data {
  public interface ICosmosDbClientFactory {
    ICosmosDbClient GetClient(string collectionName);
  }
}
