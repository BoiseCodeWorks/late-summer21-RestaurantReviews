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
  public class ReviewsController : ControllerBase
  {
    private readonly ReviewsService _service;

    public ReviewsController(ReviewsService service)
    {
      _service = service;
    }
    // FIXME not needed to "get all reviews"
    [HttpGet]
    public ActionResult<List<Review>> Get()
    {
      try
      {
        List<Review> reviews = _service.Get();
        return Ok(reviews);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpGet("{id}")]
    public ActionResult<Review> Get(int id)
    {
      try
      {
        Review review = _service.Get(id);
        return Ok(review);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }


    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Review>> Create([FromBody] Review newReview)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        newReview.CreatorId = userInfo.Id;
        Review review = _service.Create(newReview);
        return Ok(review);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }


    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Review>> Edit([FromBody] Review updatedReview, int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        updatedReview.CreatorId = userInfo.Id;
        updatedReview.Id = id;
        Review review = _service.Edit(updatedReview);
        return Ok(review);
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