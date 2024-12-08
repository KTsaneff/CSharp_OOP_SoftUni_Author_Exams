using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace NetTraderSystem.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Constructor_ShouldInitializeEmptyProductsCollection()
        {
            int inventoryLimit = 10;

            TradingPlatform platform = new TradingPlatform(inventoryLimit);

            Assert.IsNotNull(platform.Products, "Products collection should not be null.");
            Assert.IsEmpty(platform.Products, "Products collection should be initialized as empty.");
        }

        [Test]
        public void AddProduct_ShouldPrevent_AddingAProduct_WhenCapacityIsFull()
        {

            int inventoryLimit = 1;
            Product product1 = new Product("Product1", "Category1", 100);
            Product product2 = new Product("Product2", "Category2", 200);

            TradingPlatform platform = new TradingPlatform(inventoryLimit);
            platform.AddProduct(product1);

            string result = platform.AddProduct(product2);

            Assert.That(result, Is.EqualTo("Inventory is full"), "Adding a product to a full inventory should return 'Inventory is full'.");
            Assert.That(platform.Products.Count, Is.EqualTo(1), "Products count should not exceed the inventory limit.");
        }

        [Test]
        public void Validate_AddProduct_IsActuallyAddedInTheCollection()
        {
            int inventoryLimit = 2;
            Product product = new Product("Laptop", "Electronics", 1500);
            TradingPlatform platform = new TradingPlatform(inventoryLimit);

            string result = platform.AddProduct(product);

            Assert.That(result, Is.EqualTo("Product Laptop added successfully"));
            Assert.That(platform.Products.Count, Is.EqualTo(1), "Products count should reflect the added product.");
            Assert.That(platform.Products.Contains(product), Is.True, "The product should exist in the collection after being added.");
        }

        [Test]
        public void Validate_AddProduct_ReturnsCorrectMesssage_WhenInventoryIsFull()
        {
            int inventoryLimit = 1;
            Product product1 = new Product("Product1", "Category1", 100);
            Product product2 = new Product("Product2", "Category2", 200);

            TradingPlatform platform = new TradingPlatform(inventoryLimit);
            platform.AddProduct(product1);

            string result = platform.AddProduct(product2);

            Assert.That(result, Is.EqualTo("Inventory is full"), "Adding a product to a full inventory should return 'Inventory is full'.");
        }

        [Test]
        public void RemoveProduct_ShouldReturnFalse_WhenProductDoesNotExist()
        {
            int inventoryLimit = 1;
            Product product1 = new Product("Product1", "Category1", 100);
            Product product2 = new Product("Product2", "Category2", 200);

            TradingPlatform platform = new TradingPlatform(inventoryLimit);
            platform.AddProduct(product1);

            bool result = platform.RemoveProduct(product2);

            Assert.That(result, Is.False, "Removing a product that does not exist should return false.");
        }

        [Test]
        public void RemoveProduct_ShouldReturnTrue_WhenProductExists()
        {
            int inventoryLimit = 1;
            Product product = new Product("Product1", "Category1", 100);

            TradingPlatform platform = new TradingPlatform(inventoryLimit);
            platform.AddProduct(product);

            bool result = platform.RemoveProduct(product);

            Assert.That(result, Is.True, "Removing a product that exists should return true.");
        }

        [Test]
        public void Validate_SellProduct_ReturnsCorrectEntity_WhenSellingProduct()
        {
           int inventoryLimit = 1;
            Product product = new Product("Product1", "Category1", 100);

            TradingPlatform platform = new TradingPlatform(inventoryLimit);
            platform.AddProduct(product);

            Product soldProduct = platform.SellProduct(product);
            Product trySellTheSameProduct = platform.SellProduct(product);

            Assert.That(soldProduct, Is.EqualTo(product), "The sold product should be the same as the one passed to the method.");
            Assert.That(trySellTheSameProduct, Is.Null, "The product should not exist in the inventory after being sold.");
        }

        [Test]
        public void Validate_InventoryReport_Returns_CorrectReport()
        {
            int inventoryLimit = 2;
            Product product1 = new Product("Product1", "Category1", 100);
            Product product2 = new Product("Product2", "Category2", 200);

            TradingPlatform platform = new TradingPlatform(inventoryLimit);
            platform.AddProduct(product1);
            platform.AddProduct(product2);

            string report = platform.InventoryReport();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Inventory Report:");
            sb.AppendLine("Available Products: 2");
            sb.AppendLine("Name: Product1, Category: Category1 - $100.00");
            sb.AppendLine("Name: Product2, Category: Category2 - $200.00");

            Assert.That(report, Is.EqualTo(sb.ToString().TrimEnd()), "The report should contain the correct information.");
        }
    }
}