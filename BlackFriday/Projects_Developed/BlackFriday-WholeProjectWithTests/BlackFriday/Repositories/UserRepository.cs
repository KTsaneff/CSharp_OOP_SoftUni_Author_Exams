using BlackFriday.Models.Contracts;
using BlackFriday.Repositories.Contracts;

namespace BlackFriday.Repositories
{
    public class UserRepository : IRepository<IUser>
    {
        private readonly List<IUser> models;
        public UserRepository()
        {
            this.models = new List<IUser>();
        }
        public IReadOnlyCollection<IUser> Models => this.models;

        public void AddNew(IUser model)
        {
            this.models.Add(model);
        }

        public IUser GetByName(string name)
        {
            return this.models.FirstOrDefault(u => u.UserName == name);
        }

        public bool Exists(string name)
        {
            return this.models.Any(u => u.UserName == name);
        }
    }
}
