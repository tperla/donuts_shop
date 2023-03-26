// See https://aka.ms/new-console-template for more information
using System;

namespace Stage0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome4713();
            Welcome8218();
            Console.ReadKey();
        }
        private static void Welcome4713()
        {
            Console.Write("Enter your name: ");
            string name;
            name = Console.ReadLine()!;
            Console.WriteLine("{0}, welcome to my first console application", name);
        }
        static partial void Welcome8218();
    }
}
