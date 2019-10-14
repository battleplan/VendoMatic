using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models.Monies
{
    public class OneDollar : Money
    {
        /// <summary>
        /// Creates a new one dollar bill.
        /// </summary>
        /// <param name="canMakeChange">Can this currency be returned as change?</param>
        public OneDollar(bool canMakeChange) : base(canMakeChange)
        {
        }

        /// <summary>
        /// Creates a new one dollar bill.
        /// </summary>
        /// <param name="canMakeChange">Can this currency be returned as change?</param>
        /// <param name="balance">The amount of one dollar bills to create.</param>
        public OneDollar(bool canMakeChange, int currencyQuantity) : base(canMakeChange, currencyQuantity)
        {
        }

        /// <summary>
        /// Singular or plural of one dollar bill.
        /// </summary>
        public override string Name => Count != 1 ? "one dollar bills" : "one dollar bill";

        /// <summary>
        /// The dollar value of one dollar.
        /// </summary>
        public override decimal UnitValue => 1M;
    }
}
