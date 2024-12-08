using BlackFriday.Models.Contracts;
using BlackFriday.Repositories;
using BlackFriday.Repositories.Contracts;

namespace BlackFriday.Models
{
    public class Application : IApplication
    {
        private IRepository<IProduct> products;
        private IRepository<IUser> users;
        public Application()
        {
            this.products = new ProductRepository();
            this.users = new UserRepository();
        }

        public IRepository<IProduct> Products => this.products;

        public IRepository<IUser> Users => this.users;
    }
}
