using BinaryTree;
using PlantsLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlantsLibraryVer2;

namespace BinaryTreeTests
{
    [TestClass]
    public class TreeNodeTests
    {
        [TestMethod]
        public void TreeNodeConstructorSetsDataCorrectly()
        {
            var plant = new Plant("Дуб", "Зеленый");
            var node = new TreeNode<Plant>(plant);

            Assert.AreEqual(plant, node.Data);
            Assert.IsNull(node.Left);
            Assert.IsNull(node.Right);
        }

        [TestMethod]
        public void TreeNodeCanSetLeftAndRightNodes()
        {
            var root = new TreeNode<Plant>(new Plant("Дуб", "Зеленый"));
            var left = new TreeNode<Plant>(new Plant("Береза", "Белый"));
            var right = new TreeNode<Plant>(new Plant("Сосна", "Темно-зеленый"));

            root.Left = left;
            root.Right = right;

            Assert.AreEqual(left, root.Left);
            Assert.AreEqual(right, root.Right);
        }
    }
}