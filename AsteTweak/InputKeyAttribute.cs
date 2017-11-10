using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsteTweak
{
    [AttributeUsage(AttributeTargets.Property)]
    class InputKeyAttribute : Attribute
    {
        public string Name { get; private set; }
        public int Order { get; private set; }
        public bool IsLong { get; private set; }

        public InputKeyAttribute(string name, int order, bool isLong = false)
        {
            Name = name;
            Order = order;
            IsLong = isLong;
        }
    }
}
