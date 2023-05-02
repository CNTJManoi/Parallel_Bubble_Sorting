using System.Diagnostics;
using Parallel_Bubble_Sorting.Models;

namespace Parallel_Bubble_Sorting.Algorithm;

public class BubbleSort
{
    public BubbleSort()
    {
        Checker = new MassiveChecker();
    }
    private Stopwatch Stopwatch { get; set; }
    private MassiveChecker Checker { get; set; }

    public SortedArrayModel NormalBubbleSortStart(int[] arr)
    {
        if (arr == null) throw new ArgumentNullException(nameof(arr));
        if (arr.Length == 0) throw new ArgumentException("Массив не может быть пустой.", nameof(arr));
        Stopwatch = Stopwatch.StartNew();
        var resultMassive = NormalBubbleSortCycle(arr);
        Stopwatch.Stop();
        return new SortedArrayModel(Stopwatch.Elapsed, resultMassive);
    }

    private int[] NormalBubbleSortCycle(int[] arr)
    {
        var swapped = true;
        var end = arr.Length;
        while (swapped)
        {
            swapped = false;

            for (var i = 0; i < end - 1; i++)
                if (arr[i] > arr[i + 1])
                {
                    (arr[i], arr[i + 1]) = (arr[i + 1], arr[i]);
                    swapped = true;
                }

            end--;
        }

        return arr;
    }

    public int[] ParallelBubbleSortStart(int[] array, CancellationToken token)
    {
        Stopwatch = Stopwatch.StartNew();
        var resultMassive = ParallelBubbleSort(array, 8, token);
        Stopwatch.Stop();
        return resultMassive;
    }

    private int[] ParallelBubbleSort(int[] arr, int numThreads, CancellationToken token)
    {
        if (arr == null) throw new ErrorSortException("Произошла ошибка в алгоритме сортировки!");
        for (int i = 0; !Checker.CheckArrayForSorting(arr); i++)
        {
            token.ThrowIfCancellationRequested();
            
            if (i % 2 == 0)
            {

                Parallel.For(0, numThreads, j =>
                {
                    int start = j * (arr.Length / numThreads);
                    int end = (j + 1) * (arr.Length / numThreads);
                    if (j == numThreads - 1) end = arr.Length;

                    for (int k = start; k < end - 1; k++)
                    {
                        if (arr[k] > arr[k + 1])
                        {
                            int temp = arr[k];
                            arr[k] = arr[k + 1];
                            arr[k + 1] = temp;
                        }
                    }
                });
            }
            else
            {
                Parallel.For(0, numThreads, j =>
                {
                    int start = j * (arr.Length / numThreads) + 1;
                    int end = (j + 1) * (arr.Length / numThreads) + 1;
                    if (j == numThreads - 1) end = arr.Length;

                    for (int k = end - 1; k > start - 1; k--)
                    {
                        if (arr[k] < arr[k - 1])
                        {
                            int temp = arr[k];
                            arr[k] = arr[k - 1];
                            arr[k - 1] = temp;
                        }
                    }
                });
            }
        }
        return arr;
    }

    public int[] ParallelSortWithMistake(int[] arr)
    {
        Parallel.For(1, arr.Length, i =>
        {
            for (var j = 0; j < arr.Length - i; j++)
            {
                if (arr[j] > arr[j + 1])
                {
                    (arr[j], arr[j + 1]) = (arr[j + 1], arr[j]);
                }
            }

        });
        return arr;
    }
}