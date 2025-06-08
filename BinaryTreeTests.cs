using BinaryTree;
using PlantsLibraryVer2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace BinaryTreeTests
{
    [TestClass]
    public class BinaryTreeTests
    {
        private BinaryTree<Plant> tree;

        [TestInitialize]
        public void TestInitialize()
        {
            tree = new BinaryTree<Plant>();
        }

        [TestMethod]
        public void NewTreeIsEmpty()
        {
            Assert.IsTrue(tree.Count == 0);
        }

        [TestMethod]
        public void AddIncreasesCount()
        {
            tree.Add(new Plant("Дуб", "Зеленый"));
            Assert.AreEqual(1, tree.Count);
        }

        [TestMethod]
        public void AddPlacesSmallerElementsToLeft()
        {
            var oak = new Plant("Дуб", "Зеленый");
            var birch = new Plant("Береза", "Белый");

            tree.Add(oak);
            tree.Add(birch);

            Assert.IsTrue(tree.Contains(birch));
        }

        [TestMethod]
        public void AddPlacesLargerElementsToRight()
        {
            var oak = new Plant("Дуб", "Зеленый");
            var pine = new Plant("Сосна", "Темно-зеленый");

            tree.Add(oak);
            tree.Add(pine);

            Assert.IsTrue(tree.Contains(pine));
        }

        [TestMethod]
        public void ContainsReturnsFalseForEmptyTree()
        {
            Assert.IsFalse(tree.Contains(new Plant("Дуб", "Зеленый")));
        }

        [TestMethod]
        public void ContainsReturnsTrueForExistingElement()
        {
            var plant = new Plant("Дуб", "Зеленый");
            tree.Add(plant);
            Assert.IsTrue(tree.Contains(plant));
        }

        [TestMethod]
        public void RemoveDecreasesCount()
        {
            var plant = new Plant("Дуб", "Зеленый");
            tree.Add(plant);
            tree.Remove(plant);
            Assert.AreEqual(0, tree.Count);
        }

        [TestMethod]
        public void RemoveReturnsFalseForNonExistingElement()
        {
            Assert.IsFalse(tree.Remove(new Plant("Дуб", "Зеленый")));
        }

        [TestMethod]
        public void RemoveReturnsTrueForExistingElement()
        {
            var plant = new Plant("Дуб", "Зеленый");
            tree.Add(plant);
            Assert.IsTrue(tree.Remove(plant));
        }

        [TestMethod]
        public void ClearResetsCountToZero()
        {
            tree.Add(new Plant("Дуб", "Зеленый"));
            tree.Clear();
            Assert.AreEqual(0, tree.Count);
        }

        [TestMethod]
        public void BuildBalancedTreeCreatesBalancedStructure()
        {
            var plants = new Plant[]
            {
                new Plant("Акация", "Зеленый"),
                new Plant("Береза", "Белый"),
                new Plant("Дуб", "Темно-зеленый"),
                new Plant("Ель", "Зеленый"),
                new Plant("Сосна", "Темно-зеленый")
            };

            tree.BuildBalancedTree(plants);
            Assert.AreEqual(plants.Length, tree.Count);
        }

        [TestMethod]
        public void ToSearchTreeCreatesValidSearchTree()
        {
            var plants = new Plant[]
            {
                new Plant("Акация", "Зеленый"),
                new Plant("Береза", "Белый"),
                new Plant("Дуб", "Темно-зеленый")
            };

            tree.BuildBalancedTree(plants);
            var searchTree = tree.ToSearchTree();

            Assert.AreEqual(tree.Count, searchTree.Count);
            Assert.IsTrue(searchTree.Contains(plants[0]));
        }

        [TestMethod]
        public void AverageCalculatesCorrectValueForTrees()
        {
            var plants = new Plant[]
            {
                new Tree("Дуб", "Зеленый", 10),
                new Tree("Сосна", "Темно-зеленый", 20),
                new Tree("Береза", "Белый", 30)
            };

            tree.BuildBalancedTree(plants);
            double avg = tree.Average(p => (p as Tree)?.Height ?? 0);

            Assert.AreEqual(20.0, avg);
        }

        [TestMethod]
        public void EnumeratorTraversesInOrder()
        {
            var plants = new Plant[]
            {
                new Plant("Береза", "Белый"),
                new Plant("Дуб", "Зеленый"),
                new Plant("Акация", "Зеленый")
            };

            tree.BuildBalancedTree(plants.OrderBy(p => p.Name).ToArray());
            var result = tree.ToList();

            Assert.AreEqual("Акация", result[0].Name);
            Assert.AreEqual("Береза", result[1].Name);
            Assert.AreEqual("Дуб", result[2].Name);
        }

        [TestMethod]
        public void PrintByLevelsDoesNotThrowForEmptyTree()
        {
            tree.PrintByLevels();
        }

        [TestMethod]
        public void PrintByLevelsDoesNotThrowForNonEmptyTree()
        {
            tree.Add(new Plant("Дуб", "Зеленый"));
            tree.PrintByLevels();
        }
    }
}