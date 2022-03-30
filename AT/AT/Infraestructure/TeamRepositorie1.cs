using Domain;
using System.Collections.Generic;


namespace Infraestructure
{
    public class TeamRepositorie1 : ITeamRepository
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

        public IList<Team> Edit(int i, string nome, byte titulosM, int titulosBR, DateOnly dataCriacao)
        {
            List<Team> editedTeam = new List<Team>();
            _teamsList[i].Nome = nome;
            _teamsList[i].TitulosMundiais = titulosM;
            _teamsList[i].TitulosBrasileiros = titulosBR;
            _teamsList[i].DataCriacao = dataCriacao;
            return editedTeam;
        }

        public IList<Team> Delete(int i)
        {
            var index = _teamsList[i].Id;
            var itemToRemove = _teamsList.SingleOrDefault(r => r.Id == index);
            if (itemToRemove != null)
                _teamsList.Remove(itemToRemove);
            return _teamsList;
        }

        public IList<Team> ShowAllTeams()
        {
            int index = 0;
            List<Team> teams = new List<Team>();
            foreach (var team in _teamsList)
            {
                Console.WriteLine($"{index} - {team.Nome}");
                index++;
            }
            return teams;
        }
        public string ShowTeamInfo(Team team)
        {
            string teamInfo = $"\n Nome do Time: {team.Nome}" +
                              $"\n Titulos Brasileiros {team.TitulosBrasileiros} - Titulos Mundiais: {team.TitulosMundiais}" +
                              $"\n O Clube já possui {team.tempoAtivo()} anos de história!\n" +
                              $"\n GUID - {team.Id}\n";
            return teamInfo;
        }
        
        static void BackToMenu()
        {
            Console.WriteLine("Data inválida! Dados descartados! Pressione qualquer tecla para exibir o menu principal ...");
            Console.ReadKey();
        }

    }
}