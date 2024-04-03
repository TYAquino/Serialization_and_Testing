using System;
using System.Collections.Generic;

namespace Assignment3.Utility
{
    [Serializable]
    public class SLL : ILinkedListADT
    {
        private Node head;

        public SLL()
        {
            head = null;
        }

        public bool IsEmpty()
        {
            return head == null;
        }

        public void Clear()
        {
            head = null;
        }

        public void AddLast(User value)
        {
            Node newNode = new Node(value);
            if (IsEmpty())
            {
                head = newNode;
            }
            else
            {
                Node current = head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = newNode;
            }
        }

        public void AddFirst(User value)
        {
            Node newNode = new Node(value)
            {
                Next = head
            };
            head = newNode;
        }

        public void Add(User value, int index)
        {
            if (index < 0 || index > Count())
                throw new IndexOutOfRangeException("Index out of range");

            if (index == 0)
            {
                AddFirst(value);
                return;
            }

            Node newNode = new Node(value);
            Node current = head;
            for (int i = 0; i < index - 1; i++) // Stop one node before the index
            {
                current = current.Next;
            }

            newNode.Next = current.Next;
            current.Next = newNode;
        }

        public void Replace(User value, int index)
        {
            if (index < 0 || index >= Count())
                throw new IndexOutOfRangeException("Index out of range");

            Node current = head;
            for (int i = 0; i < index; i++)
            {
                current = current.Next;
            }

            current.Data = value;
        }

        public int Count()
        {
            int count = 0;
            Node current = head;
            while (current != null)
            {
                count++;
                current = current.Next;
            }
            return count;
        }

        public void RemoveFirst()
        {
            if (IsEmpty())
                throw new InvalidOperationException("Cannot remove from an empty list.");

            head = head.Next;
        }

        public void RemoveLast()
        {
            if (IsEmpty())
                throw new InvalidOperationException("Cannot remove from an empty list.");

            if (head.Next == null)
            {
                head = null;
                return;
            }

            Node current = head;
            while (current.Next.Next != null) // Find the second-to-last node
            {
                current = current.Next;
            }

            current.Next = null; // Remove the last node
        }

        public void Remove(int index)
        {
            if (index < 0 || index >= Count())
                throw new IndexOutOfRangeException("Index out of range");

            if (index == 0)
            {
                RemoveFirst();
                return;
            }

            Node current = head;
            for (int i = 0; i < index - 1; i++) // Stop one node before the index
            {
                current = current.Next;
            }

            current.Next = current.Next.Next;
        }

        public User GetValue(int index)
        {
            if (index < 0 || index >= Count())
                throw new IndexOutOfRangeException("Index out of range");

            Node current = head;
            for (int i = 0; i < index; i++)
            {
                current = current.Next;
            }

            return current.Data;
        }

        public int IndexOf(User value)
        {
            Node current = head;
            int index = 0;
            while (current != null)
            {
                if (current.Data.Equals(value))
                {
                    return index;
                }
                index++;
                current = current.Next;
            }
            return -1; // Not found
        }

        public bool Contains(User value)
        {
            return IndexOf(value) != -1;
        }

        public void Join(ILinkedListADT other)
        {
            // Nothing to do if the other list is empty
            if (other.IsEmpty()) return;

            // If the current list is empty, set head to the head of the other list
            if (this.IsEmpty())
            {
                this.head = ((SLL)other).head;
                return;
            }

            // Find the last node of the current list
            Node current = this.head;
            while (current.Next != null)
            {
                current = current.Next;
            }

            // Connect the last node of the current list to the head of the other list
            current.Next = ((SLL)other).head;

            // Optionally, you may want to clear the other list to avoid shared references
            other.Clear();
        }
    }
}
