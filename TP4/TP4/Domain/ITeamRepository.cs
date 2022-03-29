using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface ITeamRepository
    {
        IList<Team> Search(string searchString);
        IList<Team> Edit(int id, string nome);
        void Add(Team team);
        string ShowTeamInfo(Team team);

    }
}
