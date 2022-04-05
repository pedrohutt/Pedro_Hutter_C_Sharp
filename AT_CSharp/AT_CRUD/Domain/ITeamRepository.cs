namespace Domain
{
    public interface ITeamRepository 
    {
        IEnumerable<Team> GetTeams();
        IList<Team> GetFiveLast();
        Team GetForId(int id);
        IEnumerable<Team> GetName(string name);
        void Insert(Team team);
        void Update(Team team);
        void Delete(Team team); 
    }
}