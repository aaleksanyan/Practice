using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Threading;

namespace practice
{
    class Program
    {
        // static void Main(string[] args)
        // {
        //     // Testing QuickSort

        //     int[] unsorted = { 1, 11, 1, 0, -5, 6 };
        //     List<int> fart = new List<int>(unsorted);
        //     Console.WriteLine("Fart b4");
        //     foreach (int i in fart)
        //     {
        //         Console.WriteLine(i);
        //     }

        //     var sort = QuickSort(fart);

        //     Console.WriteLine("Fart now:");
        //     foreach (int i in sort)
        //     {
        //         Console.WriteLine(i);
        //     }

        //     // Testing MergeSortedArrs

        //     int[] merge1 = { 1, 2, 6, 8, 10 };
        //     List<int> shorter = new List<int>(merge1);

        //     int[] merge2 = { 0, 3, 3, 4, 9, 15, 16 };
        //     List<int> longer = new List<int>(merge2);

        //     var merged = MergeSortedArrs(shorter, longer);
        //     Console.WriteLine("Merged now:");
        //     foreach (int i in merged)
        //     {
        //         Console.WriteLine(i);
        //     }

        //     // Testing missingN

        //     int[] subsequent = { 1, 2, 3, 4, 6, 7, 8 };
        //     var missing = missingN(subsequent);
        //     Console.WriteLine("Missing number: ");
        //     Console.WriteLine(missing);

        //     // Testing MostOccurences

        //     int[] occurences = { 1, 1, 3, 4, 1, 2, 3, 4, 5, 3, 9, 2, 3, 4, 1, 2, 3, 10, 3 }; // Should be 3??
        //     var mode = mostRepetitive(occurences);
        //     Console.WriteLine("Most occurences: ");
        //     Console.WriteLine(mode);
        // }

        static void JustNumbers()
        {
            // print 5 numbers 
            // stop get user input Continue? Y/N
            // either continue or exit
            var cur = 0;
            const int interval = 5; // Putting it here so it doesn't re-allocate each run of loop
            do
            {
                for (var step = 0; step < interval; step++)
                {
                    Console.WriteLine($"Current number - {++cur}");
                }
                Console.WriteLine("Continue? Enter N to Stop:");
                var answer = Console.ReadLine();
                if (answer.ToUpper() == "N")
                {
                    Console.WriteLine("Exiting...");
                    break;
                }
            }
            while (true);
        }

        /*  
         *  Gets prime numbers "numbers" at a time using Sieve of Eratosthenes.
         */
        static void Primes(int number)
        {
            // Print 5 subsequent prime numbers. Prompt user to continue. If yes, print 5 more. If no, exit.

            // List of all prime numbers amassed so far. Add to it on every iteration.
            List<int> primes = new List<int>();
            do
            {
                for (var i = 0; i < number; i++)
                {
                    int cur = Sieve(primes);
                    // Set cur equal to the next prime based on the list of primes we have so far. Something like cur = sieve(list)
                    Console.WriteLine($"Current prime - {cur}");
                    primes.Add(cur);
                }
                Console.WriteLine("Continue? Enter N to Stop:");
                var answer = Console.ReadLine();
                if (answer.ToUpper() == "N")
                {
                    Console.WriteLine("Exiting...");
                    break;
                }

            }
            while (true);

        }

        static int Sieve(List<int> previous)
        {
            if (previous.Count < 1)
            {
                return 2;
            }
            if (previous.Count == 1)
            {
                return 3;
            }
            int nextPrime = previous[previous.Count - 1] + 2;

            // Now we need to find the next prime. If it is not divisible by all the numbers in the List, then it is prime.
            // If, for any number in the list, it is divisble, add 1 and start again.
            for (var i = 0; i < previous.Count; i++)
            {
                if (nextPrime % previous[i] == 0)
                {
                    nextPrime += 2;
                    i = 0;
                }
            }
            return nextPrime;
        }

        /* Get the Nth prime number.
         */
        static int NthPrime(int prev)
        {
            // Something like --
            // While the list is not len == N, run sieve and get the next number
            return 0;
        }


        /* Given an array, we set the following variables:
         * PivotPoint = last index
         * i = -1
         * j = 0 < lastIndex (iterate)
         * 
         * We have two base cases, if the array is len = 1, or len = 2
         */
        static List<int> QuickSort(List<int> input)
        {
            List<int> unsorted = new List<int>(input);
            if (unsorted.Count == 1 || unsorted.Count == 0)
            {
                return unsorted;
            }
            //if (unsorted.Count == 2)
            //{
            //    // Do something
            //    return unsorted;
            //}

            // Recursive case
            else
            {
                var i = -1;
                int pp = unsorted[unsorted.Count - 1]; // Last element
                for (var j = 0; j < (unsorted.Count - 1); j++)
                {
                    var next = unsorted[j];
                    if (next < pp)
                    {
                        i++;
                        if (j != i)
                        {
                            unsorted[j] = unsorted[i];
                            unsorted[i] = next;
                        }

                    }
                }
                // Move pp
                i++;
                unsorted[unsorted.Count - 1] = unsorted[i];
                unsorted[i] = pp;

                // Sort left and right arrs
                List<int> leftArr = QuickSort(unsorted.GetRange(0, i));
                List<int> rightArr = QuickSort(unsorted.GetRange(i + 1, unsorted.Count - 1 - i));
                leftArr.Add(pp);
                leftArr.AddRange(rightArr);
                return leftArr;
            }
        }

        /* Takes two arrays that have already been sorted and merges them together.
         * First one is necessarily shorter than second.
         */
        static List<int> MergeSortedArrs(List<int> first, List<int> second)
        {
            // Firstly, get the length of the smaller array. We will be iterating through it.
            // This in itself is a hiccup. How do we tell the code "this one is shorter, use that"
            // Well... a lazy starting way is to make it so that the user does inputs such that the first array is not longer than the second.
            // Therefore the first will always be shorter.

            List<int> merged = new List<int>();
            var i = 0;
            for (var j = 0; j < first.Count; j += 0)
            {
                if(i == second.Count - 1 || first[j] < second[i])
                {
                    merged.Add(first[j++]);
                }
                else
                {
                    merged.Add(second[i++]);
                }
            }

            while(second.Count - i > 1)
            {
                merged.Add(second[i++]);
            }

            return merged;

        }


        // Goes through an array 1-N and returns the missing number
        // O(N)
        static int missingN(int[] nums) 
        {
            var prev = 0;
            for(var i = 0; i < nums.Length; i++)
            {
                if(nums[i] != prev+1)
                {
                    return prev + 1;
                }
                prev++;
            }

            return -1;
        }

        // Goes through an array of numbers and returns the modal number
        static int mostRepetitive(int[] nums)
        {
            // I think what I will do here is create a dictionary of number (int) -> occurences (int)
            // Iterate through nums array one time to create dictionary, where every time you see a number, either create a new key or increment the value at that key.
            // Then go through the dictionary and return the key with the highest value.
            Dictionary<int, int> occurences = new Dictionary<int, int>();
            for (var i = 0; i < nums.Length; i++)
            {
                var num = nums[i];
                if (occurences.ContainsKey(num))
                {
                    // Increment
                    occurences[num] += 1;
                }
                else
                {
                    // Create key and set value to 1
                    occurences.Add(num, 1);
                }
            }

            // Go through the dictionary, find the key with the maximum value
            var max = double.NegativeInfinity;
            var mode = -1;

            foreach (var item in occurences)
            {
                if (item.Value > max)
                {
                    max = item.Value;
                    mode = item.Key;
                }
            }
            return mode;
        }

    }
}
