using System;
using System.Collections.Generic;
using RestaurantReviews.Models;
using RestaurantReviews.Repositories;

namespace RestaurantReviews.Services
{
  public class ReviewsService
  {
    private readonly ReviewsRepository _repo;

    public ReviewsService(ReviewsRepository repo)
    {
      _repo = repo;
    }

    internal List<Review> Get()
    {
      return _repo.GetAll();
    }

    internal List<Review> GetByRestaurantId(int restaurantId)
    {
      return _repo.GetAll(restaurantId);
    }

    internal Review Create(Review newReview)
    {
      // Find if one already exists
      Review found = _repo.findExisting(newReview.RestaurantId, newReview.CreatorId);
      if (found != null)
      {
        throw new Exception("You have already reviewed this restaurant");
      }
      return _repo.Create(newReview);
    }

    internal Review Get(int id)
    {
      Review found = _repo.GetById(id);
      if (found == null)
      {
        throw new Exception("Invalid Id");
      }
      return found;
    }

    internal void Delete(int restaurantId, string userId)
    {
      Review toDelete = Get(restaurantId);
      if (toDelete.CreatorId != userId)
      {
        throw new Exception("Thats not your restaurant");
      }
      _repo.Delete(restaurantId);
    }

    internal Review Edit(Review updatedReview)
    {
      Review original = Get(updatedReview.Id);
      if (original.CreatorId != updatedReview.CreatorId)
      {
        throw new Exception("Hands off Buddy");
      }
      // NOTE these are the same kind of evaluation ('??' Null Coalesing Operator)
      original.Body = updatedReview.Body ?? original.Body;
      original.Rating = updatedReview.Rating ?? original.Rating;
      original.Published = updatedReview.Published ?? original.Published;
      original.Title = updatedReview.Title ?? original.Title;
      _repo.Edit(original);
      return original;
    }
  }
}