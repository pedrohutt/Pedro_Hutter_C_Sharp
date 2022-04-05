using Domain;
using System.Text.RegularExpressions;

namespace Infraestructure
{
    public class TeamRepositoryInMemory : ITeamRepository
    {
        private static readonly List<Team> _teamsList = new List<Team>();

        public void Insert(Team team)
        {
            var count = _teamsList.Any() ? _teamsList.Max(x => x.Id) + 1 : 1;
            team.Id = count;
            _teamsList.Add(team);
        }

        public void Update(Team team)
        {
            var result = _teamsList.FirstOrDefault(x => x.Id == team.Id);
            if (result != null)
            {
                _teamsList.Remove(result);
                _teamsList.Add(team);
            }
        }

        public void Delete(Team team)
        {
            var result = GetForId(team.Id);

            if (result != null)
            {
                _teamsList.Remove(result);

            }
        }

        public IEnumerable<Team> GetTeams()
        {
            return _teamsList;
        }

        public Team GetForId(int id)
        {
            return _teamsList.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Team> GetName(string name)
        {

            return _teamsList.Where(x => Regex.IsMatch(x.Name, name, RegexOptions.IgnoreCase));
        }

        public IList<Team> GetFiveLast()
        {
            return _teamsList.OrderByDescending(x => x.GetDataRegister()).Take(5).ToList();
        }
    }
}
