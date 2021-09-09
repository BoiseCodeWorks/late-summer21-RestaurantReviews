using System;
using RestaurantReviews.Interfaces;

namespace RestaurantReviews.Models
{
  public class Account : Profile, IDbItem<string>
  {
    public string Email { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
  }
}