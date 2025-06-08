using System;
using System.Collections;
using System.Collections.Generic;
using PlantsLibraryVer2;

namespace PlantTree
{
    // Бинарное дерево
    public class BinaryTree<T> : IEnumerable<T> where T : IComparable<T>, ICloneable
    {
        private TreeNode<T> root;
        private int count;

        public BinaryTree()
        {
            root = null;
            count = 0;
        }

        // Добавление элемента в дерево поиска
        public void Add(T data)
        {
            if (root == null)
            {
                root = new TreeNode<T>(data);
                count++;
                return;
            }

            TreeNode<T> current = root;
            while (true)
            {
                if (data.CompareTo(current.Data) < 0)
                {
                    if (current.Left == null)
                    {
                        current.Left = new TreeNode<T>(data);
                        count++;
                        return;
                    }
                    current = current.Left;
                }
                else
                {
                    if (current.Right == null)
                    {
                        current.Right = new TreeNode<T>(data);
                        count++;
                        return;
                    }
                    current = current.Right;
                }
            }
        }

        // Построение идеально сбалансированного дерева
        public void BuildBalancedTree(T[] elements)
        {
            root = BuildBalanced(elements, 0, elements.Length - 1);
            count = elements.Length;
        }

        private TreeNode<T> BuildBalanced(T[] elements, int start, int end)
        {
            if (start > end) return null;

            int mid = (start + end) / 2;
            TreeNode<T> node = new TreeNode<T>(elements[mid]);

            node.Left = BuildBalanced(elements, start, mid - 1);
            node.Right = BuildBalanced(elements, mid + 1, end);

            return node;
        }

        // Преобразование в дерево поиска
        public BinaryTree<T> ToSearchTree()
        {
            List<T> elements = new List<T>();
            InOrderTraversal(root, elements);

            BinaryTree<T> searchTree = new BinaryTree<T>();
            foreach (var item in elements)
            {
                searchTree.Add((T)item.Clone());
            }

            return searchTree;
        }

        private void InOrderTraversal(TreeNode<T> node, List<T> elements)
        {
            if (node == null) return;

            InOrderTraversal(node.Left, elements);
            elements.Add(node.Data);
            InOrderTraversal(node.Right, elements);
        }

        // Удаление узла 
        public bool Remove(T data)
        {
            TreeNode<T> current = root;
            TreeNode<T> parent = null;

            // Поиск узла для удаления
            while (current != null && current.Data.CompareTo(data) != 0)
            {
                parent = current;
                if (data.CompareTo(current.Data) < 0)
                    current = current.Left;
                else
                    current = current.Right;
            }

            if (current == null) return false; // Узел не найден

            if (current.Left == null || current.Right == null)
            {
                TreeNode<T> newChild = current.Left ?? current.Right;

                if (parent == null)
                    root = newChild;
                else if (parent.Left == current)
                    parent.Left = newChild;
                else
                    parent.Right = newChild;
            }
            else
            {
                // Находим минимальный элемент в правом поддереве
                TreeNode<T> minParent = current;
                TreeNode<T> min = current.Right;

                while (min.Left != null)
                {
                    minParent = min;
                    min = min.Left;
                }
                current.Data = min.Data;

                // Удаляем минимальный элемент
                if (minParent.Left == min)
                    minParent.Left = min.Right;
                else
                    minParent.Right = min.Right;
            }

            count--;
            return true;
        }

        // Поиск элемента
        public bool Contains(T data)
        {
            TreeNode<T> current = root;
            while (current != null)
            {
                int comparison = data.CompareTo(current.Data);
                if (comparison == 0)
                    return true;
                else if (comparison < 0)
                    current = current.Left;
                else
                    current = current.Right;
            }
            return false;
        }

        // Очистка дерева
        public void Clear()
        {
            root = null;
            count = 0;
        }
        public int Count => count;

        // Печать дерева
        public void PrintByLevels()
        {
            if (root == null)
            {
                Console.WriteLine("Дерево пустое");
                return;
            }

            Queue<TreeNode<T>> queue = new Queue<TreeNode<T>>();
            queue.Enqueue(root);
            int level = 0;

            while (queue.Count > 0)
            {
                int levelSize = queue.Count;
                Console.Write($"Уровень {level}: ");

                for (int i = 0; i < levelSize; i++)
                {
                    TreeNode<T> node = queue.Dequeue();

                    // Определение типа ветви
                    string branchType = "";
                    if (node == root)
                        branchType = " (корень)";
                    else if (node.Data.CompareTo(root.Data) < 0)
                        branchType = " (левая ветвь)";
                    else
                        branchType = " (правая ветвь)";

                    Console.Write(node.Data + branchType);

                    if (i < levelSize - 1) Console.Write(", ");

                    if (node.Left != null) queue.Enqueue(node.Left);
                    if (node.Right != null) queue.Enqueue(node.Right);
                }

                Console.WriteLine();
                level++;
            }
        }

        // Вычисление среднего арифметического для высоты
        public double Average(Func<T, int> selector)
        {
            if (root == null) return 0;

            int sum = 0;
            int count = 0;

            foreach (var item in this)
            {
                sum += selector(item);
                count++;
            }

            return (double)sum / count;
        }
        public IEnumerator<T> GetEnumerator()
        {
            return InOrderTraversal().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private IEnumerable<T> InOrderTraversal()
        {
            Stack<TreeNode<T>> stack = new Stack<TreeNode<T>>();
            TreeNode<T> current = root;

            while (current != null || stack.Count > 0)
            {
                while (current != null)
                {
                    stack.Push(current);
                    current = current.Left;
                }

                current = stack.Pop();
                yield return current.Data;
                current = current.Right;
            }
        }
    }
}