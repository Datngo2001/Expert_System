using System;
using System.Collections.Generic;
using System.Text;

namespace Expert_System
{
    class InferenceEngine
    {
        protected List<Parameter> facts = new List<Parameter>();
        public List<Parameter> Facts 
        {
            get { return facts; }
            set { facts = Facts; } 
        }
        public KnowledgeBase Base { get; set; }
        public InferenceEngine()
        {

        }
        public InferenceEngine(KnowledgeBase knowledgeBase)
        {
            this.Base = knowledgeBase;
        }
        public void Start()
        {
            for (int i = 0; i < Base.Parameters.Count; i++)
            {
                Base.Parameters[i].Value = askPeople(Base.Parameters[i]);
                Facts.Add(Base.Parameters[i]);
            }
            do
            {
                for (int i = 0; i < Base.Rules.Count; i++)
                {
                    if (isRelated(Base.Rules[i].Conditions))
                    {
                        bool satify = true;
                        for (int j = 0; j < Base.Rules[i].Conditions.Count && satify; j++)
                        {
                            if (isAsked(Base.Rules[i].Conditions[j]))
                            {
                                if (!SearchFact(Base.Rules[i].Conditions[j]))
                                {
                                    satify = false;
                                }
                            }
                            else
                            {
                                bool answer = askPeople(Base.Rules[i].Conditions[j]);
                                if (Base.Rules[i].Conditions[j].Value != answer)
                                {
                                    satify = false;
                                    Parameter fact = new Parameter();
                                    fact.Name = Base.Rules[i].Conditions[j].Name;
                                    fact.Value = answer;
                                    Facts.Add(fact);
                                }
                                else
                                {
                                    Facts.Add(Base.Rules[i].Conditions[j]);
                                }
                            }
                        }
                        if (satify)
                        {
                            for (int j = 0; j < Base.Rules[i].Results.Count; j++)
                            {
                                if (!SearchFact(Base.Rules[i].Results[j]))
                                {
                                    Facts.Add(Base.Rules[i].Results[j]);
                                }
                            }
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            } while (FactIncrease());

        }
        public void ShowResult()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Co the ban bi: ");
            for (int i = 0; i < Facts.Count; i++)
            {
                if (Facts[i].Name.EndsWith('*') && Facts[i].Value == true)
                {
                    Console.WriteLine(Facts[i].Name.Remove(Facts[i].Name.Length - 1, 1));
                }
            }
            Console.WriteLine();
            Console.WriteLine("Ban nen: ");
            for (int i = 0; i < Facts.Count; i++)
            {
                if (Facts[i].Name.EndsWith('+') && Facts[i].Value == true)
                {
                    Console.WriteLine(Facts[i].Name.Remove(Facts[i].Name.Length - 1, 1));
                }
            }
            Console.ResetColor();
        }
        protected bool askPeople(Parameter parameter)
        {
            string question = "";
            question = "Ban co " + parameter.Name + " khong? (Co/Khong)";
            Console.WriteLine(question);
            if (ToBool(Console.ReadLine()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        protected int lastCount = 0;
        protected bool FactIncrease()
        {
            if(Facts.Count > lastCount)
            {
                lastCount = Facts.Count;
                return true;
            }
            return false;
        }
        protected bool ToBool(string value)
        {
            if (value == "Co" || value == "co" || value == "yes" || value == "Yes")
            {
                return true;
            }
            else if (value == "Khong" || value == "ko" || value == "khong" || value == "No" || value == "no")
            {
                return false;
            }
            return false;
        }
        protected bool SearchFact(Parameter parameter)
        {
            for (int i = 0; i < Facts.Count; i++)
            {
                if(parameter.Name == Facts[i].Name && parameter.Value == Facts[i].Value)
                {
                    return true;
                }
            }
            return false;
        }
        protected bool isRelated(List<Parameter> conditions)
        {
            for (int i = 0; i < conditions.Count; i++)
            {
                if (SearchFact(conditions[i]))
                {
                    return true;
                }
                else
                {
                    
                }
            }
            return false;
        }
        protected bool isAsked(Parameter condition)
        {
            for (int i = 0; i < Facts.Count; i++)
            {
                if (condition.Name == Facts[i].Name)
                {
                    return true;
                }
            }
            return false;
        }
    }
}