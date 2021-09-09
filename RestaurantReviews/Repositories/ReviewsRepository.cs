using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using RestaurantReviews.Interfaces;
using RestaurantReviews.Models;

namespace RestaurantReviews.Repositories
{
  public class ReviewsRepository : IRepo<Review, int>
  {
    private readonly IDbConnection _db;

    public ReviewsRepository(IDbConnection db)
    {
      _db = db;
    }

    public List<Review> GetAll()
    {
      string sql = @"
      SELECT
       a.*,
       r.*
      FROM reviews r
      JOIN accounts a ON a.id = r.creatorId
      ;";
      return _db.Query<Profile, Review, Review>(sql, (prof, review) =>
      {
        review.Creator = prof;
        return review;
      }, splitOn: "id").ToList();
    }

    internal List<Review> GetAll(int restaurantId)
    {
      string sql = @"
      SELECT 
        a.*,
        r.*
      FROM reviews r
      JOIN accounts a ON a.id = r.creatorId
      WHERE r.restaurantId = @restaurantId AND published = 1;";
      return _db.Query<Profile, Review, Review>(sql, (prof, review) =>
      {
        review.Creator = prof;
        return review;
      }, new { restaurantId }, splitOn: "id").ToList();
    }

    // NOTE the double populate here might be overkill as you already have the account
    // internal List<RestaurantReview> GetAllByAccountId(string accountId)
    // {
    //   string sql = @"
    //   SELECT 
    //     a.*,
    //     rv.*,
    //     rt.*
    //   FROM reviews rv
    //   JOIN accounts a ON a.id = rv.creatorId,
    //   JOIN restaurants rt ON rv.restaurantId = rt.id
    //   WHERE rv.creatorId = @accountId;";
    //   return _db.Query<Profile, RestaurantReview, Restaurant, RestaurantReview>(sql, (prof, review, restaurant) =>
    //   {
    //     review.Creator = prof;
    //     review.Restaurant = restaurant;
    //     return review;
    //   }, new { accountId }, splitOn: "id").ToList();
    // }

    internal List<RestaurantReview> GetAllByAccountId(string accountId)
    {
      string sql = @"
      SELECT 
        rv.*,
        rt.*
      FROM reviews rv
      JOIN restaurants rt ON rv.restaurantId = rt.id
      WHERE rv.creatorId = @accountId;";
      return _db.Query<RestaurantReview, Restaurant, RestaurantReview>(sql, (review, restaurant) =>
      {
        review.Restaurant = restaurant;
        return review;
      }, new { accountId }, splitOn: "id").ToList();
    }

    internal Review findExisting(int restaurantId, string creatorId)
    {
      string sql = "SELECT * FROM reviews WHERE restaurantId = @restaurantId AND creatorId = @creatorId;";
      return _db.QueryFirstOrDefault<Review>(sql, new { restaurantId, creatorId });
    }

    public Review GetById(int id)
    {
      string sql = @"
      SELECT
       a.*,
       r.* 
      FROM reviews r
      JOIN accounts a ON a.id = r.creatorId
      WHERE r.id = @id;";
      return _db.Query<Profile, Review, Review>(sql, (prof, review) =>
      {
        review.Creator = prof;
        return review;
      }, new { id }, splitOn: "id").FirstOrDefault();
    }

    public Review Create(Review newData)
    {
      string sql = @"
      INSERT INTO reviews
      (title, body, restaurantId, rating, published, creatorId)
      VALUES
      (@Title, @Body, @RestaurantId, @Rating, @Published, @CreatorId);
      SELECT LAST_INSERT_ID();
      ";
      newData.Id = _db.ExecuteScalar<int>(sql, newData);
      return GetById(newData.Id);

    }

    public Review Edit(Review updatedData)
    {
      string sql = @"
      UPDATE reviews
      SET
        title = @Title,
        body = @Body,
        published = @Published,
        rating = @Rating
      WHERE id = @Id
      ;";
      _db.Execute(sql, updatedData);
      return updatedData;
    }

    public void Delete(int id)
    {
      string sql = "DELETE FROM reviews WHERE id = @id LIMIT 1;";
      _db.Execute(sql, new { id });
    }

  }
}