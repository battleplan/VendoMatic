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
        /// <param name="balance">The amount of dollars to convert into quarters.</param>
        public Quarter(decimal balance) : base(balance)
        {
        }

        /// <summary>
        /// Singular or plural of quarter.
        /// </summary>
        public override string Name => Count != 1 ? "quarters" : "quarter";

        /// <summary>
        /// The dollar value of one quarter.
        /// </summary>
        public override decimal Value
        {
            get
            {
                return 0.25M;
            }
        }
    }
}
