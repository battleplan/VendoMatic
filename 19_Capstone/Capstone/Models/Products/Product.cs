using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public abstract class Product
    {
        // TODO Set up summary
        public string Name { get; }


        // TODO Is this needed here?
        // TODO Set up summary
        public string Price { get; }


        private int quantitySold;

        // TODO Override this method
        public abstract void YumYum();
    }
}
