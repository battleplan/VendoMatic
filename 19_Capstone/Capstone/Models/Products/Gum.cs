using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Gum : Product
    {
        // Constructor
        /// <summary>
        /// Create new gum.
        /// </summary>
        /// <param name="name">Name of gum.</param>
        public Gum(string name) : base(name)
        {

        }

        /// <summary>
        /// Message to display to operator upon purchase.
        /// </summary>
        /// <returns>String of message.</returns>
        public override string YumYum()
        {
            return "Chew Chew, Yum!";
        }
    }
}
