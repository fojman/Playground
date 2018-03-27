using System;
using System.Collections.Generic;
using DataStructures.HashTable.Interfaces;

namespace DataStructures.HashTable
{
    /*
     Requirements: 

    *  If setter tries to set null, such element must be removed from the dictionary.
    *  Any .Net object can be used as a key or value. +
    *   If you try to add the object by a key which already exists in the hash table – throw an exception.
    *   If you try to get an element by key which does not exit in the has table – throw an exceptoin.
        --Cover it with unit tests
     */

    // hash table with chaining collision resolution

    public class HashTable<TKey, TValue>: IHashTable<TKey, TValue>
        //where TKey: IComparable<TKey>
    {
        private readonly EqualityComparer<TKey> _keyComparer = EqualityComparer<TKey>.Default;
        private readonly EqualityComparer<TValue> _valueComparer = EqualityComparer<TValue>.Default;





        private LinkedList<Bucket>[] _buckets;

        private int _size;
        private int _freeSlots;
        private int _capacity;

        private const double LoadFactor = .75;
        private const int MaxCapacity = int.MaxValue;
        private const int DefaultCapacity = 10;


        public HashTable(int initialCapacity = 0) 
        {
            _buckets = new LinkedList<Bucket>[DefaultCapacity];
            _capacity = _buckets.Length;
            _freeSlots = _buckets.Length;
            _size = 0;
        }

        public bool Contains(TKey key)
        {            
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            var bucketIndex = GetHashCodeImpl(key);

            return _buckets[bucketIndex] != null &&
                   _buckets[bucketIndex].Contains(new Bucket(key));
        }

        public void Add(TKey key, TValue value)
        {
            if ((double)_freeSlots / _capacity > LoadFactor)
            {
                Expand();
            }

            AddImpl(key, value);
        }

        internal void Expand()
        {
            _capacity = _capacity * 2; // _capacity << 1;
            var newStorage = new LinkedList<Bucket>[_capacity];
            _freeSlots = newStorage.Length;

            for (int i = 0; i < _buckets.Length; i++)
            {
                if (_buckets[i] != null)
                {                    
                    var current = _buckets[i].First;
                    while (current != null)
                    {
                        var rawHash = current.Value.HashCode;
                        var index = rawHash % _capacity;

                        if (newStorage[index] == null)
                        {
                            newStorage[index] = new LinkedList<Bucket>();
                            _freeSlots--;
                        }

                        newStorage[index].AddLast(current.Value);


                        current = current.Next;
                    }
                }
            }

            _buckets = newStorage;
        }

        internal void AddImpl(TKey key, TValue value)
        {
            var rawHash = GetRawHashCode(key) ;
            var index = rawHash % _capacity;

            var bucket = new Bucket(key, value, rawHash);

            if (_buckets[index] == null) // first insertion into postion 'index'
            {
                _buckets[index] = new LinkedList<Bucket>();
                _freeSlots--; // occupied
            }
            else if (_buckets[index].Count > 0 && _buckets[index].Contains(bucket))
            {
                throw new ArgumentException($"Table alredy contains key - {key}");
            }

            _buckets[index].AddLast(bucket);

            _size++;
        }

        
        // -   If you try to add the object by a key which already exists in the hash table – throw an exception.
        // -   If you try to get an element by key which does not exit in the has table – throw an exceptoin.
        public TValue this[TKey key]
        {
            get
            {
                TValue value;
                if (TryGet(key, out value))
                {
                    return value;
                }

                throw new ArgumentException("There is no such key in table");
            }

            // If setter tries to set null, such element must be removed from the dictionary.
            set
            {
                // 
                var index = GetRawHashCode(key) % _buckets.Length;
                if (_buckets[index] != null)
                {
                    var bucket = new Bucket(key);
                    var node = _buckets[index].Find(bucket);
                    if (node == null)
                    {
                        throw new ArgumentException("There is no such key in table");
                    }

                    if (value == null)
                    {
                        _buckets[index].Remove(bucket); // remove element from list
                        if (_buckets[index].Count == 0) // if list gets empty - remove list
                        {
                            _buckets[index] = null;
                        }
                    }
                    else
                    {
                        node.Value.Value = value; // looks ugly!?
                    }
                }
            }
        }

        public bool TryGet(TKey key, out TValue value)
        {
            value = default(TValue);

            var index = GetRawHashCode(key) % _buckets.Length;
            if (_buckets[index] != null)
            {
                var node = _buckets[index].Find(new Bucket(key));
                if (node != null)
                {
                    value = node.Value.Value;
                    return true;
                }
            }

            return false;
        }

        internal uint GetHashCodeImpl(TKey key)
        {
            return GetRawHashCode(key) % (uint)_capacity;
        }

        internal uint GetHashCodeImpl1(TKey key, int len)
        {
            return GetRawHashCode(key) % (uint)len;
        }

        internal uint GetRawHashCode(TKey key)
        {
            var hc = _keyComparer.GetHashCode(key);

            return Convert.ToUInt32(Math.Abs(hc));
        }
            


        // todo:
        internal class Bucket
        {
            public TKey Key { get; }
            public TValue Value { get; set; }


            // cached hash code value
            // - used for avoiding hash recalculation during expacding storage, @see - expand/rehash
            public uint HashCode { get; set; }

            public override bool Equals(object obj) => Equals(obj as Bucket);

            private bool Equals(Bucket bucket)
            {
                return EqualityComparer<TKey>.Default.Equals(Key, bucket.Key); // does all required checks - null, type, etc.
            }

            public override int GetHashCode() => Key.GetHashCode();

            public Bucket(TKey key)
            {
                Key = key;
            }

            public Bucket(TKey key, TValue value, uint hashCode)
            {
                Key = key;
                Value = value;
                HashCode = hashCode;

            }
        }
    }



}