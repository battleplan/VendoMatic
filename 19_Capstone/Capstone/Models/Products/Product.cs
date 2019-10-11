using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public abstract class Product
    {
        /// <summary>
        /// Name of the product
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Running total of quantity sold.
        /// </summary>
        public int QuantitySold;

        /// <summary>
        /// Message to display to user upon purchase.
        /// </summary>
        /// <returns>String of message for each product type.</returns>
        public abstract string YumYum();

        // Constructor
        /// <summary>
        /// Create a new product.
        /// </summary>
        /// <param name="name">Name of the product.</param>
        public Product(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Record that the product has been sold.
        /// </summary>
        public void SellProduct()
        {
            QuantitySold++;
        }
    }
}
