namespace Grocery.Model {


  /// <summary>
  /// https://github.com/Azure-Samples/PartitionedRepository/blob/master/TodoService.Api/Startup.cs
  /// </summary>

  public class GroceryItem: Entity {
    public string Category { get; set; }
  }
}
