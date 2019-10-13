using System;
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
        public int Count { get; }
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
        /// Creates a new money.
        /// </summary>
        /// <param name="balance">The amount of dollars to convert into this type of money.</param>
        public Money(decimal balance) => Count = (int)(balance / UnitValue);

        /// <summary>
        /// Displays money in a string.
        /// </summary>
        /// <returns>Formatted string (e.g. "2 quarters")</returns>
        public override string ToString() => $"{Count} {Name}";
    }
}
