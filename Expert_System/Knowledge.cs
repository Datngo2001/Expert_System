using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Expert_System
{
    class KnowledgeBase
    {
        protected List<string> parameters = new List<string>();
        protected Dictionary<string, string> rules = new Dictionary<string, string>();
        public List<string> Parameters
        {
            get { return parameters; }
            set { parameters = value; }
        }
        public Dictionary<string,string> Rules
        {
            get { return rules; }
            set { rules = value; }
        }
        public void Parse(string path)
        {
            try
            {
                StreamReader reader = new StreamReader(path);
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();

                    if (line == "" || line[0] == '-' || line[0] == '#') continue;


                }
                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
