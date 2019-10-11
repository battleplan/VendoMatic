﻿using Capstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapstoneTests
{
    [TestClass]
    public class SlotTests
    {
        [TestMethod]
        public void DispenseTest()
        {
            // Arrange
            Slot slot = new Slot("", new Chip(""), 0M);

            // Act
            bool actualReturn = slot.Dispense();

            // Assert
            Assert.AreEqual(true, actualReturn);
            Assert.AreEqual(4, slot.QuantityRemaining);
            Assert.AreEqual(true, slot.HasStock);


            // Act
            actualReturn = slot.Dispense();
            actualReturn = slot.Dispense();
            actualReturn = slot.Dispense();

            // Assert
            Assert.AreEqual(true, actualReturn);
            Assert.AreEqual(1, slot.QuantityRemaining);
            Assert.AreEqual(true, slot.HasStock);

            // Act
            actualReturn = slot.Dispense();

            // Assert
            Assert.AreEqual(true, actualReturn);
            Assert.AreEqual(0, slot.QuantityRemaining);
            Assert.AreEqual(false, slot.HasStock);

            // Act
            actualReturn = slot.Dispense();

            // Assert
            Assert.AreEqual(false, actualReturn);
            Assert.AreEqual(0, slot.QuantityRemaining);
            Assert.AreEqual(false, slot.HasStock);
        }
    }
}
