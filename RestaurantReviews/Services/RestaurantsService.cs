using System;
using System.Collections.Generic;
using RestaurantReviews.Models;
using RestaurantReviews.Repositories;

namespace RestaurantReviews.Services
{
  public class RestaurantsService
  {
    private readonly RestaurantsRepository _repo;

    public RestaurantsService(RestaurantsRepository repo)
    {
      _repo = repo;
    }

    internal List<Restaurant> Get()
    {
      return _repo.GetAll();
    }

    internal Restaurant Create(Restaurant newRestaurant)
    {
      return _repo.Create(newRestaurant);
    }

    internal Restaurant Get(int id)
    {
      Restaurant found = _repo.GetById(id);
      if (found == null)
      {
        throw new Exception("Invalid Id");
      }
      return found;
    }

    internal void Delete(int restaurantId, string userId)
    {
      Restaurant toDelete = Get(restaurantId);
      if (toDelete.CreatorId != userId)
      {
        throw new Exception("Thats not your restaurant");
      }
      _repo.Delete(restaurantId);
    }

    internal Restaurant Edit(Restaurant updatedRestaurant)
    {
      Restaurant original = Get(updatedRestaurant.Id);
      if (original.CreatorId != updatedRestaurant.CreatorId)
      {
        throw new Exception("Hands off Buddy");
      }
      // NOTE these are the same kind of evaluation ('??' Null Coalesing Operator)
      original.Name = updatedRestaurant.Name != null ? updatedRestaurant.Name : original.Name;
      original.Location = updatedRestaurant.Location ?? original.Location;
      _repo.Edit(original);
      return original;
    }
  }
}