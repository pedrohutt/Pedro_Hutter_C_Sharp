using Domain;
using System.Collections.Generic;


namespace Infraestructure
{
    public class TeamRepositorie1 : ITeamRepository
    {
        public static List<Teams> _teams = new List<Teams>();

        public IList<Teams> Search(string searchString)
        {
            List<Teams> teams = new List<Teams>();
            foreach (var team in _teams)
            {
                if (team.Nome.Contains(searchString))
                    teams.Add(team);
            }
            return teams;
        }
        public void Add(Teams team)
        {
            if (team != null)
                _teams.Add(team);
        }
        public static string ShowTeamInfo(Teams team)
        {
            string teamInfo = $"\n Nome do Time: {team.Nome}" +
                              $"\n Titulos Brasileiros {team.TitulosBrasileiros} - Titulos Mundiais: {team.TitulosMundiais}" +
                              $"\n O Clube já possui {team.tempoAtivo()} anos de história!\n";
            return teamInfo;
        }
        
        static void BackToMenu()
        {
            Console.WriteLine("Data inválida! Dados descartados! Pressione qualquer tecla para exibir o menu principal ...");
            Console.ReadKey();
        }

    }
}