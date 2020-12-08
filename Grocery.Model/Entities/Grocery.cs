using System;
using System.Collections.Generic;
using System.Text;

namespace Grocery.Model.Entities {
  public class Grocery : Entity {
    public string Title { get; set; }
    public DateTime LastChange { get; set; }
    public bool Active { get; set; }
  }







  public class Translation {
    public string Language { get; set; }
    public string Title { get; set; }
  }
}
