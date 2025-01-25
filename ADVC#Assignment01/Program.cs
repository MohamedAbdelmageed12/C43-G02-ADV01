using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Assignment01
{
    #region Q1: Generic Range<T> Class
    public class Range<T> where T : IComparable<T>
    {
        public T Minimum { get; }
        public T Maximum { get; }

        public Range(T minimum, T maximum)
        {
            if (minimum.CompareTo(maximum) > 0)
                throw new ArgumentException("Minimum value cannot be greater than maximum value.");

            Minimum = minimum;
            Maximum = maximum;
        }

        public bool IsInRange(T value)
        {
            return value.CompareTo(Minimum) >= 0 && value.CompareTo(Maximum) <= 0;
        }

        public T Length()
        {
            dynamic min = Minimum;
            dynamic max = Maximum;
            return max - min;
        }
    }
    #endregion

    #region Q2: Reverse ArrayList In-Place
    public static class ArrayListExtensions
    {
        public static void ReverseInPlace(this ArrayList list)
        {
            int left = 0;
            int right = list.Count - 1;

            while (left < right)
            {
                var temp = list[left];
                list[left] = list[right];
                list[right] = temp;

                left++;
                right--;
            }
        }
    }
    #endregion

    #region Q3: Filter Even Numbers from List
    public static class ListExtensions
    {
        public static List<int> GetEvenNumbers(this List<int> numbers)
        {
            return numbers.Where(num => num % 2 == 0).ToList();
        }
    }
    #endregion

    #region Q4: FixedSizeList<T>
    public class FixedSizeList<T>
    {
        private readonly T[] items;
        private int count;

        public int Capacity { get; }

        public FixedSizeList(int capacity)
        {
            if (capacity <= 0)
                throw new ArgumentException("Capacity must be greater than zero.");

            Capacity = capacity;
            items = new T[capacity];
            count = 0;
        }

        public void Add(T item)
        {
            if (count >= Capacity)
                throw new InvalidOperationException("List is full. Cannot add more items.");

            items[count++] = item;
        }

        public T Get(int index)
        {
            if (index < 0 || index >= count)
                throw new IndexOutOfRangeException("Invalid index.");

            return items[index];
        }
    }
    #endregion

    #region Q5: First Non-Repeated Character in String
    public static class StringExtensions
    {
        public static int FirstNonRepeatedCharacterIndex(this string input)
        {
            var charCount = new Dictionary<char, int>();

            foreach (var ch in input)
            {
                if (charCount.ContainsKey(ch))
                    charCount[ch]++;
                else
                    charCount[ch] = 1;
            }

            for (int i = 0; i < input.Length; i++)
            {
                if (charCount[input[i]] == 1)
                    return i;
            }

            return -1;
        }
    }
    #endregion

    class Program
    {
        static void Main(string[] args)
        {
            #region Test Q1: Range<T>
            Console.WriteLine("Testing Range<T>:");
            var intRange = new Range<int>(10, 20);
            Console.WriteLine($"10 is in range: {intRange.IsInRange(10)}");
            Console.WriteLine($"Length of range: {intRange.Length()}");

            var doubleRange = new Range<double>(5.5, 10.5);
            Console.WriteLine($"7.5 is in range: {doubleRange.IsInRange(7.5)}");
            Console.WriteLine($"Length of range: {doubleRange.Length()}");
            #endregion

            #region Test Q2: Reverse ArrayList In-Place
            Console.WriteLine("\nTesting Reverse ArrayList:");
            var arrayList = new ArrayList { 1, 2, 3, 4, 5 };
            Console.WriteLine("Original ArrayList: " + string.Join(", ", arrayList.ToArray()));
            arrayList.ReverseInPlace();
            Console.WriteLine("Reversed ArrayList: " + string.Join(", ", arrayList.ToArray()));
            #endregion

            #region Test Q3: Filter Even Numbers
            Console.WriteLine("\nTesting Even Numbers Filter:");
            var numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
            var evens = numbers.GetEvenNumbers();
            Console.WriteLine("Even Numbers: " + string.Join(", ", evens));
            #endregion

            #region Test Q4: FixedSizeList<T>
            Console.WriteLine("\nTesting FixedSizeList<T>:");
            var fixedList = new FixedSizeList<string>(3);
            fixedList.Add("A");
            fixedList.Add("B");
            Console.WriteLine($"Element at index 0: {fixedList.Get(0)}");

            try
            {
                fixedList.Add("C");
                fixedList.Add("D");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            #endregion

            #region Test Q5: First Non-Repeated Character
            Console.WriteLine("\nTesting First Non-Repeated Character:");
            var input = "swiss";
            int index = input.FirstNonRepeatedCharacterIndex();
            Console.WriteLine(index >= 0 ? $"First non-repeated character: {input[index]} at index {index}" : "No non-repeated character found.");
            #endregion
        }
    }
}
