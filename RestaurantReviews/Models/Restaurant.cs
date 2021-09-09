using System;
using RestaurantReviews.Interfaces;

namespace RestaurantReviews.Models
{
  public class Restaurant : IDbItem<int>
  {
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public string CreatorId { get; set; }
  }
}