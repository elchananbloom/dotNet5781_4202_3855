using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_00_4202_3855
{
    partial class Program
    {
        static void Main(string[] args)
        {

            welcome3855();
            welcome4202();
            Console.ReadKey();
        }
        static partial void welcome4202();
        private static void welcome3855()
        {
            Console.WriteLine("Enter your name: ");
            string name = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", name);
        }
    }
}
