using System;
using System.Collections.Generic;
using burgershack.Models;
using burgershack.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace burgershack.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class SmoothiesController : Controller
  {
    // EVERYTHING IS AN OBJECT, including the smoothiescontroller we just wrote
    SmoothiesRepository _repo;
    public SmoothiesController(SmoothiesRepository repo)
    {
      _repo = repo;
    }

    [HttpGet]
    // when someone does a get request to this route, i want to return them an array of smoothies
    public IEnumerable<Smoothie> Get()
    {
      return _repo.GetAll();
    }

    // when someone does a post request to this route, they can add a smoothie
    // you need to take in a smoothie
    // from body, you will be expecting to take in a smoothie-type-class called smoothie
    [HttpPost]
    public Smoothie Post([FromBody] Smoothie smoothie)
    {
      if (ModelState.IsValid)
      {
        smoothie = new Smoothie(smoothie.Name, smoothie.Description, smoothie.Price);
        return _repo.Create(smoothie);
      }
      throw new Exception("INVALID SMOOTHIE");
    }

    [HttpPut]

  }
}
