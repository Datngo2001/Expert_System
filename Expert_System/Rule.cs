using System;
using System.Collections.Generic;
using System.Text;

namespace Expert_System
{
    class Rule
    {
        protected List<Parameter> conditions = new List<Parameter>();
        public List<Parameter> Conditions
        {
            get { return conditions; }
            set { conditions = value; }
        }
        protected Parameter result = new Parameter();
        public Parameter Result
        {
            get { return result; }
            set { result = value; }
        }
    }
}
