using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models.Monies
{
    public class TwoDollar : Money
    {
        /// <summary>
        /// Creates a new two dollar bill.
        /// </summary>
        /// <param name="canMakeChange">Can this currency be returned as change?</param>
        public TwoDollar(bool canMakeChange) : base(canMakeChange)
        {
        }

        /// <summary>
        /// Creates a new two dollar bill.
        /// </summary>
        /// <param name="canMakeChange">Can this currency be returned as change?</param>
        /// <param name="balance">The amount of two dollar bills to create.</param>
        public TwoDollar(bool canMakeChange, int currencyQuantity) : base(canMakeChange, currencyQuantity)
        {
        }

        /// <summary>
        /// Singular or plural of two dollar bill.
        /// </summary>
        public override string Name => Count != 1 ? "two dollar bills" : "two dollar bill";

        /// <summary>
        /// The dollar value of two dollars.
        /// </summary>
        public override decimal UnitValue => 2M;
    }
}
