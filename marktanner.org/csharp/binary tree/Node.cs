using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace binary_tree
{
    class Node
    {
        public int data;
        public Node left;
        public Node right;

        public Node(int data)
        {
            this.data = data;
        }

        public static void levelOrder(Node root)
        {
            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(root);

            while(queue.Count() > 0)
            {
                root = queue.Dequeue();
                if(root != null)
                {
                    Console.WriteLine(root);
                    queue.Enqueue(root.left);
                    queue.Enqueue(root.right);
                }
            }
        }

        public static Node find(Node root, int data)
        {
            if(root == null || root.data == data)
            {
                return root;
            }
            
            if(data < root.data)
            {
                return find(root.left, data);
            }
            
            return find(root.right, data);
        }

        public static Node buildTree(int[] values)
        {
            Node root = new Node(values[0]);
            for (int i = 1; i < values.Length; i++)
            {
                insert(root, values[i]);
            }
            return root;
        }

        public static Node insert(Node root, int data)
        {
            if(root == null)
            {
                root = new Node(data);
            }
            else if(data <= root.data)
            {
                root.left = insert(root.left, data);
            }
            else
            {
                root.right = insert(root.right, data);
            }

            return root;
        }

        public static void preOrderPrint(Node root)
        {
            if(root == null)
            {
                return;
            }

            Console.WriteLine(root);
            preOrderPrint(root.left);
            preOrderPrint(root.right);
        }

        public static void postOrderPrint(Node root)
        {
            if (root == null)
            {
                return;
            }

            postOrderPrint(root.left);
            postOrderPrint(root.right);
            Console.WriteLine(root);
        }

        public static void inOrderPrint(Node root)
        {
            if (root == null)
            {
                return;
            }

            inOrderPrint(root.left);
            Console.WriteLine(root);
            inOrderPrint(root.right);
        }

        public override string ToString()
        {
            return data.ToString();
        }
    }
}
