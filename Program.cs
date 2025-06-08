using System;
using System.Collections.Generic;
using PlantsLibraryVer2;

namespace PlantTree
{
    class Program
    {
        static void Main(string[] args)
        {
            BinaryTree<Plant> balancedTree = new BinaryTree<Plant>();
            BinaryTree<Plant> searchTree = null;

            while (true)
            {
                Console.WriteLine("\nМеню:");
                Console.WriteLine("1. Создать идеально сбалансированное дерево");
                Console.WriteLine("2. Добавить элемент в дерево");
                Console.WriteLine("3. Удалить элемент из дерева");
                Console.WriteLine("4. Найти среднее арифметическое высот деревьев");
                Console.WriteLine("5. Преобразовать в дерево поиска");
                Console.WriteLine("6. Показать идеально сбалансированное дерево");
                Console.WriteLine("7. Показать дерево поиска");
                Console.WriteLine("8. Очистить деревья");
                Console.WriteLine("9. Выход");

                Console.Write("Выберите действие: ");
                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            CreateBalancedTree(balancedTree);
                            break;
                        case "2":
                            AddElement(balancedTree);
                            break;
                        case "3":
                            RemoveElement(balancedTree);
                            break;
                        case "4":
                            CalculateAverageHeight(balancedTree);
                            break;
                        case "5":
                            searchTree = balancedTree.ToSearchTree();
                            Console.WriteLine("Дерево преобразовано в дерево поиска");
                            break;
                        case "6":
                            Console.WriteLine("\nИдеально сбалансированное дерево:");
                            balancedTree.PrintByLevels();
                            break;
                        case "7":
                            if (searchTree == null)
                                Console.WriteLine("Сначала преобразуйте дерево в дерево поиска");
                            else
                            {
                                Console.WriteLine("\nДерево поиска:");
                                searchTree.PrintByLevels();
                            }
                            break;
                        case "8":
                            balancedTree.Clear();
                            if (searchTree != null) searchTree.Clear();
                            Console.WriteLine("Деревья очищены");
                            break;
                        case "9":
                            return;
                        default:
                            Console.WriteLine("Неверный выбор. Попробуйте снова.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
        }

        static void CreateBalancedTree(BinaryTree<Plant> tree)
        {
            Console.Write("Введите количество элементов: ");
            int count = int.Parse(Console.ReadLine());

            List<Plant> plants = new List<Plant>();
            Random rnd = new Random();

            for (int i = 0; i < count; i++)
            {
                int plantType = rnd.Next(3);
                Plant plant;

                switch (plantType)
                {
                    case 0:
                        plant = new Plant();
                        break;
                    case 1:
                        plant = new Flower();
                        break;
                    case 2:
                        plant = new Tree();
                        break;
                    default:
                        plant = new Rose();
                        break;
                }

                plant.RandomInit();
                plants.Add(plant);
            }

            plants.Sort();
            tree.BuildBalancedTree(plants.ToArray());
            Console.WriteLine($"Идеально сбалансированное дерево из {count} элементов создано");
        }

        static void AddElement(BinaryTree<Plant> tree)
        {
            Console.WriteLine("Выберите тип растения:");
            Console.WriteLine("1. Простое растение");
            Console.WriteLine("2. Цветок");
            Console.WriteLine("3. Дерево");
            Console.WriteLine("4. Роза");

            Console.Write("Ваш выбор: ");
            string typeChoice = Console.ReadLine();

            Plant plant;
            switch (typeChoice)
            {
                case "1":
                    plant = new Plant();
                    break;
                case "2":
                    plant = new Flower();
                    break;
                case "3":
                    plant = new Tree();
                    break;
                case "4":
                    plant = new Rose();
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Будет создано простое растение.");
                    plant = new Plant();
                    break;
            }

            Console.WriteLine("1. Ввести данные вручную");
            Console.WriteLine("2. Сгенерировать случайные данные");
            Console.Write("Ваш выбор: ");
            string inputChoice = Console.ReadLine();

            if (inputChoice == "1")
                plant.Init();
            else
                plant.RandomInit();

            tree.Add(plant);
            Console.WriteLine("Элемент добавлен в дерево");
        }

        static void RemoveElement(BinaryTree<Plant> tree)
        {
            Console.Write("Введите название растения для удаления: ");
            string name = Console.ReadLine();

            Plant plantToRemove = null;
            foreach (var plant in tree)
            {
                if (plant.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    plantToRemove = plant;
                    break;
                }
            }

            if (plantToRemove != null)
            {
                if (tree.Remove(plantToRemove))
                    Console.WriteLine("Элемент удален из дерева");
                else
                    Console.WriteLine("Не удалось удалить элемент");
            }
            else
            {
                Console.WriteLine("Растение с таким названием не найдено");
            }
        }

        static void CalculateAverageHeight(BinaryTree<Plant> tree)
        {
            if (tree.Count == 0)
            {
                Console.WriteLine("Дерево пустое");
                return;
            }

            double average = tree.Average(p =>
            {
                if (p is Tree treeObj)
                    return treeObj.Height;
                return 0;
            });

            Console.WriteLine($"Средняя высота деревьев: {average:F2} метров");
        }
    }
}