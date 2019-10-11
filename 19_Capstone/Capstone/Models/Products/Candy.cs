using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Candy : Product
    {
        // Constructor
        /// <summary>
        /// Create new candy.
        /// </summary>
        /// <param name="name">Name of candy.</param>
        public Candy(string name) : base(name)
        {

        }

        /// <summary>
        /// Message to display to operator upon purchase.
        /// </summary>
        /// <returns>String of message.</returns>
        public override string YumYum()
        {
           return "Munch Munch, Yum!";
        }
    }
}
