using System;
using System.IO;

namespace MaxmimumSumOfTheNumbers
{
    class TriangleCalculater
    {
        static void Main(string[] args)
        {
            var number = GetNumbers(); //Getting numbers

            string[] lines = number.Split('\n'); //Split numbers

            var matrix = matrixmaking(lines); //Making two dimensional array

            var total = checking(lines, matrix); //Travelling inside numbers

            Console.WriteLine($"Maximum sum of the numbers: {total[0, 0]}"); //Writes total to console

            Console.ReadKey();

        }
        private static string GetNumbers()
        {
            string input = File.ReadAllText(@"numbers.txt"); //Getting numbers from text file
            return input;
        }
        private static int[,] checking(string[] lines, int[,] matrix) //Traveling numbers and calculating maximum sum of the numbers
        {
            for (int i = lines.Length - 2; i >= 0; i--)
            {
                for (int j = 0; j < lines.Length; j++)
                {

                    if (!PrimeCheck(matrix[i, j])) //Prime checking
                    {
                        matrix[i, j] = Math.Max(matrix[i, j] + matrix[i + 1, j], matrix[i, j] + matrix[i + 1, j + 1]); //Sum of the non prime numbers
                    }
                }
            }
            return matrix;
        }
        private static int[,] matrixmaking(string[] lines) //Making two dimensional array with the giving numbers
        {
            int[,] matrix = new int[lines.Length, lines.Length + 1];

            for (int row = 0; row < lines.Length; row++)
            {
                var eachnumbers = lines[row].Trim().Split(' '); //Splitting each numbers

                for (int column = 0; column < eachnumbers.Length; column++)
                {
                    int number;
                    int.TryParse(eachnumbers[column], out number);
                    matrix[row, column] = number;
                }
            }
            return matrix;
        }
        public static bool PrimeCheck(int a) //Checking prime numbers 
        {

            if ((a & 1) == 0)
            {
                if (a == 2) //Control for two
                {
                    return true;
                }
                return false;
            }

            for (int i = 3; i <= a; i += 2) //Control for other primes
            {
                if ((a % i) == 0)
                {
                    return false;
                }
            }
            return a == 0;

        }
    }
}
