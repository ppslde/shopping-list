using System.Collections.Generic;

namespace Grocery.Model.Entities {
  public class Food : Entity {
    public string Title { get; set; }
    public string CategoryId { get; set; }
    public List<Translation> Translations { get; set; } = new List<Translation>();
  }
}
