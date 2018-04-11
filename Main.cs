using System;
using System.Collections;

class Sorts
{
    delegate void Sort(int[] nums);

    static void Main(string[] args)
    {
        //Sorts to test
        Sort[] sorts = { Selection, Merge, Insertion, Quick, Heap };

        //Tests case location for each sort
        ArrayList tests = new ArrayList();

        //Seed Random
        Random r = new Random();

        foreach (Sort sort in sorts)
        {
            //Display sort intro
            System.Console.WriteLine(new String('=', 125));
            System.Console.WriteLine(sort.Method.Name + " Sort: ");

            //Setup test cases
            tests.Clear(); //Clean up from previous tests
            tests.Add(Tuple.Create(new int[] { 5 }, "array of length one"));
            tests.Add(Tuple.Create(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, "already sorted array"));
            tests.Add(Tuple.Create(new int[] { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 }, "array in descending order"));
            tests.Add(Tuple.Create(genRandomInts(20, r), "unsorted Array"));

            //Indent and run each test case
            foreach (Tuple<int[], string> test in tests)
            {
                //Test heading
                System.Console.WriteLine("\tTesting: " + test.Item2);

                //Show before and after sort
                System.Console.WriteLine("\t\tUnsorted: " + ArrayString(test.Item1));
                sort(test.Item1);
                System.Console.WriteLine("\t\tSorted:   " + ArrayString(test.Item1));
            }
        }

        //Hold console open
        while (true) ;
    }

    private static void Heap(int[] nums) { }

    private static void Quick(int[] nums) { }

    private static void Insertion(int[] nums) { }

    private static void Merge(int[] nums) { }

    //Given array of ints, array is sorted on termination.
    static void Selection(int[] nums)
    {
        for (int i = 0; i < nums.Length; i++)
        {
            int min = i;
            for (int j = i + 1; j < nums.Length; j++)
            {
                if (nums[j] < nums[min])
                {
                    min = j;
                }
            }
            int temp = nums[i];
            nums[i] = nums[min];
            nums[min] = temp;
        }
    }

    static string ArrayString(int[] nums)
    {
        string retVal = "[";

        for (int i = 0; i < nums.Length; i++)
        {
            retVal += nums[i];
            if (i != nums.Length - 1)
            {
                retVal += ", ";
            }

        }

        return retVal + "]";
    }

    /* Given int, returns random int array X such that:
     * A given number n where n is in X,
     * -150 >= n >= 150
     */
    static int[] genRandomInts(int n, Random r)
    {
        int[] retVal = new int[n];
        for (int i = 0; i < n; i++)
        {
            int parity = r.Next() % 2;
            parity = parity == 0 ? -1 : 1;

            retVal[i] = (r.Next() % 150) * parity;
        }
        return retVal;
    }
}