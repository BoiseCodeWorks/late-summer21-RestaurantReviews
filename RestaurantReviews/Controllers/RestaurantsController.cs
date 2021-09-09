using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantReviews.Models;
using RestaurantReviews.Services;

namespace RestaurantReviews.Controllers
{
  [ApiController]
  [Route("/api/[controller]")]
  public class RestaurantsController : ControllerBase
  {
    private readonly RestaurantsService _service;
    private readonly ReviewsService _reviewsService;

    public RestaurantsController(RestaurantsService service, ReviewsService reviewsService)
    {
      _service = service;
      _reviewsService = reviewsService;
    }

    [HttpGet]
    public ActionResult<List<Restaurant>> Get()
    {
      try
      {
        List<Restaurant> restaurants = _service.Get();
        return Ok(restaurants);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpGet("{id}")]
    public ActionResult<Restaurant> Get(int id)
    {
      try
      {
        Restaurant restaurant = _service.Get(id);
        return Ok(restaurant);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpGet("{id}/reviews")]
    public ActionResult<List<Review>> GetReviews(int id)
    {
      try
      {
        List<Review> reviews = _reviewsService.GetByRestaurantId(id);
        return Ok(reviews);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }


    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Restaurant>> Create([FromBody] Restaurant newRestaurant)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        newRestaurant.CreatorId = userInfo.Id;
        Restaurant restaurant = _service.Create(newRestaurant);
        return Ok(restaurant);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }


    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Restaurant>> Edit([FromBody] Restaurant updatedRestaurant, int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        updatedRestaurant.CreatorId = userInfo.Id;
        updatedRestaurant.Id = id;
        Restaurant restaurant = _service.Edit(updatedRestaurant);
        return Ok(restaurant);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }


    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<String>> Delete(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _service.Delete(id, userInfo.Id);
        return Ok("DELORTED");
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }
  }
}