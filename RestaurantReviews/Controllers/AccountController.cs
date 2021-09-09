using System;
using System.Threading.Tasks;
using RestaurantReviews.Models;
using RestaurantReviews.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace RestaurantReviews.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class AccountController : ControllerBase
  {
    private readonly AccountService _accountService;

    public AccountController(AccountService accountService)
    {
      _accountService = accountService;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<Account>> Get()
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_accountService.GetOrCreateProfile(userInfo));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("reviews")]
    [Authorize]
    public async Task<ActionResult<Account>> GetReviews()
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        List<RestaurantReview> reviews = _accountService.GetReviews(userInfo.Id);
        return Ok(reviews);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }


}