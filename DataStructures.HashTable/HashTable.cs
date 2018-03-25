using System;
using System.Collections.Generic;
using DataStructures.HashTable.Interfaces;

namespace DataStructures.HashTable
{
    /*
     Requirements: 

    *  If setter tries to set null, such element must be removed from the dictionary.
    *  Any .Net object can be used as a key or value.
    *   If you try to add the object by a key which already exists in the hash table – throw an exception.
    *   If you try to get an element by key which does not exit in the has table – throw an exceptoin.
        --Cover it with unit tests
     */

    public class HashTable<TKey, TValue>: IHashTable<TKey, TValue>
        where TKey: IComparable<TKey>
    {
        private EqualityComparer<TKey> _keyComparer = EqualityComparer<TKey>.Default;
        private EqualityComparer<TValue> _valueComparer = EqualityComparer<TValue>.Default;

        private IList<Bucket>[]  _buckets1 = new List<Bucket>[100];
        private LinkedList<Bucket>[] _buckets;

        private int _size;
        private int _capacity;

        private const double LoadFactor = .75;
        private const int MaxCapacity = int.MaxValue;
        private const int DefaultCapacity = 10;


        public HashTable(int initialCapacity = 0) 
        {
            _buckets = new LinkedList<Bucket>[DefaultCapacity];
            _capacity = _buckets.Length;
            _size = 0;
        }

        public bool Contains(TKey key)
        {
            var hashCode = GetHashCodeForKey(key, _capacity);

            return _buckets[hashCode] != null &&
                   _buckets[hashCode].Count > 0 &&
                   _buckets[hashCode].Find(new Bucket {Key = key}) != null;
        }

        public void Add(TKey key, TValue value)
        {
            var bucket = new Bucket {Key = key, Value = value};

            // should we re-alloc storage
            var reallocate =  (double)_size / _capacity >= LoadFactor;
            if (reallocate)
            {
                var newSize = _capacity * 2; // todo: user ReallocateStrategy - i.e. 1/3, 2/3, etc; fibs ?

                var newStorage = new LinkedList<Bucket>[newSize];

                // we need recalculate hashes for increased size
                for (var i = 0; i < _capacity; i++)
                {
                    newStorage[i] = _buckets[i];

                    var currentNode = _buckets[i].First;
                    while (currentNode != null)
                    {
                        var b = currentNode.Value;
                        b.KeyHashCode = GetHashCodeForKey(b.Key, newSize);
                        currentNode = currentNode.Next;
                    }
                }

            }
        }

        

        internal void AddImpl(TKey key, TValue value)
        {
            var hashCode = GetHashCodeForKey(key, _capacity);

            if (_buckets[hashCode] == null)
            {
                _buckets[hashCode] = new LinkedList<Bucket>();
            }
            else
            {
                var alredyContains = _buckets[hashCode].Find(new Bucket {Key = key}) != null;
                if (alredyContains)
                {
                    throw new ArgumentException("Table alredy contains key");
                }
            }

            _buckets[hashCode].AddLast(new Bucket {Key = key, Value = value, KeyHashCode = hashCode});

            _size++;        
        }

        public TValue this[TKey key]
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }

        public bool TryGet(TKey key, out TValue value)
        {
            throw new System.NotImplementedException();
        }


        internal uint GetHashCodeForKey(TKey key, int lenght)
        {
            var code = key.GetHashCode(); // can return < 0
            var absCode = Math.Abs(code); // only 0 or more

            var normalizedCode = absCode % lenght;

            return Convert.ToUInt32(normalizedCode);
        }

        internal class Bucket
        {
            public TKey Key { get; set; }

            public TValue Value { get; set; }

            public uint KeyHashCode { get; set; }

            public bool Equals(Bucket bucket)
            {
                var equal = EqualityComparer<TKey>.Default.Equals(Key, bucket.Key);

                return equal;
            }

            // ignoring warn as Bucket wont be used in any hash based data structure
            public override bool Equals(object obj)
            {
                return Equals(obj as Bucket);
            }
        }
    }
}