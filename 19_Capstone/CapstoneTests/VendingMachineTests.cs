using Capstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CapstoneTests
{
    [TestClass]
    public class VendingMachineTests
    {
        [TestMethod]
        public void StockTest()
        {
            // Stock with good input
            // Arrange
            VendingMachine vm = new VendingMachine();

            // Act
            string[] inputLines = new string[]
            {
                "A1|Potato Crisps|3.05|Chip",
                "A2|Stackers|1.45|Chip"
            };

            vm.Stock(inputLines);

            Chip potatoCrisps = new Chip("Potato Crisps");
            Chip stackers = new Chip("Stackers");

            List<Product> expectedProducts = new List<Product>()
            {
                potatoCrisps,
                stackers
            };

            List<Slot> expectedSlots = new List<Slot>()
            {
                new Slot("A1", potatoCrisps, 3.05M),
                new Slot("A2", stackers, 1.45M)
            };

            // Stock with poorly formatted input
            // Assert
            Assert.AreEqual(expectedProducts[0].Name, vm.Products["Potato Crisps"].Name);
            Assert.AreEqual(expectedProducts[1].Name, vm.Products["Stackers"].Name);
            Assert.AreEqual(expectedSlots[0].Identifier, vm.Slots["A1"].Identifier);
            Assert.AreEqual(expectedSlots[0].Price, vm.Slots["A1"].Price);
            Assert.AreEqual(expectedSlots[0].Product.Name, vm.Slots["A1"].Product.Name);
            Assert.AreEqual(expectedSlots[1].Identifier, vm.Slots["A2"].Identifier);
            Assert.AreEqual(expectedSlots[1].Price, vm.Slots["A2"].Price);
            Assert.AreEqual(expectedSlots[1].Product.Name, vm.Slots["A2"].Product.Name);


            // Arrange
            vm = new VendingMachine();

            // Act
            inputLines = new string[]
            {
                "A1|Product|Chip|3.05"
            };

            vm.Stock(inputLines);

            // Assert
            Assert.AreEqual(0, vm.Products.Count);
            Assert.AreEqual(0, vm.Slots.Count);
        }

        [TestMethod]
        public void FeedMoneyTest()
        {
            // Feed good amount
            // Arrange
            VendingMachine vm = new VendingMachine();
            // Act
            bool actualFedResult = vm.FeedMoney((int)2.5);
            // Assert
            Assert.AreEqual(2M, vm.Balance);
            Assert.AreEqual(true, actualFedResult);

            // Feed negative amount
            // Act
            actualFedResult = vm.FeedMoney(-2);
            // Assert
            Assert.AreEqual(2M, vm.Balance);
            Assert.AreEqual(false, actualFedResult);

            // Feed zero amount
            // Act
            actualFedResult = vm.FeedMoney(0);
            // Assert
            Assert.AreEqual(2M, vm.Balance);
            Assert.AreEqual(false, actualFedResult);
        }

        [TestMethod]
        public void FinishTransactionTest()
        {
            // Change for one dollar
            // Arrange
            VendingMachine bobsAMachine = new VendingMachine();
            bobsAMachine.FeedMoney(1);

            // Act
            string actualResult = bobsAMachine.FinishTransaction();

            // Assert
            Assert.AreEqual($"Your Change is 4 Quarter(s), 0 Dime(s), and 0 Nickel(s)", actualResult);
            Assert.AreEqual(0, bobsAMachine.Balance);

            // Change for 90 cents
            // Arrange
            bobsAMachine.FeedMoney(1);
            bobsAMachine.Stock(new string[] { "A1|Product|0.10|Chip" });
            bobsAMachine.Purchase("A1");
            actualResult = bobsAMachine.FinishTransaction();

            // Assert
            Assert.AreEqual($"Your Change is 3 Quarter(s), 1 Dime(s), and 1 Nickel(s)", actualResult);

            // Change for 0 cents
            // Arrange
            actualResult = bobsAMachine.FinishTransaction();

            // Assert
            Assert.AreEqual($"Your Change is 0 Quarter(s), 0 Dime(s), and 0 Nickel(s)", actualResult);
        }

        [TestMethod]
        public void PurchaseTest()
        {
            // Arrange
            VendingMachine vm = new VendingMachine();
            string[] inputLines = new string[]
            {
                "A1|Potato Crisps|3.05|Chip",
                "A2|Stackers|1.45|Chip"
            };

            vm.Stock(inputLines);

            decimal expectedTotalSales = 3.05M;
            int expectedQuantitySold = 1;

            vm.FeedMoney(4);

            decimal expectedBalance = 0.95M;

            // Act
            bool actualPurchaseMade = vm.Purchase("A1");

            // Assert
            Assert.AreEqual(4, vm.Slots["A1"].QuantityRemaining);
            Assert.AreEqual(expectedTotalSales, vm.TotalSales);
            Assert.AreEqual(expectedBalance, vm.Balance);
            Assert.AreEqual(expectedQuantitySold, vm.Slots["A1"].Product.QuantitySold);
            Assert.AreEqual(true, actualPurchaseMade);
            Assert.AreEqual(1, vm.Slots["A1"].Product.QuantitySold);



            // Arrange
            vm = new VendingMachine();

            vm.Stock(inputLines);

            expectedTotalSales = 0M;
            expectedQuantitySold = 0;

            vm.FeedMoney(2);

            expectedBalance = 2M;

            // Act
            actualPurchaseMade = vm.Purchase("A1");

            // Assert
            Assert.AreEqual(5, vm.Slots["A1"].QuantityRemaining);
            Assert.AreEqual(expectedTotalSales, vm.TotalSales);
            Assert.AreEqual(expectedBalance, vm.Balance);
            Assert.AreEqual(expectedQuantitySold, vm.Slots["A1"].Product.QuantitySold);
            Assert.AreEqual(false, actualPurchaseMade);
            Assert.AreEqual(0, vm.Slots["A1"].Product.QuantitySold);
        }
    }
}
