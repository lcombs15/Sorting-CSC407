using System;
using System.Collections;

class Sorts
{
    delegate void Sort(int[] nums);

    static void Main(string[] args)
    {
        //Sorts to test
        Sort[] sorts = { Selection, Bubble, Insertion, Quick, Heap };

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
            tests.Add(Tuple.Create(genRandomInts(1, r), "array of length one"));
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
                System.Console.Write("\t\t");
                System.Console.Write(IsSorted(test.Item1) ? "PASSED!\n" : "FAILED!!!\n");
            }
        }

        //Hold console open
        while (true) ;
    }

    private static void Heap(int[] nums)
    {
        // Build heap (rearrange array)
        for (int i = nums.Length / 2 - 1; i >= 0; i--)
        {
            heapify(nums, nums.Length, i);
        }

        // One by one extract an element from heap
        for (int i = nums.Length - 1; i >= 0; i--)
        {
            // Move current root to end
            swap(nums, 0, i);

            // call max heapify on the reduced heap
            heapify(nums, i, 0);
        }
    }

    // To heapify a subtree rooted with node i which is
    // an index in arr[]. n is size of heap
    private static void heapify(int[] nums, int length, int i)
    {
        int largest = i;
        int left = 2 * i + 1;
        int right = 2 * i + 2;

        //check if left is larger than current largest
        if (left < length && nums[left] > nums[largest])
        {
            //sets largest to value of left
            largest = left;
        }
        //check if right is larger than current largest
        if (right < length && nums[right] > nums[largest])
        {
            //sets largest to value of right
            largest = right;
        }
        //check if largest is not equal to i
        if (largest != i)
        {
            //swap i and largest in the nums array
            swap(nums, i, largest);
            //recursive call to heapify using the new largest 
            heapify(nums, length, largest);
        }
    }

    private static void Quick(int[] nums)
    {
        Quick(nums, 0, nums.Length - 1);
    }

    /*
     * Sorts int array using quicksort.
     * Low and high the respective extremes for valid array indices.
     */
    private static void Quick(int[] nums, int low, int high)
    {
        if (low < high)
        {
            int p = Partition(nums, low, high);
            Quick(nums, low, p - 1);
            Quick(nums, p + 1, high);
        }

    }

    /*
     *  Return index P in nums[] such that everything to the left of P
     *  is less that or equal to P, and everything to the right is greater than P.
     *      
     *      Low and high are assumed to be valid indexes
     *      IE: high could be nums.length()-1 but not nums.length()
     */
    private static int Partition(int[] nums, int low, int high)
    {
        // pivot (Element to be placed at right position)
        int pivot = nums[high];

        //Highest index before pivot
        int i = low - 1;

        for (int j = low; j < high; j++)
        {
            //Need to move everything <= pivot
            if (nums[j] <= pivot)
            {
                //Index before pivot moves up
                i++;

                //Swap new element <= pivot into position
                swap(nums, i, j);
            }
        }

        //Swap pivot into rightful place
        swap(nums, i + 1, high);

        //Return final pivot position
        return i + 1;
    }

    //Swap X and Y in nums array
    public static void swap(int[] nums, int x, int y)
    {
        int temp = nums[x];
        nums[x] = nums[y];
        nums[y] = temp;
    }

    /*
     *  This gets a little complicated
     *  becuase of the void nature of this method.
     *  
     *  int[] nums is sorting in increasing order upon termination
     */
    private static void Insertion(int[] nums)
    {
        /*
         * Make a copy of the given nums so that original nums
         * array can function as the "new" list insertion sort depends on
         */
        int[] copy = new int[nums.Length];
        nums.CopyTo(copy, 0);

        //Loop through all elements
        for (int i = 0; i < copy.Length; i++)
        {
            //Determine where the given element needs to go
            int j = 0;
            while (copy[i] >= nums[j] && j < i)
            {
                j++;
            }

            /* 
             * Move other elements in the array up & out of the way
             * to make room for new element
             */
            for (int k = i; k > j; k--)
            {
                nums[k] = nums[k - 1];
            }

            //Place new element into sorted position
            nums[j] = copy[i];
        }
    }

    private static void Bubble(int[] nums)
    {
        for (int i = 0; i <= nums.Length - 2; i++)
        {
            for (int j = 0; j <= nums.Length - 2; j++)
            {
                if (nums[j] > nums[j + 1])
                {
                    swap(nums, j, j + 1);
                }
            }
        }
    }


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
            swap(nums, min, i);
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

    //Returns true if nums are sorted in increasing order
    static bool IsSorted(int[] nums)
    {
        //Any array with 1 or less elements is sorted
        if (nums.Length <= 1)
            return true;

        int prev = nums[0];
        for (int i = 1; i < nums.Length; i++)
        {
            if (nums[i] < prev)
            {
                return false;
            }
            prev = nums[i];
        }

        return true;
    }
}
