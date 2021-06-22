using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuSolver
{
    class Program
    {
        private static readonly int[][] originalArray = {
            new[] {0, 0, 0, 0, 3, 0, 0, 0, 6},
            new[] {0, 3, 0, 0, 0, 0, 2, 0, 0},
            new[] {8, 0, 0, 0, 0, 0, 7, 5, 0},
            new[] {2, 0, 8, 0, 0, 1, 0, 0, 4},
            new[] {0, 0, 0, 0, 0, 0, 0, 0, 5},
            new[] {1, 0, 0, 0, 7, 8, 0, 0, 2},
            new[] {0, 0, 9, 0, 0, 0, 8, 0, 0},
            new[] {0, 0, 0, 2, 5, 0, 0, 4, 0},
            new[] {0, 6, 0, 0, 0, 4, 0, 0, 0}
        };

        private static void PrintPattern(int index,int length, Action printAction)
        {
            if ((index+1) % 3 == 0 && index != length - 1)
            {
                printAction();
            }
        }

        private static void PrintSudoku(int[][] inputArray)
        {
            for (var i = 0; i <= inputArray.Length -1 ; i++)
            {
                for (var j = 0; j <= inputArray[i].Length -1 ; j++)
                {
                    var length = inputArray[i].Length;
                    Console.Write($"{inputArray[i][j]} ");
                    PrintPattern(j, length, () => { Console.Write($"| "); });
                }
                Console.WriteLine();
                PrintPattern(i, originalArray.Length, () =>
                {
                    Console.WriteLine("- - - | - - - | - - - ");
                });
            }
        }

        private static (int i, int j, bool isFound) FindZeroPosition(int[][] inputArray)
        {
            for (var i = 0; i <= inputArray.Length -1 ; i++)
            {
                for (var j = 0; j <= inputArray[i].Length -1 ; j++)
                {
                    if (inputArray[i][j] == 0)
                    {
                        return (i, j, true);
                    }
                }
            }

            return (-1, -1, false);
        }

        private static bool IsValid(IReadOnlyList<int[]> array, int number, (int, int) position)
        {
            var (row, col) = position;
            for (var i = 0; i <= array[row].Length - 1; i++)
            {
                if (array[row][i] == number && i != col)
                {
                    return false;
                }
            }

            for (var j = 0; j <= array.Count - 1; j++)
            {
                if (array[j][col] == number && j!=row)
                {
                    return false;
                }
            }

            var x = col / 3;
            var y = row / 3;

            for (var i = y*3; i <= y*3 + 2; i++)
            {
                for (var j = x*3; j <= x*3 + 2; j++)
                {
                    if (array[i][j] == number && i != row && j != col)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static bool Solve(int[][] array)
        {
            var (i1, j, isFound) = FindZeroPosition(array);
            if (!isFound)
            {
                return true;
            }

            var position = (i: i1, j: j);

            for (var i = 1; i <= 9 ; i++)
            {
                if (!IsValid(array, i, position)) continue;
                
                array[position.i][position.j] = i;
                if (Solve(array))
                {
                    return true;
                }

                array[position.i][position.j] = 0;
            }

            return false;
        }
            
        static void Main(string[] args)
        {
            Solve(originalArray);
            PrintSudoku(originalArray);
            Console.ReadLine();
        }
    }
}