using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parallel_Bubble_Sorting.Algorithm
{
    internal class SelectionSort
    {
        public int[] ParalSort(int[] mas)
        {
            int indx;
            object lockObject = new object();
            for (int i = 0; i < mas.Length; i++)
            {
                indx = i;
                Parallel.For(i, mas.Length, j =>
                {
                    if (mas[j] < mas[indx])
                    {
                        lock (lockObject)
                        {
                            indx = j;
                        }

                    }
                });
                if (mas[indx] == mas[i])
                    continue;
                int temp = mas[i];
                mas[i] = mas[indx];
                mas[indx] = temp;
            }

            return mas;
        }
    }
}
