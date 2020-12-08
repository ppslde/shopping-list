using System;
using System.Collections.Generic;

namespace Grocery.Options {
  public class CosmosDbOptions {
    public Uri ServiceEndpoint { get; set; }
    public string AuthKey { get; set; }
    public string DatabaseName { get; set; }
  }
}
