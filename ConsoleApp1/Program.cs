using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            // сортировка бога
            /*static bool IsSorted(int[] arr)
            {
                for (int i = 1; i < arr.Length; i++)
                {
                    if (arr[i - 1] > arr[i])
                    {
                        return false;
                    }
                }
                return true;
            }

            static T[] Shuffle<T>(T[] array)
            {
                var random = new Random();
                for (int i = array.Length; i > 1; i--)
                {
                    int j = random.Next(i);

                    T tmp = array[j];
                    array[j] = array[i - 1];
                    array[i - 1] = tmp;
                }
                return array;
            }

            int Min = 0;
            int Max = 9;

            int[] randomAray = new int[8];

            Random randNum = new Random();
            for (int i = 0; i < randomAray.Length; i++)
            {
                randomAray[i] = randNum.Next(Min, Max);
            }

            foreach (var value in randomAray)
            {
                Console.Write(value + " ");
            }

            Console.WriteLine("next");
            randomAray = Shuffle<int>(randomAray);

            var counter = 0;

            while (true)
            {
                randomAray = Shuffle<int>(randomAray);

                if (IsSorted(randomAray))
                {
                    Console.WriteLine("Done ! ");
                    foreach (var value in randomAray)
                    {
                        Console.Write(value + " ");
                    }
                    break;
                }

                counter += 1;
                Console.WriteLine(counter);
            }
            Console.ReadKey();*/
        }
    }
}
