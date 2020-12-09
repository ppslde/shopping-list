using Grocery.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grocery.Model {
  public class GroceryStore {
    public int Id { get; set; }
    public string Title { get; set; }
    public List<GrocerySection> Sections { get; set; }
  }
}
