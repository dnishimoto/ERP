using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.Models
{
    public static class MDArray
    {
        public static void MultidimensionalArray()
        {
            long[,] data = new long[2, 4]{
            {1,2,3,4},
            {7,8,9,10}
            };

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Console.Write("{0}", data[i, j]);
                }
                Console.WriteLine();

            }
            long[][] data2 = new long[2][]
                {
                    new long[4]{1,2,3,4},
                    new long[4]{7,8,9,10}
                };
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Console.Write("{0}", data2[i][j]);
                }
                Console.WriteLine();

            }


            string[] movieStars = new string[] {
                "Harrison Ford",
                "John Wayne",
                "Elizabeth Taylor"
            };
            foreach (var item in movieStars)
            {
                Console.WriteLine("{0}", item);
            }

            char[] charList = new char[3] { 'a', 'b', 'c' };
            Console.WriteLine("{0}", String.Join("+", charList));


        }
    }
}
