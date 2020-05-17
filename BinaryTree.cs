using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace BinaryTree
{
    [TestFixture]
    class Test
    {
        [TestCase(new[] { 6, 3, 4, 7, 2, 5 }, 6)]
        [TestCase(new[] { 6, 8, 4, 7, 3, 5,2,1 }, 2)]
        public void TestCaces(int[] valueArray, int expected)
        {
            var tree = Program.MakeTree(valueArray);
            int elem;
            if (Program.GetResult(tree, out elem))
                Assert.AreEqual(elem, expected);
        }
    }
    public class Program
    {
        static void Main(string[] args)
        {
        }

        public static bool GetResult(BinaryTree<int> binaryTree, out int elem)
        {
            elem = 0;
            var queue = new Queue<TreeNode<int>>();
            queue.Enqueue(binaryTree.root);
            while(queue.Count != 0)
            {
                var curNode = queue.Dequeue();
                if ((curNode.Left != null && curNode.Value % curNode.Left.Value == 0)||
                    (curNode.Right != null && curNode.Value % curNode.Right.Value ==0))
                {
                    elem = curNode.Value;
                    return true;
                }

                if (curNode.Left != null)
                    queue.Enqueue(curNode.Left);
                if (curNode.Right != null)
                    queue.Enqueue(curNode.Right);
            }
            return false;
        }

        public static BinaryTree<T> MakeTree<T>(T[] valueArray)
            where T : IComparable
        {
            var tree = new BinaryTree<T>();
            foreach (var e in valueArray)
                tree.Add(e);
            return tree;
        }
    }

    public class TreeNode<T>
    {
        public T Value;
        public TreeNode<T> Left, Right;
    }

    public class BinaryTree<Tvalue>
        where Tvalue : IComparable
    {
        public TreeNode<Tvalue> root;
        public void Add(Tvalue value)
        {
            if (root == null)
                root = new TreeNode<Tvalue> { Value = value };
            else
            {
                var currentNode = root;
                while (true)
                {
                    var t = GetSubTree(currentNode, value);
                    if (t == null)
                    {
                        AddLeaf(currentNode, value);
                        break;
                    }
                    currentNode = t;
                }
            }
        }

        private void AddLeaf(TreeNode<Tvalue> treeNode, Tvalue value)
        {
            if (value.CompareTo(treeNode.Value) >= 0)
                treeNode.Right = new TreeNode<Tvalue> { Value = value };
            else
                treeNode.Left = new TreeNode<Tvalue> { Value = value };
        }

        private TreeNode<Tvalue> GetSubTree(TreeNode<Tvalue> treeNode, Tvalue value)
        {
            if (value.CompareTo(treeNode.Value) >= 0)
                return treeNode.Right;
            else
                return treeNode.Left;
        }
    }
}
