using Capstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CapstoneTests
{
    [TestClass]
    public class VendingMachineTest
    {
        // TODO Test for feeding negative dollar amount


        [TestMethod]
        public void StockTest()
        {
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

            // Assert
            Assert.AreEqual(expectedProducts[0].Name, vm.Products[0].Name);
            Assert.AreEqual(expectedProducts[1].Name, vm.Products[1].Name);
            Assert.AreEqual(expectedSlots[0].Identifier, vm.Slots[0].Identifier);
            Assert.AreEqual(expectedSlots[0].Price, vm.Slots[0].Price);
            Assert.AreEqual(expectedSlots[0].Product.Name, vm.Slots[0].Product.Name);
            Assert.AreEqual(expectedSlots[1].Identifier, vm.Slots[1].Identifier);
            Assert.AreEqual(expectedSlots[1].Price, vm.Slots[1].Price);
            Assert.AreEqual(expectedSlots[1].Product.Name, vm.Slots[1].Product.Name);
        }

        [TestMethod]
        public void FeedMoneyTest()
        {
            // Arrange
            VendingMachine vm = new VendingMachine();
            // Act
            vm.FeedMoney((int)2.5);
            // Assert
            Assert.AreEqual(2M, vm.Balance);
        }

        //TODO write another test for Transactions non whole dollar amounts
        [TestMethod]
        public void FinishTransactionTest()
        {
            //Arrange
            VendingMachine bob = new VendingMachine();
            bob.FeedMoney(1);
            //Act
            string bobsEx = bob.FinishTransaction();
            //Assert
            Assert.AreEqual($"Your Change is 4 Quarters, 0 Dimes, and 0 Nickels", bobsEx);
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

            decimal expectedTotalSales = vm.TotalSales + vm.Slots[0].Price;
            int expectedQuantitySold = vm.Slots[0].Product.QuantitySold + 1;

            vm.FeedMoney(4);

            decimal expectedBalance = 0.95M;

            // Act
            vm.Purchase(vm.Slots[0]);

            // Assert
            Assert.AreEqual(4, vm.Slots[0].QuantityRemaining);
            Assert.AreEqual(expectedTotalSales, vm.TotalSales);
            Assert.AreEqual(expectedBalance, vm.Balance);
            Assert.AreEqual(expectedQuantitySold, vm.Slots[0].Product.QuantitySold);

            // TODO Sales report totals
            //Assert.AreEqual(, vm.Slots[0].Product.QuantitySold)
        }
    }
}
