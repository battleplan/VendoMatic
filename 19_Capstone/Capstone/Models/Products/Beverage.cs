using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Beverage : Product
    {
        // Constructor
        /// <summary>
        /// Create a new beverage.
        /// </summary>
        /// <param name="name">Name of the beverage.</param>
        public Beverage(string name) : base(name)
        {
            
        }
        
        /// <summary>
        /// Message to display to operator upon purchase.
        /// </summary>
        /// <returns>String of message.</returns>
        public override string YumYum()
        {
           return "Glug Glug, Yum!";
        }
    }
}
