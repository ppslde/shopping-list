using System;
using System.Collections.Generic;
using System.Text;

namespace Grocery.Model.Entities {
  public class GroceryList : Entity {
    public string Title { get; set; }
    public DateTime LastChange { get; set; }
    public bool Active { get; set; }
    public List<GroceryItem> Items { get; set; }
    public List<GroceryStore> Stores { get; set; }
  }
}