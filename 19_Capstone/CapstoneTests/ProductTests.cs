using Capstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapstoneTests
{
    [TestClass]
    public class ProductTests
    {
        [TestMethod]
        public void SellProductTest()
        {
            // Arrange
            Product product = new Chip("");

            // Sell one product
            // Act
            product.SellProduct();

            // Assert
            Assert.AreEqual(1, product.QuantitySold);

            // Sell one hundred thousand more
            // Act
            for (int i = 1; i <= 100000; i++)
            {
                product.SellProduct();
            }

            // Assert
            Assert.AreEqual(100001, product.QuantitySold);
        }
    }
}
