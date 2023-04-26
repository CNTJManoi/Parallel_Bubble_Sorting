using Parallel_Bubble_Sorting.Algorithm;
using Parallel_Bubble_Sorting.Models;
using ParralelBubleSorting.WPF.Models;
using ParralelBubleSorting.WPF.Models.Status;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ParralelBubleSorting.WPF.Logic
{
    class SortAlgorithms
    {
        private DispatcherTimer _timer;
        private BubbleSort BubbleSort { get; set; }
        private ChoiceSort ChoiceSort { get; set; }
        private InsertionSort InsertionSort { get; set; }
        public ISortingProgress SortingProgress { get; set; }
        private Task<int[]> task1;
        private Task<int[]> task2;
        private Task<int[]> task3;
        public SortAlgorithms(ISortingProgress ProgressTasks)
        {
            BubbleSort = new BubbleSort();
            ChoiceSort = new ChoiceSort();
            InsertionSort = new InsertionSort();
            SortingProgress = ProgressTasks;
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += OnTimerTick;
            _timer.Start();
        }
        public async Task<int[]> RunSortingAlgorithms(int[] array, TypeSort type)
        {
            var copyArray = new int[array.Length];
            copyArray = array.Select(x => x).ToArray();
            Task<int[]> completedTask = null;
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            int[] sortedArray;
            
            task1 = Task.Run(() => BubbleSort.ParallelBubbleSortStart(copyArray, token), token);
            task2 = Task.Run(() => ChoiceSort.SelectionSort(copyArray, token), token);
            task3 = Task.Run(() => InsertionSort.InsertionSortParallel(copyArray, token), token);
            SortingProgress.UpdateStatus(task1, task2, task3);

            if(type == TypeSort.SortWithOneWait) completedTask = await Task.WhenAny(task1, task2, task3);
            else await Task.WhenAll(task1, task2, task3);

            if(type == TypeSort.SortWithOneWait)
            {
                cts.Cancel();
                sortedArray = completedTask.Result;
                Thread.Sleep(100);
            }
            else
            {
                sortedArray = task1.Result;
            }
            SortingProgress.UpdateStatus(task1, task2, task3);
            return sortedArray;
        }
        public Task<int[]> RunSortingAlgorithmsWithBlock(int[] array, TypeSort type)
        {
            var copyArray = new int[array.Length];
            copyArray = array.Select(x => x).ToArray();
            int result = -1;
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            Task<int[]> sortedArray = null;

            task1 = Task.Run(() => BubbleSort.ParallelBubbleSortStart(copyArray, token), token);
            task2 = Task.Run(() => ChoiceSort.SelectionSort(copyArray, token), token);
            task3 = Task.Run(() => InsertionSort.InsertionSortParallel(copyArray, token), token);
            SortingProgress.UpdateStatus(task1, task2, task3);

            if (type == TypeSort.SortWithOneWait) result = Task.WaitAny(task1, task2, task3);
            else Task.WaitAll(task1, task2, task3);

            if (type == TypeSort.SortWithOneWait)
            {
                cts.Cancel();
                switch (result)
                {
                    case 0:
                        sortedArray = task1;
                        break;
                    case 1:
                        sortedArray = task2;
                        break;
                    case 2:
                        sortedArray = task3;
                        break;
                }
                Thread.Sleep(100);
            }
            else
            {
                sortedArray = task1;
            }
            SortingProgress.UpdateStatus(task1, task2, task3);
            return sortedArray;
        }
        private void OnTimerTick(object sender, EventArgs e)
        {
            if(task1 != null || task2 != null || task3 != null)
                SortingProgress.UpdateStatus(task1, task2, task3);
        }
    }
}
