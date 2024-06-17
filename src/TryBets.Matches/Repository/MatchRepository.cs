using TryBets.Matches.DTO;
using TryBets.Matches.Models;

namespace TryBets.Matches.Repository;

public class MatchRepository : IMatchRepository
{
    protected readonly ITryBetsContext _context;
    public MatchRepository(ITryBetsContext context)
    {
        _context = context;
    }

    public IEnumerable<MatchDTOResponse> Get(bool matchFinished)
    {
        return _context.Matches
            .Where(match => match.MatchFinished == matchFinished)
            .Select(match => new MatchDTOResponse
            {
                MatchId = match.MatchId,
                MatchDate = match.MatchDate,
                MatchTeamAId = match.MatchTeamAId,
                MatchTeamBId = match.MatchTeamBId,
                TeamAName = match.MatchTeamA!.TeamName,
                TeamBName = match.MatchTeamB!.TeamName,
                MatchFinished = match.MatchFinished,
                MatchWinnerId = match.MatchWinnerId
            }).OrderBy(match => match.MatchId);
    }
}