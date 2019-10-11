using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Chip : Product
    {

        //constructor
        public Chip(string name) : base(name)
        {

        }
        // TODO Set up summary
        public override string YumYum()
        {
            return "Crunch Crunch, Yum!";
        }
    }
}
