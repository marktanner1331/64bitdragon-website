using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace triangular_array
{
    class TriangularArray <T>
    {
        private int height;
        private T[] elements;

        public TriangularArray(int height)
        {
            this.height = height;
            elements = new T[getTriangleNumber(height)];
        }

        public T getElementAt(int row, int column)
        {
            if (row < column)
            {
                throw new Exception("the row must be greater or equal to the column");
            }

            return elements[rowAndColumnToIndex(row, column)];
        }

        public void setElementAt(int row, int column, T value)
        {
            if (row < column)
            {
                throw new Exception("the row must be greater or equal to the column");
            }

            elements[rowAndColumnToIndex(row, column)] = value;
        }

        private static int rowAndColumnToIndex(int row, int column)
        {
            //remember rows and columns are 0 based
            return getTriangleNumber(row) + column;
        }

        private static int getTriangleNumber(int n)
        {
            return n * (n + 1) / 2;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            int i = 0;
            for (int k = 0; k < height; k++)
            {
                for(int j = 0;j < k;j++, i++)
                {
                    sb.Append(elements[i]);
                    sb.Append(j < k - 1 ? ", " : k < height - 1 ? "\n" : "");
                }
            }

            return sb.ToString();
        }
    }
}
