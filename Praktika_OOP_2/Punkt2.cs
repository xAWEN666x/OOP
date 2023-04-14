using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktika_OOP_2
{
    internal class Punkt2
    {
        private static int FindGCDEuclid(int a, int b)
        {
            if (a == 0) { return b; }
            while (b != 0)
            {
                if (a > b)
                {
                    a -= b;
                }
                else
                {
                    b -= a;
                }
            }
            return a;
        }
        /// <summary>
        /// Реализует метод Евклида для нахождения наибольшего общего делителя множества чисел.
        /// </summary>
        /// <param name="first">Первое число.</param>
        /// <param name="second">Второе число.</param>
        /// <param name="numbers">Последующие числа.</param>
        /// <returns>Наибольший общий делитель множества чисел.</returns>
        /// <remarks>
        /// При работе с большими числами может произойти переполнение. 
        /// Рекомендуется использовать тип данных, достаточный для хранения результатов вычислений,
        /// либо обрабатывать переполнение.
        /// </remarks>
        public static int FindGCDEuclid(params int[] numbers)
        {
            if (numbers.Length == 0) { return 0; }
            if (numbers.Length == 1) { return numbers[0]; }
            int result = FindGCDEuclid(numbers[0], numbers[1]);

            for (int i = 2; i < numbers.Length; i++)
            {
                result = FindGCDEuclid(result, numbers[i]);
            }

            return result;
        }
        public static int FindGCDEuclid(params string[] str_numbers)
        {
            int[] numbers = new int[str_numbers.Length];

            for (int i = 0; i < str_numbers.Length; i++)
            {
                numbers[i] = int.Parse(str_numbers[i]);
            }
            return FindGCDEuclid(numbers);
        }
    }
}