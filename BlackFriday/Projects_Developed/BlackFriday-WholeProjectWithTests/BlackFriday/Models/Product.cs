using BlackFriday.Models.Contracts;
using BlackFriday.Utilities.Messages;

namespace BlackFriday.Models
{
    public abstract class Product : IProduct
    {
        private string productName;
        private double basePrice;
        private bool isSold;

        public Product(string productName, double basePrice)
        {
            ProductName = productName;
            BasePrice = basePrice;
            IsSold = false;
        }
        public string ProductName
        { 
            get => this.productName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new System.ArgumentException(string.Format(ExceptionMessages.ProductNameRequired));
                }
                this.productName = value;
            }
        }

        public double BasePrice
        {
            get => this.basePrice;
            private set
            {
                if (value <= 0)
                {
                    throw new System.ArgumentException(string.Format(ExceptionMessages.ProductPriceConstraints));
                }
                this.basePrice = value;
            }
        }

        public abstract double BlackFridayPrice { get; }

        public bool IsSold
        {
            get => this.isSold;
            private set
            {
                this.isSold = value;
            }
        }

        public void UpdatePrice(double newPriceValue)
        {
            this.BasePrice = newPriceValue;
        }

        public void ToggleStatus()
        {
            this.IsSold = !this.IsSold;
        }

        public override string ToString()
        {
            return $"Product: {ProductName}, Price: {BasePrice:F2}, You Save: {(BasePrice - BlackFridayPrice):F2}";
        }
    }
}
