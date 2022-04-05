using Domain;
using System.Text.RegularExpressions;

namespace Infraestructure
{
    public class TeamRepositorieInText : ITeamRepository
    {
        public List<Team> _teamsList = new List<Team>();

        private readonly string _sourceFile = "TeamText.txt";
        private string? _dir = Directory.GetCurrentDirectory();

        public void ReadFile()
        {
            var teams = new List<Team>();
            var path = $@"{_sourceFile}";

            if (File.Exists(path))
            {
                using (var openFile = File.OpenRead(path))
                {
                    using (var readText = new StreamReader(openFile))
                    {
                        while (readText.EndOfStream == false)
                        {
                            var read = readText.ReadLine();

                            if (read != null)
                            {
                                string[] aux = read.Split('|');
                                DateOnly date = DateOnly.ParseExact(aux[4], "dd/MM/yyyy", null);
                                teams.Add(new Team(int.Parse(aux[0]), aux[1],byte.Parse(aux[2]),short.Parse(aux[3]),date));
                            }
                        }
                        _teamsList = teams;
                    }
                }
            }
        }

        public void Save()
        {
            string path = $@"{_sourceFile}";
            var Source = new List<string>();

            foreach (Team team in _teamsList)
            {
                Source.Add(team.ToCSV());
            }

            if (File.Exists(path))
            {
                File.WriteAllLines(path, Source);
                ReadFile();
            }
        }
        public void Insert(Team team)
        {
            var count = _teamsList.Any() ? _teamsList.Max(x => x.Id) + 1 : 1;
            team.Id = count;
            _teamsList.Add(team);
            Save();
        }
        public void Update(Team team)
        {
            var result = _teamsList.FirstOrDefault(x => x.Id == team.Id);
            if (result != null)
            {
                _teamsList.Remove(result);
                _teamsList.Add(team);
                Save();
            }
        }

        public void Delete(Team team)
        {
            var result = GetById(team.Id);

            if (result != null)
            {
                _teamsList.Remove(result);
                Save();
            }
        }
        public Team GetById(int id)
        {
            return _teamsList.FirstOrDefault(x => x.Id == id);
        }
        public IEnumerable<Team> GetAllTeams()
        {
            return _teamsList;
        }
        public IList<Team> GetFiveLast()
        {
            return _teamsList.OrderByDescending(x => x.GetDataRegister()).Take(5).ToList();
        }

        public IEnumerable<Team> Search(string searchString)
        {
            return _teamsList.Where(x => Regex.IsMatch(
                x.Nome, searchString, RegexOptions.IgnoreCase));
        }
    }
}
