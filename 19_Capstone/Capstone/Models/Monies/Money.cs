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
        /// Value of the currency in dollars.
        /// </summary>
        public abstract decimal Value { get; }
        /// <summary>
        /// Amount of the currency held.
        /// </summary>
        public int Count { get; }

        /// <summary>
        /// Creates a new money.
        /// </summary>
        /// <param name="balance">The amount of dollars to convert into this type of money.</param>
        public Money (decimal balance)
        {
            Count = (int)(balance / Value);
        }
    }
}
