using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models.Monies
{
    public class FiftyDollar : Money
    {
        /// <summary>
        /// Creates a new fifty dollar bill.
        /// </summary>
        /// <param name="canMakeChange">Can this currency be returned as change?</param>
        public FiftyDollar(bool canMakeChange) : base(canMakeChange)
        {
        }

        /// <summary>
        /// Creates a new fifty dollar bill.
        /// </summary>
        /// <param name="canMakeChange">Can this currency be returned as change?</param>
        /// <param name="balance">The amount of fifty dollar bills to create.</param>
        public FiftyDollar(bool canMakeChange, int currencyQuantity) : base(canMakeChange, currencyQuantity)
        {
        }

        /// <summary>
        /// Singular or plural of fifty dollar bill.
        /// </summary>
        public override string Name => Count != 1 ? "fifty dollar bills" : "fifty dollar bill";

        /// <summary>
        /// The dollar value of fifty dollars.
        /// </summary>
        public override decimal UnitValue => 50M;
    }
}
