using System;
using System.Collections.Generic;

namespace Expert_System
{
    class Parameter
    {
        public string Name { get; set; }
        protected List<string> values = new List<string>();
        public List<string> Values
        {
            get { return values; }
            set { values = value; }
        }
    }
}
