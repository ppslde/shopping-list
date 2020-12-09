using Grocery.Data.Interfaces;
using Grocery.Model;
using Grocery.Model.Entities;
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

    private readonly ICategoryRepository _repo;

    public GroceryItemController(ICategoryRepository repo) {
      _repo = repo;
    }

    [HttpGet]
    public async Task<Category> GetAsync() {
      var i = await _repo.AddAsync(
        new Category {
          GroceryId = Guid.NewGuid().ToString(),
          Title = "TestCat",
          Translations = new[] { 
            new Translation { Language = "de", Title = "Gruppe 1" }
          }.ToList()
        });
      return i;
    }
  }
}
