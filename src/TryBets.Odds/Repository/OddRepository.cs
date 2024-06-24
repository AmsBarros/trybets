using TryBets.Odds.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Globalization;

namespace TryBets.Odds.Repository;

public class OddRepository : IOddRepository
{
    protected readonly ITryBetsContext _context;
    public OddRepository(ITryBetsContext context)
    {
        _context = context;
    }

    public Match Patch(int MatchId, int TeamId, string BetValue)
    {
        string BetValueConverted = BetValue.Replace(',', '.');
        decimal BetValueDecimal = decimal.Parse(BetValueConverted, CultureInfo.InvariantCulture);
        
        var match = _context.Matches.FirstOrDefault(m => m.MatchId == MatchId);
        var team = _context.Teams.FirstOrDefault(t => t.TeamId == TeamId);

        if (match == null)
        {
            throw new ArgumentException("Match not found");
        }

        if (team == null)
        {
            throw new ArgumentException("Team not found");
        }

        if (match.MatchTeamAId != TeamId && match.MatchTeamBId != TeamId)
        {
            throw new ArgumentException("Team is not in this match");
        }

        if (team.TeamId == match.MatchTeamAId)
        {
            match.MatchTeamAValue += BetValueDecimal;
        }
        else
        {
            match.MatchTeamBValue += BetValueDecimal;
        }

        _context.SaveChanges();

        return match; 
    }
}