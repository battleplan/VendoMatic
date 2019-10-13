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
        public Dime() : base()
        {
        }

        /// <summary>
        /// Creates a new dime.
        /// </summary>
        /// <param name="balance">The amount of dimes to create.</param>
        public Dime(int currencyQuantity) : base(currencyQuantity)
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
