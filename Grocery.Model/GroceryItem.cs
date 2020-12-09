using Grocery.Model.Entities;

namespace Grocery.Model {


  /// <summary>
  /// https://github.com/Azure-Samples/PartitionedRepository/blob/master/TodoService.Api/Startup.cs
  /// </summary>

  public class GroceryItem {
    public string FoodId { get; set; }
    public string Display { get; set; }
    public bool Done { get; set; }
  }
}
