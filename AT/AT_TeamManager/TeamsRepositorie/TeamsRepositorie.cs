using Domain;

namespace Repositorie
{
    public class TeamsRepositorie
    {
        private static List<Teams> ListaTeams = new List<Teams>();

        public static void RegisterTeam(Teams team)
        {
            if (team != null)
            {
                ListaTeams.Add(team);
            }
        }
        public static List<Teams> SearchTeam(string searchString)
        {
            List<Teams> searchResult = new List<Teams>();
            foreach (var team in ListaTeams)
            {
                if (team.Nome.Contains(searchString))
                {
                    searchResult.Add(team);
                }
            }
            return searchResult;
        }
        public static string ShowTeamInfo(Teams team)
        {
            string teamInfo = $"\nNome do Time: {team.Nome} - Estado: {team.Estado}" +
                $"\n Titulos Estaduais {team.TitulosEstaduais} - Titulos Brasileiros {team.TitulosBrasileiros}" +
                $"\n O Clube já possui {team.TempoAtivo()} anos de história!\n" +
                $"\n O Código Guid do Time é {team.Id}";
            return teamInfo;
        }
    }
}