using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStructures.HashTable;
using NUnit.Framework;

namespace HashTable.UnitTests
{
    public class HashTableTests
    {
        [Test]
        public void HashTable_Add_ShouldOk()
        {
            var hashTable = new HashTable<int, int>();

            hashTable.Add(key:1, value:1);
            hashTable.Add(key: 1, value: 1);


        }


        [Test]
        public void HashTable_NotRelated_Ok()
        {
            const int capacity = 17;

            var emptyStringHashCode = string.Empty.GetHashCode();

            var bucketes = new int[capacity];
            foreach (var i in Enumerable.Range(0, 100600))
            {
                var code = i.GetHashCode();

                bucketes[code % capacity]++;
            }


            for (int i = 0; i < bucketes.Length; i++)
            {
                Console.WriteLine($"B[{i}]={bucketes[i]}");
            }

            Assert.Fail("Intentionaly fails");
        }
    }
}
