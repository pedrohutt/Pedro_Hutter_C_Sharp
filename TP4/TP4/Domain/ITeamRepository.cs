using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface ITeamRepository
    {
        IList<Teams> Search(string termoDePesquisa);
        void Add(Teams team);
    }
}
