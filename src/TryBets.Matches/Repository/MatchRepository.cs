using TryBets.Matches.DTO;
using System.Collections.Generic;
using System.Linq;
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
       return (from match in _context.Matches
                where match.MatchFinished == matchFinished
                orderby match.MatchId
                join teamA in _context.Teams on match.MatchTeamAId equals teamA.TeamId
                join teamB in _context.Teams on match.MatchTeamBId equals teamB.TeamId
                select new MatchDTOResponse {
                    MatchId = match.MatchId,
                    MatchDate = match.MatchDate,
                    MatchTeamAId = match.MatchTeamAId,
                    MatchTeamBId = match.MatchTeamBId,
                    TeamAName = teamA.TeamName,
                    TeamBName = teamB.TeamName,
                    MatchTeamAOdds = $"{match.MatchTeamAValue + match.MatchTeamBValue / match.MatchTeamAValue}",
                    MatchTeamBOdds = $"{match.MatchTeamBValue + match.MatchTeamAValue / match.MatchTeamBValue}",
                    MatchFinished = match.MatchFinished,
                    MatchWinnerId = match.MatchWinnerId
                }).ToList();
    }
}