#nullable disable
using Domain;


namespace Infraestructure
{
    public class TeamRepositorieInMemory 
    {
        public List<Team> _teamsList = new List<Team>();

        public IList<Team> Search(string searchString)
        {
            List<Team> teams = new List<Team>();
            foreach (var team in _teamsList)
            {
                if (team.Nome.Contains(searchString))
                    teams.Add(team);
            }
            return teams;
        }

        public void Add(Team team)
        {   
            if (team != null)
            _teamsList.Add(team);
        }

        public void Update(Team team, int i)
        {
            var index = _teamsList[i].Id;
            var result = _teamsList.FirstOrDefault(x => x.Id== index);
            if (result != null)
            {
                _teamsList.Remove(result);
                _teamsList.Add(team);
            }
        }

        public void Delete(int i)
        {
            var index = _teamsList[i].Id;
            var itemToRemove = _teamsList.SingleOrDefault(r => r.Id == index);
            if (itemToRemove != null)
                _teamsList.Remove(itemToRemove);
        }


        public IList<Team> GetFiveLast()
        {
            return _teamsList.OrderByDescending(x => x.Nome).Take(5).ToList();
        }

        public IEnumerable<Team> GetAllTeams()
        {
            int index = 0;
            List<Team> allTeams = new List<Team>();
            foreach (var team in _teamsList)
            {
                allTeams.Add(team);
                index++;
            }                
            return allTeams;
        }

        public IList<Team> GetTeams(string nome)
        {
            throw new NotImplementedException();
        }

        public void Delete(string nome)
        {
            throw new NotImplementedException();
        }
    }
}