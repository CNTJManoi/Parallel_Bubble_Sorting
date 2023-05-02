using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parallel_Bubble_Sorting.Algorithm
{
    public class InsertionSort
    {
        public int[] InsertSort(int[] array, CancellationToken token)
        {
            for (int i = 1; i < array.Length; i++)
            {
                if (token.IsCancellationRequested)
                    token.ThrowIfCancellationRequested();
                var key = array[i];
                var flag = 0;

                for (int j = i - 1; j >= 0 && flag != 1;)
                {
                    if (key < array[j])
                    {
                        array[j + 1] = array[j];
                        j--;
                        array[j + 1] = key;
                    }
                    else flag = 1;
                }
            }

            return array;
        }
    }
}
