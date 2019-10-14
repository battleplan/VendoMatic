using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models.Monies
{
    public class HundredDollar : Money
    {
        /// <summary>
        /// Creates a new hundred dollar bill.
        /// </summary>
        /// <param name="canMakeChange">Can this currency be returned as change?</param>
        public HundredDollar(bool canMakeChange) : base(canMakeChange)
        {
        }

        /// <summary>
        /// Creates a new hundred dollar bill.
        /// </summary>
        /// <param name="canMakeChange">Can this currency be returned as change?</param>
        /// <param name="balance">The amount of hundred dollar bills to create.</param>
        public HundredDollar(bool canMakeChange, int currencyQuantity) : base(canMakeChange, currencyQuantity)
        {
        }

        /// <summary>
        /// Singular or plural of hundred dollar bill.
        /// </summary>
        public override string Name => Count != 1 ? "hundred dollar bills" : "hundred dollar bill";

        /// <summary>
        /// The dollar value of hundred dollars.
        /// </summary>
        public override decimal UnitValue => 100M;
    }
}
