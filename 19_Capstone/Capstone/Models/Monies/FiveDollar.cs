using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models.Monies
{
    public class FiveDollar : Money
    {
        /// <summary>
        /// Creates a new five dollar bill.
        /// </summary>
        /// <param name="canMakeChange">Can this currency be returned as change?</param>
        public FiveDollar(bool canMakeChange) : base(canMakeChange)
        {
        }

        /// <summary>
        /// Creates a new five dollar bill.
        /// </summary>
        /// <param name="canMakeChange">Can this currency be returned as change?</param>
        /// <param name="balance">The amount of five dollar bills to create.</param>
        public FiveDollar(bool canMakeChange, int currencyQuantity) : base(canMakeChange, currencyQuantity)
        {
        }

        /// <summary>
        /// Singular or plural of five dollar bill.
        /// </summary>
        public override string Name => Count != 1 ? "five dollar bills" : "five dollar bill";

        /// <summary>
        /// The dollar value of five dollars.
        /// </summary>
        public override decimal UnitValue => 5M;
    }
}
