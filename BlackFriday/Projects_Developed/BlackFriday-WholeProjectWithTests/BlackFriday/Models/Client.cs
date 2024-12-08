namespace BlackFriday.Models
{
    public class Client : User
    {
        private Dictionary<string, bool> purchases;
        public Client(string userName, string email) 
            : base(userName, email, false)
        {
            this.purchases = new Dictionary<string, bool>();
        }

        public IReadOnlyDictionary<string, bool> Purchases => this.purchases;

        public void PurchaseProduct(string productName, bool blackFridayFlag)
        {
            this.purchases[productName] = blackFridayFlag;
        }
    }
}
