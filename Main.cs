using System;
class Sorts
{
    delegate void Sort(int[] nums);

    static void Main(string[] args)
    {
        Sort[] sorts = { Selection, Merge, Insertion, Quick, Heap };
        Random r = new Random();
        foreach (Sort sort in sorts)
        {
            //Generate numbers to sort
            int[] nums = genRandomInts(20, r);

            //Display sort intro
            System.Console.WriteLine(new String('=', 110));
            System.Console.Write(sort.Method.Name + " Sort: ");
            System.Console.Write("\nUnsorted: ");
            print_array(nums);

            //Show that sort has sorted as expected
            sort(nums);
            System.Console.Write("\nSorted: ");
            print_array(nums);
            System.Console.WriteLine("\n");
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

    static void print_array(int[] nums)
    {
        System.Console.Write("[");
        for (int i = 0; i < nums.Length; i++)
        {
            System.Console.Write(nums[i]);
            if (i != nums.Length - 1)
            {
                System.Console.Write(", ");
            }

        }
        System.Console.Write("]");
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