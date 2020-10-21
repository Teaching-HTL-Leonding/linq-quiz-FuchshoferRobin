using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace LinqQuiz.Library
{
    public static class Quiz
    {
        /// <summary>
        /// Returns all even numbers between 1 and the specified upper limit.
        /// </summary>
        /// <param name="exclusiveUpperLimit">Upper limit (exclusive)</param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Thrown if <paramref name="exclusiveUpperLimit"/> is lower than 1.
        /// </exception>
        public static int[] GetEvenNumbers(int exclusiveUpperLimit)
        {
            if (exclusiveUpperLimit < 1)
            {
                throw new ArgumentOutOfRangeException();
            }       
            var numbers = new List<int>();

            for (int i = 1; i < exclusiveUpperLimit; i++)
            {
                numbers.Add(i);
            }
            var evenNumbers = numbers.Where(number => number % 2 == 0);
            
            return evenNumbers.ToArray();
        }

        /// <summary>
        /// Returns the squares of the numbers between 1 and the specified upper limit 
        /// that can be divided by 7 without a remainder (see also remarks).
        /// </summary>
        /// <param name="exclusiveUpperLimit">Upper limit (exclusive)</param>
        /// <exception cref="OverflowException">
        ///     Thrown if the calculating the square results in an overflow for type <see cref="System.Int32"/>.
        /// </exception>
        /// <remarks>
        /// The result is an empty array if <paramref name="exclusiveUpperLimit"/> is lower than 1.
        /// The result is in descending order.
        /// </remarks>
        public static int[] GetSquares(int exclusiveUpperLimit)
        {
            int[] result = new int[0];

            if(exclusiveUpperLimit < 1)
                return result;

            var numbers = new List<int>();
            checked
            {
                for (int i = 1; i < exclusiveUpperLimit; i++)
                {
                    numbers.Add(i);
                }

                var SquareNumbers = numbers.Where(number => number % 7 == 0)
                                           .Select(number => number * number)
                                           .OrderByDescending(number => number);

                return SquareNumbers.ToArray();
            }
            
        }

        /// <summary>
        /// Returns a statistic about families.
        /// </summary>
        /// <param name="families">Families to analyze</param>
        /// <returns>
        /// Returns one statistic entry per family in <paramref name="families"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if <paramref name="families"/> is <c>null</c>.
        /// </exception>
        /// <remarks>
        /// <see cref="FamilySummary.AverageAge"/> is set to 0 if <see cref="IFamily.Persons"/>
        /// in <paramref name="families"/> is empty.
        /// </remarks>
        public static FamilySummary[] GetFamilyStatistic(IReadOnlyCollection<IFamily> families)
        {
            if (families == null)
            {
                throw new ArgumentNullException();
            }
            var familiySummary = new FamilySummary[families.Count(n => n.Persons != null)];
            familiySummary = families.Select(x => new FamilySummary() { FamilyID = x.ID, AverageAge = x.Persons.Count > 0 ? x.Persons.Average(x => x.Age) : 0, NumberOfFamilyMembers = x.Persons.Count }).ToArray();
            return familiySummary;
        }

        /// <summary>
        /// Returns a statistic about the number of occurrences of letters in a text.
        /// </summary>
        /// <param name="text">Text to analyze</param>
        /// <returns>
        /// Collection containing the number of occurrences of each letter (see also remarks).
        /// </returns>
        /// <remarks>
        /// Casing is ignored (e.g. 'a' is treated as 'A'). Only letters between A and Z are counted;
        /// special characters, numbers, whitespaces, etc. are ignored. The result only contains
        /// letters that are contained in <paramref name="text"/> (i.e. there must not be a collection element
        /// with number of occurrences equal to zero.
        /// </remarks>
        public static (char letter, int numberOfOccurrences)[] GetLetterStatistic(string text)
        {
           return text.Where(x => char.IsLetter(x)).GroupBy(x => x).Select(x => (letter: x.Key, numberOfOcc: x.Count())).ToArray();
        }
    }
}
