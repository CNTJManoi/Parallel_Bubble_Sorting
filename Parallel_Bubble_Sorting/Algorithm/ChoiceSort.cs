using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parallel_Bubble_Sorting.Algorithm
{
    internal class ChoiceSort
    {
        public int[] ParallelSelectionSortNoError(int[] arr)
        {
            int indx;
            object lockObject = new object();
            for (int i = 0; i < arr.Length; i++)
            {
                indx = i;
                Parallel.For(i, arr.Length, j =>
                {
                    lock (lockObject)
                    {
                        if (arr[j] < arr[indx])
                        {

                            indx = j;

                        }
                    }
                });
                if (arr[indx] != arr[i])
                {
                    int temp = arr[i];
                    arr[i] = arr[indx];
                    arr[indx] = temp;
                }
            }

            return arr;
        }
    }
}
