using Parallel_Bubble_Sorting.Algorithm;
using Parallel_Bubble_Sorting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParralelBubleSorting.WPF.Logic
{
    class BubbleSortAsync
    {
        private BubbleSort Sort { get; set; }
        public BubbleSortAsync()
        {
            Sort = new BubbleSort();
        }
        public async Task<SortedArrayModel> ParallelBubbleSortAsync(int[] arr, int numThreads = 8)
        {
            SortedArrayModel result = null;
            if (arr != null)
            {
                int length = arr.Length;
                result = await Task.Run(() =>
                {
                    int[] tempArr = new int[length];
                    Array.Copy(arr, tempArr, length);
                    var res = Sort.ParallelBubbleSortStart(tempArr, numThreads);
                    return res;
                });
            }
            return result;
        }
        public Task<SortedArrayModel> ParallelBubbleSortAsyncWithBlock(int[] arr, int numThreads = 8)
        {
            Task<SortedArrayModel> result = null;
            if (arr != null)
            {
                int length = arr.Length;
                result = Task.Run(() =>
                {
                    int[] tempArr = new int[length];
                    Array.Copy(arr, tempArr, length);
                    var res = Sort.ParallelBubbleSortStart(tempArr, numThreads);
                    return res;
                });
            }
            return result;
        }
    }
}
