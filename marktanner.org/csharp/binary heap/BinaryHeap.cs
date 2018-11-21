using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace binary_heap
{
    class BinaryHeap
    {
        //first element isnt used to make the maths easier
        private int[] values;
        public int size;

        public BinaryHeap()
        {
            values = new int[1];
            size = 0;
        }

        public BinaryHeap(int[] data)
        {
            values = new int[data.Length + 1];
            size = data.Length;

            //notice how values is one based
            Array.Copy(data, 0, values, 1, size);

            refreshAllNodes();
        }

        public int deleteMin()
        {
            if(size == 0)
            {
                throw new Exception("binary heap is of size 0");
            }

            int min = values[1];

            //make the smallest element the root
            values[1] = values[size];
            size--;

            if (size > 0)
            {
                percolateDown(1);
            }

            return min;
        }

        private void refreshAllNodes()
        {
            //in a balanced tree, size / 2 will refer to the last element in the penultimate layer
            for (int i = size / 2; i > 0; i--)
            {
                percolateDown(i);
            }
        }

        private void insert(int value)
        {
            if (size == values.Length - 1)
            {
                doubleCapacity();
            }

            size++;

            values[size] = value;
            percolateUp(size);
        }

        private void doubleCapacity()
        {
            int[] values2 = new int[values.Length];
            Array.Copy(values, 1, values2, 1, size);
            values = values2;
        }

        public override string ToString()
        {
            string s = "";
            for (int i = 1; i < size; i++)
            {
                s += values[i] + ", ";
            }
            s += values[size];
            return s;
        }

        private void percolateDown(int index)
        {
            int valueToPercolate = values[index];
            int index2;

            //index refers to the parent
            //index2 refers to the child
            
            //remember that values is one-based
            while(index * 2 <= size)
            {
                index2 = index * 2;

                //if the other child is larger, use it
                if(index2 != size && values[index2] > values[index2 + 1])
                {
                    index2++;
                }

                //swap if the value we are perolating is greater than the child
                if (valueToPercolate > values[index2])
                {
                    values[index] = values[index2];
                }
                else
                {
                    break;
                }

                index = index2;
            }

            values[index] = valueToPercolate;
        }

        private void percolateUp(int index)
        {
            int valueToPercolate = values[index];

            //while the index is not the root, and the value is greater than the values parant
            while (index > 1 && valueToPercolate > values[index / 2])
            {
                //put the parant where the child was
                values[index] = values[index / 2];
                
                //move to the parent
                index = index / 2;
            }

            values[index] = valueToPercolate;
        }
    }
}
