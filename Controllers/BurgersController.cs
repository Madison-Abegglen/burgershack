using System;
using System.Collections.Generic;
using burgershack.Models;
using burgershack.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace burgershack.Controllers
{
  [Route("api/[controller]")] // you could have "api/bananas", you could remove api and just have "/bananas", in url => localhost:5001/bananas
  [ApiController]
  public class BurgersController : Controller
  {
    // this needs to be instantiated by someone; you're never gonna be the one to instantiate the repository
    BurgersRepository _repo;
    public BurgersController(BurgersRepository repo)
    {
      _repo = repo;
    }

    // EVERYTHING IS AN OBJECT, including the burgerscontroller we just wrote

    [HttpGet]
    // when someone does a get request to this route, i want to return them an array of burgers
    public IEnumerable<Burger> Get()
    {
      return _repo.GetAll();
    }

    [HttpPost]
    // when someone does a post request to this route, they can add a burger
    // you need to take in a burger
    // from body, you will be expecting to take in a burger-type-class called burger
    public Burger Post([FromBody] Burger burger)
    {
      // from here you can add the burger, and return it
      // every request made to server, the controller becomes reconstructed
      // due to it being a huge application, and security reasons, theres no reason to keep a controller in memory.
      // this enhances speed, as well as ensures your information is encapsulated 
      // you wouldn't want one user's information to be stored and then sent you another user 
      if (ModelState.IsValid)
      {
        burger = new Burger(burger.Name, burger.Description, burger.Price);
        return _repo.Create(burger);
      }
      throw new Exception("INVALID BURGER");
    }
  }
}