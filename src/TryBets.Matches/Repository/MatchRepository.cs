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
                MatchTeamAOdds = CalculateOdds(match.MatchTeamAValue, match.MatchTeamBValue),
                MatchTeamBOdds = CalculateOdds(match.MatchTeamBValue, match.MatchTeamAValue),
                MatchFinished = match.MatchFinished,
                MatchWinnerId = match.MatchWinnerId
            }).OrderBy(match => match.MatchId);
    }

    private static string CalculateOdds(decimal teamValue, decimal opponentTeamValue)
    {
        if (teamValue == 0 || opponentTeamValue == 0)
        {
            return "0.00";
        }

        decimal odds = (teamValue + opponentTeamValue) / teamValue;

        return odds.ToString("F2");
    }
}