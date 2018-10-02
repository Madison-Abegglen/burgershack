using System.Collections.Generic;
using System.Data;
using System.Linq;
using burgershack.Models;
using Dapper;

namespace burgershack.Repositories
{
  public class BurgersRepository
  {
    private IDbConnection _db;
    // public string tableName = "burgers";
    // this is bad practice but you know you can't trust them users to dont do it
    public BurgersRepository(IDbConnection db)
    {
      _db = db;
    }

    // CRUD VIA SQL !!!!!

    // GET ALL BURGERS
    public IEnumerable<Burger> GetAll()
    {
      // we need to run some SQL against our database;
      // this says: hey db, lets run a query of type burger
      // whatever is pulled from db will cast as a burger, if successful you'll have a burger otherwise you wont have a successful cast
      // DONT EVER TRUST USER INPUT TO BE WHAT YOU WANT IT TO BE IE A BURGER
      // if you trusted your users you could say: 
      // return _db.Query<Burger>($"SELECT * FROM {tableName}");
      return _db.Query<Burger>("SELECT * FROM burgers");
    }


    // GET BURGER BY ID
    public Burger GetById(int id)
    {
      // @id is the parameter => the new creates a new 'dynamic' object with property called 'id'
      // dapper will check that '@id' to make sure it doesnt have any SQL commands to keep you safe
      return _db.Query<Burger>("SELECT * FROM burgers WHERE id = @id", new { id }).FirstOrDefault();
    }

    // CREATE NEW BURGER
    public Burger Create(Burger burger)
    {
      // it returns the id of the record just inserted in the command; takes the item => inserts to database, comes back with the id to keep track of later
      // the order of the first (name, description, price) doesnt matter so long as the second (@Name, @Description, @Price) lines up in the same order as the first
      // MySQL will always lowercase column names, etc
      int id = _db.ExecuteScalar<int>(@"
        INSERT INTO burgers (name, description, price)
        VALUES (@Name, @Description, @Price)
        SELECT LAST_INSERT_ID();", burger
      );
      // execute = can write, query = can read
      burger.Id = id;
      return burger;
    }
    // UPDATE BURGER
    public Burger Update(Burger burger)
    {
      _db.Execute(@"
        UPDATE burgers SET (name, description, price)
        VALUES (@Name, @Description, @Price)
        WHERE id = @Id
      ", burger);
      return burger;
    }

    // DELETE BURGER
    public Burger Delete(Burger burger)
    {
      _db.Execute("DELETE FROM burgers WHERE id = @Id", burger);
      return burger;
    }

    public int Delete(int id)
    {
      return _db.Execute("DELETE FROM burgers WHERE id = @id", new { id });
    }

  }
}