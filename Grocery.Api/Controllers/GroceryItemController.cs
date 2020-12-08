using Grocery.Model;
using Grocery.Model.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grocery.Api.Controllers {
  [Route("api/[controller]")]
  [ApiController]
  public class GroceryItemController : ControllerBase {

    private readonly IGroceryItemRepository _repo;

    public GroceryItemController(IGroceryItemRepository repo) {
      _repo = repo;
    }

    [HttpGet]
    public async Task<GroceryItem> GetAsync() {


      var i = await _repo.AddAsync(new GroceryItem { Category = "Test" });

      return i;
    }
  }
}
