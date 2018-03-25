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
    }
}
