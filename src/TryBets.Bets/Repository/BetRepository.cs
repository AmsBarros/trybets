using TryBets.Bets.DTO;
using TryBets.Bets.Models;
using Microsoft.EntityFrameworkCore;

namespace TryBets.Bets.Repository;

public class BetRepository : IBetRepository
{
    protected readonly ITryBetsContext _context;
    public BetRepository(ITryBetsContext context)
    {
        _context = context;
    }

    public BetDTOResponse Post(BetDTORequest betRequest, string email)
    {
        var team = _context.Teams.FirstOrDefault(t => t.TeamId == betRequest.TeamId);
        var match = _context.Matches.FirstOrDefault(m => m.MatchId == betRequest.MatchId);
        
        if (team == null)
        {
            throw new ArgumentException("Team not founded");
        }

        if (match == null)
        {
            throw new ArgumentException("Match not founded");
        }

        if (match.MatchFinished)
            throw new ArgumentException("Match finished");

        if (match.MatchTeamAId != betRequest.TeamId && match.MatchTeamBId != betRequest.TeamId)
        {
            throw new ArgumentException("Team is not in this match");
        }

        var bet = new Bet
        {
            UserId = _context.Users.FirstOrDefault(u => u.Email == email)!.UserId,
            MatchId = betRequest.MatchId,
            TeamId = betRequest.TeamId,
            BetValue = betRequest.BetValue,
        };

        _context.Bets.Add(bet);
        _context.SaveChanges();

        return new BetDTOResponse
        {
            BetId = bet.BetId,
            MatchId = bet.MatchId,
            TeamId = bet.TeamId,
            BetValue = bet.BetValue,
            MatchDate = match.MatchDate,
            TeamName = team.TeamName,
            Email = email
        };
    }

    public BetDTOResponse Get(int BetId, string email)
    {
        var bet = _context.Bets.Include(b => b.Match).Include(b => b.Team).FirstOrDefault(b => b.BetId == BetId);
        if (bet == null)
        {
            throw new ArgumentException("Bet not founded");
        }

        var user = _context.Users.FirstOrDefault(u => u.Email == email);
        if (bet.UserId != user!.UserId)
        {
            throw new ArgumentException("Unauthorized");
        }

        return new BetDTOResponse
        {
            BetId = bet.BetId,
            MatchId = bet.MatchId,
            TeamId = bet.TeamId,
            BetValue = bet.BetValue,
            MatchDate = bet.Match!.MatchDate,
            TeamName = bet.Team!.TeamName,
            Email = email
        };
    }
}