using System;
using System.Collections.Generic;

namespace Expert_System
{
    class Parameter
    {
        public string Name { get; set; }
        public string realValue { get; set; }
        protected List<string> values = new List<string>();
        public List<string> Values
        {
            get { return values; }
            set 
            { 
                values = value; 
                if (values.Count == 1) realValue = values[0]; 
            }
        }
    }
}
