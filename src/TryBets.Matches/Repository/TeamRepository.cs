using TryBets.Matches.DTO;

namespace TryBets.Matches.Repository;

public class TeamRepository : ITeamRepository
{
    protected readonly ITryBetsContext _context;
    public TeamRepository(ITryBetsContext context)
    {
        _context = context;
    }

    public IEnumerable<TeamDTOResponse> Get()
    {
        return (from team in _context.Teams
                select new TeamDTOResponse {
                    TeamId = team.TeamId,
                    TeamName = team.TeamName
                }).ToList();
    }
}