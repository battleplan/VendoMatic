using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models.Monies
{
    public class Nickel : Money
    {
        /// <summary>
        /// Creates a new nickel.
        /// </summary>
        /// <param name="canMakeChange">Can this currency be returned as change?</param>
        public Nickel(bool canMakeChange) : base(canMakeChange)
        {
        }

        /// <summary>
        /// Creates a new nickel.
        /// </summary>
        /// <param name="canMakeChange">Can this currency be returned as change?</param>
        /// <param name="balance">The amount of nickels to create.</param>
        public Nickel(bool canMakeChange, int currencyQuantity) : base(canMakeChange, currencyQuantity)
        {
        }

        /// <summary>
        /// Singular or plural of nickel.
        /// </summary>
        public override string Name => Count != 1 ? "nickels" : "nickel";

        /// <summary>
        /// The dollar value of one nickel.
        /// </summary>
        public override decimal UnitValue => 0.05M;
    }
}
