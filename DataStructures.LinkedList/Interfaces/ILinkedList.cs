using System.Collections.Generic;

namespace DataStructures.LinkedList.Interfaces
{
    public interface ILinkedList<T>: IEnumerable<T>
        //where T : IEqualityComparer<T>

    {
        /// <summary>
        /// Returns linked list length
        /// </summary>
        int Length { get;}

        /// <summary>
        /// Add element to list
        /// </summary>
        /// <param name="e"></param>
        void Add(T e);

        /// <summary>
        /// Removes element from list(only the first occurence)
        /// </summary>
        /// <param name="e"></param>
        void Remove(T e);

        /// <summary>
        /// Add element into position index
        /// </summary>
        /// <param name="e"></param>
        /// <param name="index"></param>
        void InsertAt(T e, int index);

        /// <summary>
        /// Removes element at position index(if no element in position - throws)
        /// </summary>
        /// <param name="index"></param>
        void RemoveAt(int index);

        /// <summary>
        /// Returns element in posion index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        T ElementAt(int index);
    }
}