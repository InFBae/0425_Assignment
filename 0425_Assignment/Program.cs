using System.Security.Cryptography;

namespace _0425_Assignment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DataStructure.BinarySearchTree<int> bst = new DataStructure.BinarySearchTree<int>();

            for(int i = 0; i < 10; i++)
            {
                //Random random = new Random();
                //bst.Add(random.Next(1,20));
                bst.Add(i);
            }
            bst.Print();
            Console.WriteLine();

            Console.WriteLine(bst.FindNode(5).Value);

            bst.Remove(5);
            bst.Remove(6);
            bst.Print();

            bst.Refactoring();
        }
    }
}