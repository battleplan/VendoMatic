using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Chip : Product
    {
        // TODO Set up summary
        public override void YumYum()
        {
            // TODO Should this write directly to the console or return a value?
            Console.WriteLine("Crunch Crunch, Yum!");
        }
    }
}
