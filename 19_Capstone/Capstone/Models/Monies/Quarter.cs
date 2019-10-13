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
        public Quarter() : base()
        {
        }

        /// <summary>
        /// Creates a new quarter.
        /// </summary>
        /// <param name="balance">The amount of quarters to create.</param>
        public Quarter(int currencyQuantity) : base(currencyQuantity)
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
