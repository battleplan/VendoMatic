using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models.Monies
{
    public class Dime : Money
    {
        /// <summary>
        /// Creates a new dime.
        /// </summary>
        /// <param name="canMakeChange">Can this currency be returned as change?</param>
        public Dime(bool canMakeChange) : base(canMakeChange)
        {
        }

        /// <summary>
        /// Creates a new dime.
        /// </summary>
        /// <param name="canMakeChange">Can this currency be returned as change?</param>
        /// <param name="balance">The amount of dimes to create.</param>
        public Dime(bool canMakeChange, int currencyQuantity) : base(canMakeChange, currencyQuantity)
        {
        }

        /// <summary>
        /// Singular or plural of dime.
        /// </summary>
        public override string Name => Count != 1 ? "dimes" : "dime";

        /// <summary>
        /// The dollar value of one dime.
        /// </summary>
        public override decimal UnitValue => 0.10M;
    }
}
