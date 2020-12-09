using System;

namespace Grocery.Model.Entities {
  public abstract class Entity {
    public string Id { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
  }
}
