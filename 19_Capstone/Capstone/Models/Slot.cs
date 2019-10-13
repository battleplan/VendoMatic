﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Slot
    {
        /// <summary>
        /// Unique identifier for the slot.
        /// </summary>
        public string Identifier { get; }

        /// <summary>
        /// Number of purchasable products left in the slot.
        /// </summary>
        public int QuantityRemaining { get; private set; }

        /// <summary>
        /// Can the slot dispense any more product.
        /// </summary>
        public bool HasStock => QuantityRemaining > 0;

        /// <summary>
        /// The product loaded into the slot.
        /// </summary>
        public Product Product { get; }

        /// <summary>
        /// The price to purchase a product from the slot.
        /// </summary>
        public decimal Price { get; }

        // Constructor
        /// <summary>
        /// Create a new slot.
        /// </summary>
        /// <param name="identifier">The unique identifier of the slot.</param>
        /// <param name="product">The product loaded into the slot.</param>
        /// <param name="price">The price to purchase a product from the slot.</param>
        public Slot(string identifier, Product product, decimal price)
        {
            Identifier = identifier;
            Product = product;
            Price = price;
            QuantityRemaining = 5;
        }

        /// <summary>
        /// Dispense a product from the slot.
        /// </summary>
        /// <returns>Success of dispensing the product.</returns>
        public bool Dispense()
        {
            if (HasStock)
            {
                QuantityRemaining--;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Display name of the slot and the product it contains.
        /// </summary>
        /// <param name="includeIdentifier">Display the identifier for the slot.</param>
        /// <returns>Complete string including identifier (if requested), quantity, price, and product name</returns>
        public string DisplayName(bool includeIdentifier)
        {
            string output = "";
            if (includeIdentifier)
            {
                output += $"[{Identifier}] ";
            }
            if (HasStock)
            {
                output += $"{Price:C} |{QuantityRemaining}| {Product.Name}";
            }
            else
            {
                output+= $"{Product.Name} SOLD OUT";
            }
            return output;
        }

        /// <summary>
        /// Display name of the slot and the product it contains.
        /// </summary>
        /// <returns>Complete string including identifier, quantity, price, and product name</returns>
        public string DisplayName()
        {
            return DisplayName(true);
        }
    }
}
