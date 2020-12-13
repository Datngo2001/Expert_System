using System;
using System.Collections.Generic;
using System.Text;

namespace Expert_System
{
    class Rule
    {
        protected List<Parameter> conditions = new List<Parameter>();
        protected List<Parameter> results = new List<Parameter>();
        public List<Parameter> Conditions
        {
            get { return conditions; }
            set { conditions = value; }
        }
        public List<Parameter> Results
        {
            get { return results; }
            set { results = value; }
        }
    }
}
