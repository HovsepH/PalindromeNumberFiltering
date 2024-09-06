using System.Collections.Concurrent;

namespace PalindromeNumberFiltering
{
    /// <summary>
    /// A static class containing methods for filtering palindrome numbers from a collection of integers.
    /// </summary>
    public static class Selector
    {
        /// <summary>
        /// Retrieves a collection of palindrome numbers from the given list of integers using sequential filtering.
        /// </summary>
        /// <param name="numbers">The list of integers to filter.</param>
        /// <returns>A collection of palindrome numbers.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the input list 'numbers' is null.</exception>
        public static IList<int> GetPalindromeInSequence(IList<int>? numbers)
        {
            ArgumentNullException.ThrowIfNull(numbers);
            return numbers.Where(number => IsPalindrome(number)).ToList();
        }

        /// <summary>
        /// Retrieves a collection of palindrome numbers from the given list of integers using parallel filtering.
        /// </summary>
        /// <param name="numbers">The list of integers to filter.</param>
        /// <returns>A collection of palindrome numbers.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the input list 'numbers' is null.</exception>
        public static IList<int> GetPalindromeInParallel(IList<int> numbers)
        {
            var palindromeNumbers = new ConcurrentBag<int>();
            _ = Parallel.ForEach(numbers, number =>
             {
                 if (IsPalindrome(number))
                 {
                     palindromeNumbers.Add(number);
                 }
             });

            return palindromeNumbers.ToList();
        }

        /// <summary>
        /// Checks whether the given integer is a palindrome number.
        /// </summary>
        /// <param name="number">The integer to check.</param>
        /// <returns>True if the number is a palindrome, otherwise false.</returns>
        private static bool IsPalindrome(int number)
        {
            if (number < 0)
            {
                return false;
            }

            return IsPositiveNumberPalindrome(number, (int)Math.Pow(10, GetLength(number) - 1));
        }

        /// <summary>
        /// Recursively checks whether a positive number is a palindrome.
        /// </summary>
        /// <param name="number">The positive number to check.</param>
        /// <param name="divider">The divider used in the recursive check.</param>
        /// <returns>True if the positive number is a palindrome, otherwise false.</returns>
        private static bool IsPositiveNumberPalindrome(int number, int divider)
        {
            var length = GetLength(number);
            if (number < 10)
            {
                return true;
            }

            int left = number / divider;
            int right = number % 10;
            var nextNumber = (number % divider) / 10;
            if (left == right)
            {
                return IsPositiveNumberPalindrome(nextNumber, divider / 100);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the number of digits in the given integer.
        /// </summary>
        /// <param name="number">The integer to count digits for.</param>
        /// <returns>The number of digits in the integer.</returns>
        private static byte GetLength(int number)
        {
            if (number < 0)
            {
                number = -number;
            }

            switch (number)
            {
                case >= 1000000000:
                    return 10;
                case >= 100000000:
                    return 9;
                case >= 10000000:
                    return 8;
                case >= 1000000:
                    return 7;
                case >= 100000:
                    return 6;
                case >= 10000:
                    return 5;
                case >= 1000:
                    return 4;
                case >= 100:
                    return 3;
                case >= 10:
                    return 2;
                default:
                    return 1;
            }
        }
    }
}
