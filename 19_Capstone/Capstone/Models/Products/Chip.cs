using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Chip : Product
    {

        // Constructor
        /// <summary>
        /// Create new chip.
        /// </summary>
        /// <param name="name">Name of chip.</param>
        public Chip(string name) : base(name)
        {

        }

        /// <summary>
        /// Message to display to operator upon purchase.
        /// </summary>
        /// <returns>String of message.</returns>
        public override string YumYum()
        {
            return "Crunch Crunch, Yum!";
        }
    }
}
