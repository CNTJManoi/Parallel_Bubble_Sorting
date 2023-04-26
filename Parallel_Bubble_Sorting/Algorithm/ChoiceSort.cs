using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parallel_Bubble_Sorting.Algorithm
{
    public class ChoiceSort
    {
        public int[] SelectionSort(int[] arr, CancellationToken token)
        {
            int n = arr.Length;
            for (int i = 0; i < arr.Length; i++)
            {
                if (token.IsCancellationRequested)
                    token.ThrowIfCancellationRequested();
                int minIndex = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (arr[j] < arr[minIndex])
                    {
                        minIndex = j;
                    }
                }
                if (minIndex != i)
                {
                    int temp = arr[i];
                    arr[i] = arr[minIndex];
                    arr[minIndex] = temp;
                }
            }
            return arr;
        }
    }
}
