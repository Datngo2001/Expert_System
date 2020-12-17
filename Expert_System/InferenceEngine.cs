﻿using System;
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
                        for (int j = 0; j < Base.Rules[i].Conditions.Count; j++)
                        {
                            if (SearchFact(Base.Rules[i].Conditions[j]))
                            {
                                continue;
                            }
                            else
                            {
                                bool answer = askPeople(Base.Rules[i].Conditions[j]);
                                if(Base.Rules[i].Conditions[j].Value != answer)
                                {
                                    satify = false;
                                }
                            }
                        }
                        if (satify)
                        {
                            Facts.Add(Base.Rules[i].Result);
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            } while (FactIncrease());
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
            else if (value == "Khong" || value == "khong" || value == "No" || value == "no")
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
                    continue;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
    }
}