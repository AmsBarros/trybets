using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;
using TryBets.Bets.Repository;
using TryBets.Bets.DTO;
using TryBets.Bets.Services;

namespace TryBets.Bets.Controllers;

[Route("[controller]")]
public class BetController : Controller
{
    private readonly IBetRepository _repository;
    private readonly IOddService _oddService;
    public BetController(IBetRepository repository, IOddService oddService)
    {
        _repository = repository;
        _oddService = oddService;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(Policy = "Client")]
    public async Task<IActionResult> Post([FromBody] BetDTORequest request)
    {
        var token = HttpContext.User.Identity as ClaimsIdentity;
        var email = token?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

        if (token == null || email == null)
        {
            return Unauthorized();
        }

        try
        {
            var bet = _repository.Post(request, email);
            await _oddService.UpdateOdd(request.MatchId, request.TeamId, request.BetValue);

            return Created("Bet", bet);
        }
        catch (Exception error)
        {
            return BadRequest(new { message = error.Message });
        }
    }

    [HttpGet("{BetId}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(Policy = "Client")]
    public IActionResult Get(int BetId)
    {
        var token = HttpContext.User.Identity as ClaimsIdentity;
        var email = token?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

        if (token == null || email == null)
        {
            return Unauthorized();
        }

        try
        {
            return Ok(_repository.Get(BetId, email));
        }
        catch (Exception error)
        {
            return BadRequest(new { message = error.Message });
        }
    }
}