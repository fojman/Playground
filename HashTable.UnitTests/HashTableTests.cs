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
        public void SetUp()
        {            
        }

        /*
         Requirements: 

        *  If setter tries to set null, such element must be removed from the dictionary.
        *  Any .Net object can be used as a key or value. +
        *   If you try to add the object by a key which already exists in the hash table – throw an exception.
        *   If you try to get an element by key which does not exit in the has table – throw an exceptoin.
            --Cover it with unit tests
         */

        [Test]
        public void HashTable_Add_ShouldOk()
        {
            var hashTable = new HashTable<string, string>();

            hashTable.Add(key:"1", value:"sky");
            //hashTable.Add(key: "1", value: 1);

            hashTable["1"] = "one";


            hashTable["1"] = null;


        }


        [Test]
        public void HashTable_InsertCollidedItems_ShouldBeChained()
        {

            var table = new HashTable<Collided, string>();

            var key = new Collided();

            table.Add(key, "first");
            table.Add(key, "second");

        }

        // https://github.com/Skylenko/HashTable/blob/master/Test/Tests.cs

        // simulate collision
        internal class Collided//: IComparable<Collided>

        {
            public override bool Equals(object obj)
            {
                return false;
            }

            public override int GetHashCode()
            {
                return 2018;
            }
        }
    }
}
