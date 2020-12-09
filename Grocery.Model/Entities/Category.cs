using System.Collections.Generic;

namespace Grocery.Model.Entities {
  public class Category : Entity {
    public string GroceryId { get; set; }
    public string Title { get; set; }
    public List<Translation> Translations { get; set; } = new List<Translation>();
  }
}
