using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glitch.Collections
{
    public class RedBlackTree<T>
    {
        private IComparer<T> comparer;
        private Node? root;

        public RedBlackTree() 
            : this(Comparer<T>.Default) { }

        public RedBlackTree(IComparer<T> comparer)
        {
            this.comparer = comparer;
        }

        public Node? Root => root;

        public IEnumerable<Node> PreOrder() => PreOrder(root);
        public IEnumerable<Node> InOrder() => InOrder(root);
        public IEnumerable<Node> PostOrder() => PostOrder(root);
        public IEnumerable<Node> LevelOrder() => LevelOrder(root);

        public void Add(T value)
        {
            if (root is null)
            {
                root = new Node(Color.Black, value);
                return;
            }

            root = Insert(root, value);
        }

        public void Remove(T value) => root = Delete(root, value);

        private Node Insert(Node? node, T value)
        {
            if (node is null)
            {
                return new Node(Color.Red, value);
            }

            int compared = comparer.Compare(node.Value, value);

            if (compared > 0)
            {
                node.Left = Insert(node.Left, value);
            }

            if (compared < 0)
            {
                node.Right = Insert(node.Right, value);
            }

            throw new ArgumentException($"{value} has already been added");
        }

        private Node? Delete(Node? node, T value)
        {
            if (node is null)
            {
                return null;
            }

            int compared = comparer.Compare(node.Value, value);

            if (compared > 0)
            {
                node.Left = Delete(node.Left, value);
            }

            if (compared < 0)
            {
                node.Right = Delete(node.Right, value);
            }

            if (node.Left is null && node.Right is null)
            {
                return null;
            }

            if (node.Left is not null && node.Right is not null)
            {
                throw new NotImplementedException("Still haven't implemented in order successor replacement");
            }

            return node.Left ?? node.Right;
        }

        private IEnumerable<Node> PreOrder(Node? node)
        {
            if (node is null)
            {
                yield break;
            }

            yield return node;

            foreach (var child in PreOrder(node.Left).Concat(PreOrder(node.Right)))
            {
                yield return child;
            }
        }

        private IEnumerable<Node> InOrder(Node? node)
        {
            if (node is null)
            {
                yield break;
            }

            foreach (var child in InOrder(node.Left))
            {
                yield return child;
            }

            yield return node;

            foreach (var child in InOrder(node.Right))
            {
                yield return child;
            }
        }

        private IEnumerable<Node> PostOrder(Node? node)
        {
            if (node is null)
            {
                yield break;
            }

            foreach (var child in PostOrder(node.Left).Concat(PostOrder(node.Right)))
            {
                yield return child;
            }

            yield return node;
        }

        private IEnumerable<Node> LevelOrder(Node? node)
        {
            if (node is null)
            {
                yield break;
            }

            var queue = new Queue<Node>();

            queue.Enqueue(node);

            do
            {
                var current = queue.Dequeue();

                if (current.Left != null)
                {
                    queue.Enqueue(current.Left);
                }

                if (current.Right != null)
                {
                    queue.Enqueue(current.Right);
                }

                yield return current;
            }
            while (queue.Count > 0);
        }

        public enum Color { Black, Red }

        public class Node
        {
            internal Node(Color color, T value)
            {
                Color = color;
                Value = value;
            }

            public T Value { get; internal set; }
            public Color Color { get; internal set; }
            public Node? Left { get; internal set; }
            public Node? Right { get; internal set; }
        }
    }
}
