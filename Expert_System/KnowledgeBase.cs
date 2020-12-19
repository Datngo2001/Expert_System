using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Expert_System
{
    class KnowledgeBase
    {
        protected List<Rule> rules = new List<Rule>();
        public List<Rule> Rules
        {
            get { return rules; }
            set { rules = value; }
        }
        protected List<Parameter> parameters = new List<Parameter>();
        public List<Parameter> Parameters
        {
            get { return parameters; }
            set { parameters = value; }
        }
        public KnowledgeBase(string path)
        {
            Parse(path);
        }
        public void Parse(string path)
        {
            try
            {
                StreamReader reader = new StreamReader(path);
                
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    line = line.TrimStart();

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
                        resultParts[0] = resultParts[0].Trim();
                        result.Name = resultParts[0];
                        resultParts[1] = resultParts[1].Trim();
                        result.Value = ToBool(resultParts[1]);
                        rule.Result = result;

                        // Them phan truoc THEN vao rule
                        List<string> conditions = new List<string>();
                        conditions.AddRange(ruleParts[0].Split("&"));
                        for (int i = 0; i < conditions.Count; i++)
                        {
                            List<string> conditionPart = new List<string>();
                            conditionPart.AddRange(conditions[i].Split("=", 2));
                            Parameter condition = new Parameter();
                            conditionPart[0] = conditionPart[0].Trim();
                            condition.Name = conditionPart[0];
                            conditionPart[1] = conditionPart[1].Trim();
                            condition.Value = ToBool(conditionPart[1]);
                            rule.Conditions.Add(condition);
                        }

                        Rules.Add(rule);
                     }
                    else
                    {
                        // doc cac tham so
                        Parameter parameter = new Parameter();
                        parameter.Name = line;
                        parameter.Value = false;
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
        protected bool ToBool(string value)
        {
            if (value == "Co" || value == "co" || value == "yes" || value == "Yes")
            {
                return true;
            }
            else if (value == "Khong" || value == "khong" || value == "No" || value == "no" || value == "ko")
            {
                return false;
            }
            return false;
        }
    }
}
