using Microsoft.AspNetCore.Mvc;
using TryBets.Odds.Repository;

namespace TryBets.Odds.Controllers;

[Route("[controller]")]
public class OddController : Controller
{
    private readonly IOddRepository _repository;
    public OddController(IOddRepository repository)
    {
        _repository = repository;
    }

    [HttpPatch("{MatchId:int}/{TeamId:int}/{BetValue}")]
    public IActionResult Patch(int MatchId, int TeamId, string BetValue)
    {
        try
        {
            return Ok(_repository.Patch(MatchId, TeamId, BetValue));
        }
        catch (Exception error)
        {
            return BadRequest(new { message = error.Message });
        }
    }
}