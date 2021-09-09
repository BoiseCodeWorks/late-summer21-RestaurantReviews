using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using RestaurantReviews.Interfaces;
using RestaurantReviews.Models;

namespace RestaurantReviews.Repositories
{
  public class RestaurantsRepository : IRepo<Restaurant, int>
  {
    private readonly IDbConnection _db;

    public RestaurantsRepository(IDbConnection db)
    {
      _db = db;
    }

    public List<Restaurant> GetAll()
    {
      string sql = "SELECT * FROM restaurants;";
      return _db.Query<Restaurant>(sql).ToList();
    }

    public Restaurant GetById(int id)
    {
      string sql = "SELECT * FROM restaurants WHERE id = @id;";
      return _db.QueryFirstOrDefault<Restaurant>(sql, new { id });
    }

    public Restaurant Create(Restaurant newData)
    {
      string sql = @"
      INSERT INTO restaurants
      (name, location, creatorId)
      VALUES
      (@Name, @Location, @CreatorId);
      SELECT LAST_INSERT_ID();
      ";
      newData.Id = _db.ExecuteScalar<int>(sql, newData);
      return newData;

    }

    public Restaurant Edit(Restaurant updatedData)
    {
      string sql = @"
      UPDATE restaurants
      SET
        name = @Name,
        location = @Location
      WHERE id = @Id
      ;";
      _db.Execute(sql, updatedData);
      return updatedData;
    }

    public void Delete(int id)
    {
      string sql = "DELETE FROM restaurants WHERE id = @id LIMIT 1;";
      _db.Execute(sql, new { id });

    }
  }
}