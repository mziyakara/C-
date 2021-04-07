using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

internal static class Program
{

    public static Dictionary<int, bool> PrimeCache = new Dictionary<int, bool>();

    private static void Main(string[] args)
    {
        var result = GetInput()
            .MakeitArray()
            .MakeitTwoDArray()
            .MakePrimeNumbersZero()
            .TravelThroughNumbers();

        Console.WriteLine($"Maximum sum of the non-prime numbers:  {result}");
        Console.ReadKey();
    }

    private static string GetInput()
    {
        string input = File.ReadAllText(@"numbers.txt");
        return input;
    }


    private static string[] MakeitArray(this string input)
    {
        return input.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
    }

    private static int[,] MakeitTwoDArray(this string[] Lines)
    {
        var lineHolder = new int[Lines.Length, Lines.Length + 1];

        for (var row = 0; row < Lines.Length; row++)
        {
            var charactersInLine = Lines[row].takeNumbers();

            for (var column = 0; column < charactersInLine.Length; column++)
                lineHolder[row, column] = charactersInLine[column];
        }
        return lineHolder;
    }


    private static int[] takeNumbers(this string rows)
    {
        return
            Regex
                .Matches(rows, "[0-9]+")
                .Cast<Match>()
                .Select(m => int.Parse(m.Value)).ToArray();
    }


    private static int[,] MakePrimeNumbersZero(this int[,] lineHolder)
    {
        var length = lineHolder.GetLength(0);
        for (var i = 0; i < length; i++)
        {
            for (var j = 0; j < length; j++)
            {
                if (lineHolder[i, j] == 0)
                    continue;
                if (PrimeCheck(lineHolder[i, j]))
                {
                    lineHolder[i, j] = 0;
                }
            }
        }
        return lineHolder;
    }

    private static int TravelThroughNumbers(this int[,] lineHolder)
    {
        var temp = lineHolder;
        var length = lineHolder.GetLength(0);


        for (var i = length - 2; i >= 0; i--)
        {
            for (var j = 0; j < length; j++)
            {
                var c = temp[i, j];
                var a = temp[i + 1, j];
                var b = temp[i + 1, j + 1];
                if ((!PrimeCheck(c) && !PrimeCheck(a)) || (!PrimeCheck(c) && !PrimeCheck(b)))
                {
                    lineHolder[i, j] = c + Math.Max(a, b);
                }

            }
        }
        return lineHolder[0, 0];
    }


    public static bool PrimeCheck(this int number)
    {
        if (PrimeCache.ContainsKey(number))
        {
            bool value;
            PrimeCache.TryGetValue(number, out value);
            return value;
        }

        if (number == 1|| number == 0)
        {
            return false;
        }

        int counter = 0;

        for (int i = 2; i < number; i++)
        {
            if (number % i == 0)
            {
                counter++;
            }
           
        }
        
        if (counter == 0)
        {
            return true;
        }      
        return false;
    }

}
