using System;
using System.Collections.Generic;
using System.Linq;

namespace DistributedIdGeneration
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new List<long>();

            Console.WriteLine("Hello World! Started generating Ids..");

            var idGen = new SnowflakeIdGenerator();
            for (int i = 0; i < 10000; i++)
            {
                list.Add(idGen.GenerateId(i));
            }

            var areUnique = list.Count == list.Distinct().Count();
            Console.WriteLine("10000 Ids generated, Are Unique ?" + areUnique);
            
            Console.ReadKey();
        }
    }
}
