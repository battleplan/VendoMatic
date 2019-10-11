using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Views
{
    public class MenuOption
    {
        public MenuOption(string name, bool isVisible)
        {
            Name = name;
            IsVisible = isVisible;
        }

        public string Name { get; }

        public bool IsVisible { get; }
    }
}
