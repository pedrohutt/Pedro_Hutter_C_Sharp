
namespace Domain
{
    public interface ITeamRepository
    {
        IEnumerable<Team> Search(string searchString);
        Team GetById(int id);
        IList<Team> GetFiveLast();
        IEnumerable<Team> GetAllTeams(); 
        void Delete(Team team);
        void Insert(Team team);
        void Update(Team team);
    }
}
