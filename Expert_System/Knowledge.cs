using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Expert_System
{
    class KnowledgeBase
    {
        protected List<Parameter> parameters = new List<Parameter>();
        protected List<Rule> rules = new List<Rule>();
        public List<Parameter> Parameters
        {
            get { return parameters; }
            set { parameters = value; }
        }
        public List<Rule> Rules
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

                    if (line.StartsWith("IF"))
                    {
                        Rule rule = new Rule();

                        line = line.Remove(0, 2); //Xoa IF

                        List<string> ruleParts = new List<string>();
                        ruleParts.AddRange(line.Split("THEN")); //Chia lam 2 phan truoc THEN va sau THEN

                        // Them phan sau THEN vao rule
                        List<string> resultParts = new List<string>();
                        resultParts.AddRange(ruleParts[1].Split("=", 2));
                        Parameter result = new Parameter();
                        result.Name = resultParts[0];
                        result.Values.Add(resultParts[1]);
                        rule.Results.Add(result);

                        // Them phan truoc THEN vao rule
                        List<string> conditions = new List<string>();
                        conditions.AddRange(ruleParts[0].Split("&"));
                        for (int i = 0; i < conditions.Count; i++)
                        {
                            List<string> conditionPart = new List<string>();
                            conditionPart.AddRange(conditions[i].Split("=", 2));
                            Parameter condition = new Parameter();
                            condition.Name = conditionPart[0];
                            condition.Values.Add(conditionPart[1]);
                            rule.Conditions.Add(condition);
                        }

                        Rules.Add(rule);
                     }
                    else
                    {
                        // doc cac tham so
                        Parameter parameter = new Parameter();
                        List<string> parameterParts = new List<string>();
                        parameterParts.AddRange(line.Split("=", 2));
                        parameter.Name = parameterParts[0];
                        List<string> values = new List<string>();
                        values.AddRange(parameterParts[1].Split("|"));
                        parameter.Values = values;
                        Parameters.Add(parameter);
                    }
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
