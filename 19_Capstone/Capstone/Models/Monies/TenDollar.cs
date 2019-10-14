using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models.Monies
{
    public class TenDollar : Money
    {
        /// <summary>
        /// Creates a new ten dollar bill.
        /// </summary>
        /// <param name="canMakeChange">Can this currency be returned as change?</param>
        public TenDollar(bool canMakeChange) : base(canMakeChange)
        {
        }

        /// <summary>
        /// Creates a new ten dollar bill.
        /// </summary>
        /// <param name="canMakeChange">Can this currency be returned as change?</param>
        /// <param name="balance">The amount of ten dollar bills to create.</param>
        public TenDollar(bool canMakeChange, int currencyQuantity) : base(canMakeChange, currencyQuantity)
        {
        }

        /// <summary>
        /// Singular or plural of ten dollar bill.
        /// </summary>
        public override string Name => Count != 1 ? "ten dollar bills" : "ten dollar bill";

        /// <summary>
        /// The dollar value of ten dollars.
        /// </summary>
        public override decimal UnitValue => 10M;
    }
}
