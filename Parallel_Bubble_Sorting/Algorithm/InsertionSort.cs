using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parallel_Bubble_Sorting.Algorithm
{
    public class InsertionSort
    {
        public int[] InsertionSortParallel(int[] array, CancellationToken token)
        {
            int length = array.Length;
            int partitionCount = Environment.ProcessorCount;
            int partitionSize = length / partitionCount;

            Parallel.For(0, partitionCount, partitionIndex =>
            {
                int startIndex = partitionIndex * partitionSize;
                int endIndex = (partitionIndex == partitionCount - 1) ? length : (partitionIndex + 1) * partitionSize;

                for (int i = startIndex + 1; i < endIndex; i++)
                {
                    if (token.IsCancellationRequested)
                        token.ThrowIfCancellationRequested();
                    int key = array[i];
                    int j = i - 1;

                    while (j >= startIndex && array[j] > key)
                    {
                        array[j + 1] = array[j];
                        j--;
                    }
                    array[j + 1] = key;
                }
            });

            for (int i = 1; i < length; i++)
            {
                if (token.IsCancellationRequested)
                    token.ThrowIfCancellationRequested();
                int key = array[i];
                int j = i - 1;

                while (j >= 0 && array[j] > key)
                {
                    array[j + 1] = array[j];
                    j--;
                }
                array[j + 1] = key;
            }
            return array;
        }
    }
}
