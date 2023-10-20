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
       decimal BetValueDecimal = Decimal.Parse(BetValueConverted, CultureInfo.InvariantCulture);
       Match match = (from m in _context.Matches
                    where m.MatchId == MatchId
                    select m).FirstOrDefault();
        if (match == null) throw new Exception("Match not found");
        if (match.MatchTeamAId == TeamId) match.MatchTeamAValue += BetValueDecimal;
        else if (match.MatchTeamBId == TeamId) match.MatchTeamBValue += BetValueDecimal;
        else throw new Exception("Team not found");

        return match;

    }
}