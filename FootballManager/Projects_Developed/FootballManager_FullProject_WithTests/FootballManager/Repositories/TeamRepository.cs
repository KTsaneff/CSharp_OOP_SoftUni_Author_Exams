using FootballManager.Models.Contracts;
using FootballManager.Repositories.Contracts;

namespace FootballManager.Repositories
{
    public class TeamRepository : IRepository<ITeam>
    {
        private readonly List<ITeam> models;
        private int capacity = 10;

        public TeamRepository()
        {
            this.models = new List<ITeam>();

        }
        public IReadOnlyCollection<ITeam> Models => this.models.AsReadOnly();

        public int Capacity => this.capacity;

        public void Add(ITeam model)
        {
            if (this.models.Count < this.capacity)
            {
                this.models.Add(model);
            }
        }

        public bool Exists(string name)
        {
            return this.models.Any(x => x.Name == name);
        }

        public ITeam Get(string name)
        {
            return this.models.FirstOrDefault(x => x.Name == name);
        }

        public bool Remove(string name)
        {
            ITeam model = this.models.FirstOrDefault(x => x.Name == name);
            if (model == null)
            {
                return false;
            }
            this.models.Remove(model);
            return true;
        }
    }
}
