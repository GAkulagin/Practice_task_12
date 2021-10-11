using System;
using System.Collections.Generic;
using System.Threading;

namespace Task_12
{
    //Выполнить сравнение двух предложенных методов сортировки одномерных массивов, содержащих n элементов, по количеству пересылок и сравнений.
    //Провести анализ методов сортировки для трех массивов: упорядоченного по возрастанию, упорядоченного по убыванию и неупорядоченного.
    //Все три массива следует отсортировать обоими методами сортировки.
    //Найти в литературе теоретические оценки сложности каждого из методов и сравнить их с оценками, полученными на практике.
    //Сделать выводы о том, насколько отличаются теоретические и практические оценки количества операций, объяснить почему это происходит. Сравнить оценки сложности двух алгоритмов

    // Сортировка простым выбором и поразрядная сортировка 
	// commit this comment

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Сравнение методов сортировки Шелла и поразрядной сортировки !!!Cool!!!");
            Console.WriteLine();

            int size = InputInteger(50, 250, "Введите размер массива");
            int swapCount = 0;  // Счетчик перестановок
            int compCount = 0;  // Счетчик сравнений

            int[] ascendArr = CreateArray(size);
            Thread.Sleep(100);
            Array.Sort(ascendArr);
            int[] descendArr = CreateArray(size);
            Thread.Sleep(100);
            Array.Sort(descendArr);
            Array.Reverse(descendArr);
            int[] unsortedArr = CreateArray(size);

            Console.WriteLine("Массив, упорядоченный по возрастанию:");
            PrintArray(ascendArr);
            Console.WriteLine("Массив, упорядоченный по убыванию:");
            PrintArray(descendArr);
            Console.WriteLine("Неупорядоченный массив:");
            PrintArray(unsortedArr);

            int[] ascendArrCopy = new int[size];
            Array.Copy(ascendArr, ascendArrCopy, size);
            int[] descendArrCopy = new int[size];
            Array.Copy(descendArr, descendArrCopy, size);
            int[] unsortedArrCopy = new int[size];
            Array.Copy(unsortedArr, unsortedArrCopy, size);

            Console.WriteLine("Поразрядная сортировка:");

            PrintResult(RadixSort(ascendArr, 2, 10, ref swapCount), swapCount, compCount, "Массив, упорядоченный по возрастанию:");
            swapCount = 0; compCount = 0;

            PrintResult(RadixSort(descendArr, 2, 10, ref swapCount), swapCount, compCount, "Массив, упорядоченный по убыванию:");
            swapCount = 0;

            PrintResult(RadixSort(unsortedArr, 2, 10, ref swapCount), swapCount, compCount, "Неупорядоченный массив:");
            swapCount = 0;

            Console.WriteLine("Сортировка простым выбором:");

            PrintResult(SimpleSort(ascendArrCopy, ref swapCount, ref compCount), swapCount, compCount, "Массив, упорядоченный по возрастанию:");
            swapCount = 0; compCount = 0;

            PrintResult(SimpleSort(descendArrCopy, ref swapCount, ref compCount), swapCount, compCount, "Массив, упорядоченный по убыванию:");
            swapCount = 0; compCount = 0;

            PrintResult(SimpleSort(unsortedArrCopy, ref swapCount, ref compCount), swapCount, compCount, "Неупорядоченный массив:");
        }


        static int[] CreateArray(int size)
        {
            int[] array = new int[size];
            Random rand = new Random();

            for (int i = 0; i < size; i++)
                array[i] = rand.Next(0, 100);

            return array;
        }

        static int InputInteger(int leftBorder, int rightBorder, string message)
        {
            int value;
            bool checkValue;
            do
            {
                Console.WriteLine(message);
                checkValue = int.TryParse(Console.ReadLine(), out value);

                if (!checkValue)
                    Console.WriteLine("Неверный ввод данных");
                else if ((value > rightBorder) || (value < leftBorder))
                {
                    Console.WriteLine("Неверный ввод данных");
                    checkValue = false;
                }

            } while (!checkValue);

            return value;
        }

        static void PrintArray(int[] array)
        {
            foreach (int item in array)
                Console.Write(item + " ");

            Console.WriteLine();
            Console.WriteLine();
        }

        static void PrintResult(int[] array, int swapCount, int compCount, string arrType)
        {
            Console.WriteLine();
            Console.WriteLine(arrType);
            PrintArray(array);
            Console.WriteLine($"Число сравнений: {compCount}");
            Console.WriteLine($"Число перестановок: {swapCount}");
            Console.WriteLine();
        }

        // Поразрядная сортировка
        static int[] RadixSort(int[] array, int length, int range, ref int swapCount)
        {
            // length - максимальное число разрядов числа
            // range - число значений разряда (10)

            List<List<int>> lists = new List<List<int>>();

            for (int i = 0; i < range; i++)
               lists.Add(new List<int>());

            for (int i = 0; i < length; i++)
            {
                //распределение по спискам
                for (int j = 0; j < array.Length; j++)
                {
                    int temp = (array[j] % (int)Math.Pow(range, i + 1)) / (int)Math.Pow(range, i);
                    lists[temp].Add(array[j]);

                    swapCount++;
                }

                //сборка
                int count = 0;
                for (int k = 0; k < range; k++)
                {
                    for (int j = 0; j < lists[k].Count; j++)
                    {
                        array[count] = lists[k][j];
                        count++;

                        swapCount++;
                    }
                }

                for (int m = 0; m < range; m++)
                    lists[m].Clear();
            }

            return array;
        }

        // Сортировка простым выбором
        static int[] SimpleSort(int[] array, ref int swapCount, ref int compCount)
        {
            for(int i = 0; i < array.Length - 1; i++)
            {
                int min = i;

                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[j] < array[min]) min = j;

                    compCount++;
                }

                int temp = array[i];
                array[i] = array[min];
                array[min] = temp;

                swapCount++;            
            }

            return array;
        }
    }
}
