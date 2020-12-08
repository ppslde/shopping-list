using System;

namespace Grocery.Options {
  public enum ConnectionStringMode {
    Azure,
    Emulator
  }

  [Obsolete("", true)]
  public class ConnectionStringsOptions {
    public ConnectionStringMode Mode { get; set; }
    public ConnectionStringOptions Azure { get; set; }
    public ConnectionStringOptions Emulator { get; set; }

    public ConnectionStringOptions ActiveConnectionStringOptions =>
        Mode == ConnectionStringMode.Azure ? Azure : Emulator;
  }
}
