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
        /// <param name="balance">The amount of dollars to convert into nickels.</param>
        public Nickel(decimal balance) : base(balance)
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
