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
using System.Windows;
using System.Windows.Threading;

namespace ParralelBubleSorting.WPF.Logic
{
    class SortAlgorithms
    {
        private int[] sortedMassive;
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
            _timer.Interval = TimeSpan.FromSeconds(0.2);
            _timer.Tick += OnTimerTick;
            _timer.Start();
        }
        public async Task<int[]> RunOneSortAlgorithm(int[] array)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            try
            {
                task1 = Task.Run(() => BubbleSort.ParallelBubbleSortStart(null, token), token);
                await Task.WhenAll(task1);
            }
            catch (ErrorSortException e)
            {
                MessageBox.Show(e.Message);
            }
            if (task1.IsFaulted)
            {
                return new int[1] { 0 };
            }
            return task1.Result;
        }
        public async Task<int[]> RunAllSortAlgorithm(int[] array)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            task1 = new Task<int[]>(() => BubbleSort.ParallelBubbleSortStart(null, token), token);
            task2 = new Task<int[]>(() => ChoiceSort.SelectionSort(null, token), token);
            task3 = new Task<int[]>(() => InsertionSort.InsertSort(null, token), token);
            var allTasks = Task.WhenAll(task1, task2, task3);
            task1.Start();
            task2.Start();
            task3.Start();
            try
            {
                await allTasks;
            }
            catch (ErrorSortException e)
            {
                string message = "";
                if (allTasks.Exception is not null)
                {
                    for (int i = 0; i < allTasks.Exception.InnerExceptions.Count; i++)
                    {
                        message += "Задача " + (i + 1) + ": " + ((ErrorSortException)allTasks.Exception.InnerExceptions[i]).Message + "\n";
                    }
                }
                MessageBox.Show(message);
            }
            if (task1.IsFaulted && task2.IsFaulted && task3.IsFaulted)
            {
                return new int[1] { 0 };
            }
            return task1.Result;
        }
        public async Task<int[]> RunAllSortWithContinue(int[] array)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            task1 = new Task<int[]>(() => BubbleSort.ParallelBubbleSortStart(null, token), token);

            var task1ContinueSuccess = task1.ContinueWith(_ => { sortedMassive = task1.Result; cts.Cancel(); MessageBox.Show("Успешно! Задача 1"); }, TaskContinuationOptions.OnlyOnRanToCompletion);
            var task1ContinueOnFault = task1.ContinueWith(_ => MessageBox.Show("Возникла ошибка!" + ((ErrorSortException)task1.Exception.InnerException).Message), 
                TaskContinuationOptions.OnlyOnFaulted);
            var task1ContinueCancel = task1.ContinueWith(_ => MessageBox.Show("Задача 1 отменена!"), TaskContinuationOptions.OnlyOnCanceled);

            task2 = new Task<int[]>(() => ChoiceSort.SelectionSort(array, token), token);

            var task2ContinueSuccess = task2.ContinueWith(_ => { sortedMassive = task2.Result; cts.Cancel(); MessageBox.Show("Успешно! Задача 2"); }, TaskContinuationOptions.OnlyOnRanToCompletion);
            var task2ContinueOnFault = task2.ContinueWith(_ => MessageBox.Show("Возникла ошибка!" + ((ErrorSortException)task2.Exception.InnerException).Message),
                TaskContinuationOptions.OnlyOnFaulted);
            var task2ContinueCancel = task2.ContinueWith(_ => MessageBox.Show("Задача 2 отменена!"), TaskContinuationOptions.OnlyOnCanceled);

            task3 = new Task<int[]>(() => InsertionSort.InsertSort(array, token), token);

            var task3ContinueSuccess = task3.ContinueWith(_ => { sortedMassive = task3.Result; cts.Cancel(); MessageBox.Show("Успешно! Задача 3"); }, TaskContinuationOptions.OnlyOnRanToCompletion) ;
            var task3ContinueOnFault = task3.ContinueWith(_ => MessageBox.Show("Возникла ошибка!" + ((ErrorSortException)task3.Exception.InnerException).Message),
                TaskContinuationOptions.OnlyOnFaulted);
            var task3ContinueCancel = task3.ContinueWith(_ => MessageBox.Show("Задача 3 отменена!"), TaskContinuationOptions.OnlyOnCanceled);

            var allTasks = Task.WhenAll(task1, task2, task3);
            task1.Start();
            task2.Start();
            task3.Start();
            try
            {
                await allTasks;
            }
            catch (ErrorSortException e)
            {
            }
            
            if (task1.IsFaulted && task2.IsFaulted && task3.IsFaulted)
            {
                return new int[1] { 0 };
            }
            return sortedMassive;
        }
        private void OnTimerTick(object sender, EventArgs e)
        {
            if(task1 != null || task2 != null || task3 != null)
                SortingProgress.UpdateStatus(task1, task2, task3);
        }
    }
}
