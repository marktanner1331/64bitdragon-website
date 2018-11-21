using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace binary_tree
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] values = {31, 59, 95, 58, 29, 71, 27, 38, 11, 95};
            Node root = Node.buildTree(values);

            Node needle = Node.find(root, 27);
            Console.WriteLine(needle);

            Node.preOrderPrint(root);
            Node.postOrderPrint(root);
            Node.inOrderPrint(root);
            Node.levelOrder(root);

            Console.Read();
        }
    }
}
