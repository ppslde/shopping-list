using System;

namespace Grocery.Model.Entities {
  public abstract class Entity {
    public string Uid { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
  }
}
