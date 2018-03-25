using System;
using System.Collections.Generic;

namespace DataStructures.HashTable.Interfaces
{
    public interface IHashTable<TKey, TValue> //: IDictionary<TKey, TValue>
        where TKey: IComparable<TKey>
    {
        /// <summary>
        /// Checks if table contains key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Contains(TKey key);

        /// <summary>
        /// Adds key-value pair into table
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void Add(TKey key, TValue value);

        /// <summary>
        /// get/set indexes
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        TValue this[TKey key] { get; set; }

        /// <summary>
        /// Checks if table contains key and returns associated value if it does
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool TryGet(TKey key, out TValue value);

    }
}