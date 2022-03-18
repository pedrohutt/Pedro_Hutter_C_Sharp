using Domain;

namespace Infraestructure
{
    public class TeamsRepositorie
    {
        private static List<Teams> ListaTeams = new List<Teams>();

        public static void TeamRegister(Teams team)
        {
            if (team != null)
                ListaTeams.Add(team);
        }
        public static List<Teams> SearchTeam(string searchstring)
        {
            List<Teams> teams = new List<Teams>();
            foreach (var team in ListaTeams)
            {
                if (team.Nome.Contains(searchstring))
                    teams.Add(team);
            }
            return teams;
        }
        public static string ShowTeamInfo(Teams team)
        {
            string teamInfo = $"\nNome do Time: {team.Nome} - Estado: {team.Estado}" +
                $"\n Titulos Estaduais {team.TitulosEstaduais} - Titulos Brasileiros {team.TitulosBrasileiros}" +
                $"\n O Clube já possui {team.tempoAtivo()} anos de história!\n";
            return teamInfo;
        }
    }
}