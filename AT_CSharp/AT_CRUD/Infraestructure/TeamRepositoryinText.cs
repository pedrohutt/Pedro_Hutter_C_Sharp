using Domain;
using System.Text;
using System.Text.RegularExpressions;

namespace Infraestructure
{
    public class TeamRepositoryInText : ITeamRepository
    {
        public List<Team> _teamRepositorie = new List<Team>();

        private readonly string dir;
        private readonly string file = "TeamTextFile.txt";

        public TeamRepositoryInText()
        {

            dir = Directory.GetCurrentDirectory();
            CreateFiles();
            ReadFiles();
        }


        public void ReadFiles()
        {
            var donations = new List<Team>();

            var path = $@"{dir}\{file}";
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
                                donations.Add(new Team(int.Parse(aux[0]),aux[1],short.Parse(aux[2]),byte.Parse(aux[3]),date));
                            }
                        } _teamRepositorie = donations;
                    }
                }
            }
        }
        private void CreateFiles()
        {
            var path = $@"{dir}\{file}";

            if (!File.Exists(path))
            {
                File.Create(path);
            }
        }


        public void Save()
        {
            string fileRoute = $@"{dir}\{file}";
            var way = new List<string>();

            foreach (Team team in _teamRepositorie)
            {
                way.Add(team.ToString());
            }

            if (File.Exists(fileRoute))
            {
                File.WriteAllLines(fileRoute, way, Encoding.UTF8);
                ReadFiles();
            }
        }


        public void Insert(Team team)
        {
            var count = _teamRepositorie.Any() ? _teamRepositorie.Max(x => x.Id) + 1 : 1;
            team.Id = count;
            _teamRepositorie.Add(team);
            Save();

        }
        public void Update(Team team)
        {
            var result = _teamRepositorie.FirstOrDefault(x => x.Id == team.Id);
            if (result != null)
            {
                _teamRepositorie.Remove(result);
                _teamRepositorie.Add(team);
                Save();
            }
        }
        public void Delete(Team team)
        {
            var result = GetForId(team.Id);

            if (result != null)
            {
                _teamRepositorie.Remove(result);
                Save();
            }
        }
        public IEnumerable<Team> GetTeams()
        {
            return _teamRepositorie;
        }

        public Team GetForId(int id)
        {
            return _teamRepositorie.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Team> GetName(string name)
        {
            return _teamRepositorie.Where(x => Regex.IsMatch(x.Name, name, RegexOptions.IgnoreCase));
        }

        public IList<Team> GetFiveLast()
        {
            return _teamRepositorie.OrderByDescending(x => x.GetDataRegister()).Take(5).ToList();
        }
    }
}
