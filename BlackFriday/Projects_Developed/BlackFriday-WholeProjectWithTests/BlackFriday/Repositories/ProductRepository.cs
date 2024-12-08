using BlackFriday.Models.Contracts;
using BlackFriday.Repositories.Contracts;

namespace BlackFriday.Repositories
{
    public class ProductRepository : IRepository<IProduct>
    {
        private readonly List<IProduct> models;

        public ProductRepository()
        {
            this.models = new List<IProduct>();
        }

        public IReadOnlyCollection<IProduct> Models => this.models;

        public void AddNew(IProduct model)
        {
            this.models.Add(model);
        }

        public bool Exists(string name) => this.models.Any(p => p.ProductName == name);

        public IProduct GetByName(string name) => this.models.FirstOrDefault(p => p.ProductName == name);
    }
}
