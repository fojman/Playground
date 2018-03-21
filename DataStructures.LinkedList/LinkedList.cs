using System;
using System.Collections;
using System.Collections.Generic;
using DataStructures.LinkedList.Interfaces;

namespace DataStructures.LinkedList
{
    public class LinkedList<T>: ILinkedList<T> 
        //where T: IEqualityComparer<T>
    {
        #region ILinkedList

        public int Length { get; private set; }

        public void Add(T e) => Append(e);

        public void Remove(T e)
        {
            var index = IndexOf(e);
            if (index != -1)
            {
                RemoveAt(index);
            }
        }

        public void InsertAt(T e, int index) => Insert(e, index);

        public void RemoveAt(int index) => Delete(index);

        public T ElementAt(int index)
        {            
            if (IsEmpty || index < 0 || index >= Length)
            {
                throw new IndexOutOfRangeException();       
            }

            var current = Head; //[0] -> i 'strictly' less
            for (var i = 0; i < index; i++)
            {
                current = current.NextNode;
            }

            return current.Value;
        }

        #endregion


        #region Impl details

        // first node
        internal Node<T> Head { get; set; }

        // last node
        internal Node<T> Tail { get; set; }

        internal bool IsEmpty => Length == 0;
        private void Append(T value)
        {
            var node = new Node<T>(value);
            if (IsEmpty)
            {
                Head = Tail = node;
            }
            else
            {
                Tail.NextNode = node;
                Tail = node;
            }

            Length++;
        }

        private int IndexOf(T e)
        {
            if (IsEmpty)
            {
                return -1;
            }

            var current = Head;
/*            var index = 0;
            while (current.NextNode != null)
            {
                var cmp = EqualityComparer<T>.Default;
                if (cmp.Equals(current.Value, e))
                {
                    return index;
                }

                index++;
            }*/

            for (var i = 0; current != null; i++, current = current.NextNode)
            {
                var cmp = EqualityComparer<T>.Default;
                if (cmp.Equals(current.Value, e))
                {
                    return i;
                }
                
            }

            return -1;
        }

        private void Prepend(T value)
        {
            var node = new Node<T>(value);
            if (IsEmpty)
            {
                Tail = Head = node;
            }
            else
            {
                node.NextNode = Head;
                Head = node;
            }

            Length++;
        }

        private void Insert(T value, int index)
        {
            if (index == 0)
            {
                Prepend(value);
            }
            else if (index == Length) // New last => [0]-->[1]-->[2] => Lenght=3 ==> .
            {
                Append(value);
            }
            else if (index > 0 && index < Length)
            {
                var node = new Node<T>(value);

                var currentNode = Head;
                for (var j = 1; j < index; j++) // @node: j==1 important - we need node befor index || or index-1
                {
                    currentNode = currentNode.NextNode;
                }

                node.NextNode = currentNode.NextNode;
                currentNode.NextNode = node;
                Length++;
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }

        private void Delete(int index)
        {
            if (IsEmpty || index < 0 || index >= Length)
            {
                throw new IndexOutOfRangeException();
            }

            // at least one element in list
            if (index == 0) //
            {
                Head = Head.NextNode;
                Length--;
            }
            else if (index == Length - 1) //
            {
                // get prev node => get L[index-1] node
                var current = Head;
                while (current.NextNode != null && current != Tail)
                {
                    current = current.NextNode;
                }

                current.NextNode = null;
                Tail = current;
                Length--;

            }
            else // no firt and not last
            {
                var current = Head;
                var i = 0;
                while (current.NextNode != null)
                {
                    if (i + 1 == index) // our node
                    {
                        current.NextNode = current.NextNode.NextNode;
                        Length--;
                        break;
                    }

                    i++;
                }
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new ListEnumerator<T>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }

    internal class ListEnumerator<T>: IEnumerator<T> 
        //where T: IEqualityComparer<T>
    {
        private readonly LinkedList<T> _list;
        private Node<T> _current;
        private Node<T> _next;

        public ListEnumerator(LinkedList<T> list)
        {
            _list = list;            
            Reset();
        }

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            if (_next == null) return false;

            _current = _next;
            _next = _next.NextNode;

            return true;
        }

        public void Reset()
        {
            _current = null;
            _next = _list.Head;
        }

        object IEnumerator.Current => Current;


        public T Current => _current.Value;
    }

    internal class Node<T>
    {
        public T Value { get; set; }

        public Node<T> NextNode;

        public Node(T value)
        {
            Value = value;            
        }

        public Node()
        {
            Value = default(T);
            NextNode = null;
        }
    }
}