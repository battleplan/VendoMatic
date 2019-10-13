﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models.Monies
{
    public abstract class Money
    {
        /// <summary>
        /// Name of the type of currency.
        /// </summary>
        public abstract string Name { get; }
        /// <summary>
        /// Value of one unit of the currency in dollars.
        /// </summary>
        public abstract decimal UnitValue { get; }
        /// <summary>
        /// Amount of the currency held.
        /// </summary>
        public int Count { get; private set; }
        /// <summary>
        /// Total value of the currency in dollars.
        /// </summary>
        public decimal TotalValue
        {
            get
            {
                return UnitValue * Count;
            }
        }

        /// <summary>
        /// Creates new currency.
        /// </summary>
        public Money() => Count = 0;

        /// <summary>
        /// Creates new currency with a specified quantity.
        /// </summary>
        /// <param name="currencyQuantity">Quantity of the currency to create. Must be greater than or equal to zero.</param>
        public Money(int currencyQuantity)
        {
            if (currencyQuantity >= 0)
            {
                Count = currencyQuantity;
            }
            else
            {
                Count = 0;
            }
        }

        /// <summary>
        /// Increases a given money's count to the amount of change that can be made.
        /// </summary>
        /// <param name="currencyAvailable">The currency available to dispense change from.</param>
        /// <param name="balance">The amount of the bill to make change from.</param>
        /// <returns></returns>
        public void MakeChange(Money currencyAvailable, decimal balance)
        {
            int currencyQuantityAvailable = currencyAvailable.Count;
            int changeCount = (int)(balance / UnitValue);
            changeCount = Math.Min(currencyQuantityAvailable, changeCount);
            currencyAvailable.Count -= changeCount;
            Count = changeCount;
        }

        /// <summary>
        /// Replenishes stock of currency up to new quantity.
        /// </summary>
        /// <param name="newQuantity">The new amount of the currency.</param>
        public void ReplenishQuantity(int newQuantity)
        {
            if (newQuantity > Count)
            {
                Count = newQuantity;
            }
        }

        /// <summary>
        /// Displays money in a string.
        /// </summary>
        /// <returns>Formatted string (e.g. "2 quarters")</returns>
        public override string ToString() => $"{Count} {Name}";
    }
}
