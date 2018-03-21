using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace LinkedList.UnitTests
{
    public class LinkedListTests
    {
        [Test]
        public void LinkedList_InsertToEmpty_Ok()
        {
            var list = new DataStructures.LinkedList.LinkedList<int>();

            list.Add(1);
            list.Add(2);
            list.Add(3);

            Assert.AreEqual(list.Length, 3);
        }

        [Test]
        public void LinkedList_RemoveFromEmpty_Throws()
        {
            var list = new DataStructures.LinkedList.LinkedList<int>();

            Assert.Throws<IndexOutOfRangeException>(() => list.RemoveAt(1));
        }

        [Test]
        public void LinkedList_RemoveAtOutOfRange_Throws()
        {
            var list = new DataStructures.LinkedList.LinkedList<int>();
            list.Add(10);

            Assert.Throws<IndexOutOfRangeException>(() => list.RemoveAt(2));
        }

        [Test]
        public void LinkedList_AddRemote_ShouldOk()
        {
            var list = new DataStructures.LinkedList.LinkedList<int>();

            list.Add(1);
            list.Remove(1);
            
            Assert.IsTrue(list.Length == 0);
        }

        [Test]
        public void LinkedList_AddRemoteMistmatch_ShouldThrow()
        {
            var list = new DataStructures.LinkedList.LinkedList<int>();

            list.Add(1);
            
            list.Remove(1);
            

            Assert.That(() => list.RemoveAt(0), Throws.Exception.TypeOf<IndexOutOfRangeException>());
        }


        [Test]
        public void LinkedList_Insert_ShouldOk()
        {
            var list = new DataStructures.LinkedList.LinkedList<int>();
                
            list.InsertAt(10, index:0);
            list.InsertAt(20, index: 1);
            list.InsertAt(30, index: 2);

            list.InsertAt(111, index: 1);
        }

        [Test]
        public void LinkedList_EnumeratorTraverse_ShouldOkk()
        {
            var list = new DataStructures.LinkedList.LinkedList<int> {1, 2, 3, 4};


            IEnumerator e = list.GetEnumerator();

            var enumerator = list.GetEnumerator();

            enumerator.MoveNext();
            Assert.AreEqual(1, enumerator.Current);

            enumerator.MoveNext();
            Assert.AreEqual(2, enumerator.Current);

            enumerator.MoveNext();
            Assert.AreEqual(3, enumerator.Current);

            enumerator.MoveNext();
            Assert.AreEqual(4, enumerator.Current);

            enumerator.MoveNext();
            Assert.AreEqual(4, enumerator.Current);

            enumerator.Reset();

            enumerator.MoveNext();
            Assert.AreEqual(1, enumerator.Current);

            enumerator.Dispose(); // make R# happy
        }

        [Test]
        public void TestBclList()
        {
            var list = new System.Collections.Generic.LinkedList<int>();

            list.GetEnumerator();
        }
    }
}