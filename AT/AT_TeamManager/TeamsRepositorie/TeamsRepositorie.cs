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

        public void DeleteTeam(Guid id)
        {
            foreach (var team in ListaTeams)
            {
                if (team.Id == id)
                {
                    ListaTeams.Remove(team);
                }
            }
        }

        public void EditTeam(Guid id, string nome, string estado, int titulosBR, int titulosES, DateOnly criacao)
        {
            foreach(var team in ListaTeams)
            {
                if (team.Id == id)
                {
                    team.Nome = nome;
                    team.Estado = estado;
                    team.TitulosBrasileiros = titulosBR;
                    team.TitulosEstaduais = titulosES;
                    team.DataCriacao = criacao;
                }
            }
        }

        public static string ShowTeamInfo(Teams team)
        {
            string teamInfo = $"\n Nome do Time: {team.Nome} - Estado: {team.Estado}" +
                $"\n Titulos Estaduais {team.TitulosEstaduais} - Titulos Brasileiros {team.TitulosBrasileiros}" +
                $"\n O Clube já possui {team.TempoAtivo()} anos de história!\n" +
                $"\n O Código Guid do Time é {team.Id}";
            return teamInfo;
        }
    }
}