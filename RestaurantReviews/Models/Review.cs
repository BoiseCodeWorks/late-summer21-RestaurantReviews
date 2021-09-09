using System;
using System.ComponentModel.DataAnnotations;
using RestaurantReviews.Interfaces;

namespace RestaurantReviews.Models
{
  public class Review : IDbItem<int>
  {
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public int RestaurantId { get; set; }
    public string CreatorId { get; set; }
    public Profile Creator { get; set; }
    // NOTE '?' makes not nullable fields nullable
    [Range(1, 5)]
    public int? Rating { get; set; }
    public bool? Published { get; set; }
  }

  class RestaurantReview : Review
  {
    public Restaurant Restaurant { get; set; }
  }
}