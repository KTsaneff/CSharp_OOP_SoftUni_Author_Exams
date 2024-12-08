using BlackFriday.Core.Contracts;
using BlackFriday.Models;
using BlackFriday.Models.Contracts;
using BlackFriday.Utilities.Messages;
using System.Text;

namespace BlackFriday.Core
{
    public class Controller : IController
    {
        private IApplication application = new Application();

        public string RegisterUser(string userName, string email, bool hasDataAccess)
        {
            if (application.Users.Exists(userName))
            {
                return string.Format(OutputMessages.UserAlreadyRegistered, userName);
            }
            if(application.Users.Models.Any(u => u.Email == email))
            {
                return string.Format(OutputMessages.SameEmailIsRegistered, email);
            }
            if (hasDataAccess)
            {
                if(application.Users.Models.Where(u => u.HasDataAccess).Count() == 2)
                {
                    return string.Format(OutputMessages.AdminCountLimited);
                }
                else
                {
                    IUser admin = new Admin(userName, email);
                    application.Users.AddNew(admin);

                    return string.Format(OutputMessages.AdminRegistered, userName);
                }
            }
            else
            {
                IUser client = new Client(userName, email);
                application.Users.AddNew(client);

                return string.Format(OutputMessages.ClientRegistered, userName);
            }
        }

        public string AddProduct(string productType, string productName, string userName, double basePrice)
        {
            if(productType != nameof(Item) && productType != nameof(Service))
            {
                return string.Format(OutputMessages.ProductIsNotPresented, productType);
            }
            if (application.Products.Exists(productName))
            {
                return string.Format(OutputMessages.ProductNameDuplicated, productName);
            }
            if (!application.Users.Exists(userName) || application.Users.GetByName(userName).GetType().Name == nameof(Client))
            {
                return string.Format(OutputMessages.UserIsNotAdmin, userName);
            }

            IProduct product;

            if(productType == nameof(Item))
            {
                product = new Item(productName, basePrice);
            }
            else
            {
                product = new Service(productName, basePrice);
            }
            application.Products.AddNew(product);

            return string.Format(OutputMessages.ProductAdded, productType, productName, basePrice.ToString("F2"));
        }

        public string UpdateProductPrice(string productName, string userName, double newPriceValue)
        {
            if(!application.Products.Exists(productName))
            {
                return string.Format(OutputMessages.ProductDoesNotExist, productName);
            }
            if (!application.Users.Exists(userName) || application.Users.GetByName(userName).GetType().Name == nameof(Client))
            {
                return string.Format(OutputMessages.UserIsNotAdmin, userName);
            }

            IProduct product = application.Products.GetByName(productName);
            double oldPriceValue = product.BasePrice;
            product.UpdatePrice(newPriceValue);

            return string.Format(OutputMessages.ProductPriceUpdated, productName, oldPriceValue.ToString("F2"), newPriceValue.ToString("F2"));
        }      

        public string PurchaseProduct(string userName, string productName, bool blackFridayFlag)
        {
            if (!application.Users.Exists(userName) || application.Users.GetByName(userName).GetType().Name != nameof(Client))
            {
                return string.Format(OutputMessages.UserIsNotClient, userName);
            }
            if (!application.Products.Exists(productName))
            {
                return string.Format(OutputMessages.ProductDoesNotExist, productName);
            }
            if (application.Products.GetByName(productName).IsSold)
            {
                return string.Format(OutputMessages.ProductOutOfStock, productName);
            }

            IProduct product = application.Products.GetByName(productName);
            Client client = (Client)application.Users.GetByName(userName);

            client.PurchaseProduct(product.ProductName, blackFridayFlag);
            product.ToggleStatus();

            if(blackFridayFlag)
            {
                return string.Format(OutputMessages.ProductPurchased, userName, productName, product.BlackFridayPrice.ToString("F2"));
            }
            else
            {
                return string.Format(OutputMessages.ProductPurchased, userName, productName, product.BasePrice.ToString("F2"));
            }
        }

        public string RefreshSalesList(string userName)
        {
            if (!application.Users.Exists(userName) || application.Users.GetByName(userName).GetType().Name == nameof(Client))
            {
                return string.Format(OutputMessages.UserIsNotAdmin, userName);
            }

            int count = application.Products.Models.Where(p => p.IsSold).Count();

            foreach (var product in application.Products.Models)
            {
                if (product.IsSold)
                {
                    product.ToggleStatus();
                }
            }

            return string.Format(OutputMessages.SalesListRefreshed, count);

        }

        public string ApplicationReport()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Application administration:");
            var admins = application.Users.Models.Where(u => u.GetType().Name == nameof(Admin));

            foreach (var admin in admins.OrderBy(a => a.UserName))
            {
                sb.AppendLine(admin.ToString());
            }

            sb.AppendLine("Clients:");
            var clients = application.Users.Models.Where(u => u.GetType().Name == nameof(Client)).Select(x => (Client)x).OrderBy(c => c.UserName);

            foreach (var client in clients)
            {
                sb.AppendLine(client.ToString());
                if(client.Purchases.Any(p => p.Value == true))
                {
                    var purchasedPromotions = client.Purchases.Where(p => p.Value == true);
                    sb.AppendLine($"-Black Friday Purchases: {purchasedPromotions.Count()}");
                    foreach (var product in purchasedPromotions)
                    {
                        sb.AppendLine("--" + product.Key);
                    }
                }
            }

            return sb.ToString().TrimEnd();
        }        
    }
}
