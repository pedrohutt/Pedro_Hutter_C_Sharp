using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infraestrutura
{
    public class EntidadeRepositorio
    {
        private static List<Teams> ListaTeams = new List<Teams>();
        
        public static void TeamRegister(Teams team)
        {
            if(team != null)
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
    }
}