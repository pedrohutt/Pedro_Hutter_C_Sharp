using Domain;
using System.Collections.Generic;

namespace Infraestructure
{
    public class TeamRepository1 : ITeamRepository
    {
        private static List<Team> _teams = new List<Team>();

        public IList<Team> Search(string searchString)
        {
            List<Team> teams = new List<Team>();
            foreach (var team in _teams)
            {
                if (team.Nome.Contains(searchString))
                    teams.Add(team);
            }
            return teams;
        }
        public void Add(Team team)
        {
            if (team != null)
                _teams.Add(team);
        }
    }
}