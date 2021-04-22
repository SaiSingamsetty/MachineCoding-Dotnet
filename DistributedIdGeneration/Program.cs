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

            var idGen = new SnowflakeIdGenerator();
            for (int i = 0; i < 1000; i++)
            {
                list.Add(idGen.GenerateId());
            }

            var areUnique = list.Count == list.Distinct().Count();
            Console.WriteLine("Are Unique 1000 ?" + areUnique);


            Console.WriteLine("Hello World!");
        }
    }
}
