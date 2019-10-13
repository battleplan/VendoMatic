using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models.Monies
{
    public class Quarter : Money
    {
        /// <summary>
        /// Creates a new quarter.
        /// </summary>
        /// <param name="canMakeChange">Can this currency be returned as change?</param>
        public Quarter(bool canMakeChange) : base(canMakeChange)
        {
        }

        /// <summary>
        /// Creates a new quarter.
        /// </summary>
        /// <param name="canMakeChange">Can this currency be returned as change?</param>
        /// <param name="balance">The amount of quarters to create.</param>
        public Quarter(bool canMakeChange, int currencyQuantity) : base(canMakeChange, currencyQuantity)
        {
        }

        /// <summary>
        /// Singular or plural of quarter.
        /// </summary>
        public override string Name => Count != 1 ? "quarters" : "quarter";

        /// <summary>
        /// The dollar value of one quarter.
        /// </summary>
        public override decimal UnitValue => 0.25M;
    }
}
