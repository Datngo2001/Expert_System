using System;

namespace Expert_System
{
    class Program
    {
        static void Main(string[] args)
        {
            KnowledgeBase knowledgeBase = new KnowledgeBase();
            knowledgeBase.Parse(@"C:\Users\supod\source\repos\Expert_System\Expert_System\law.txt");
        }
    }
}
