using Capstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CapstoneTests
{
    [TestClass]
    public class VendingMachineTest
    {
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
    }
}
