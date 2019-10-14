using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models.Monies
{
    public class TwentyDollar : Money
    {
        /// <summary>
        /// Creates a new twenty dollar bill.
        /// </summary>
        /// <param name="canMakeChange">Can this currency be returned as change?</param>
        public TwentyDollar(bool canMakeChange) : base(canMakeChange)
        {
        }

        /// <summary>
        /// Creates a new twenty dollar bill.
        /// </summary>
        /// <param name="canMakeChange">Can this currency be returned as change?</param>
        /// <param name="balance">The amount of twenty dollar bills to create.</param>
        public TwentyDollar(bool canMakeChange, int currencyQuantity) : base(canMakeChange, currencyQuantity)
        {
        }

        /// <summary>
        /// Singular or plural of twenty dollar bill.
        /// </summary>
        public override string Name => Count != 1 ? "twenty dollar bills" : "twenty dollar bill";

        /// <summary>
        /// The dollar value of twenty dollars.
        /// </summary>
        public override decimal UnitValue => 20M;
    }
}
