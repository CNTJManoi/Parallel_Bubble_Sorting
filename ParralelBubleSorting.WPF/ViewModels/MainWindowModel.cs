using ParralelBubleSorting.WPF.Command;
using ParralelBubleSorting.WPF.Logic;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ParralelBubleSorting.WPF.ViewModels
{
    internal class MainWindowModel : ViewModelBase
    {
        private int[] _massiveNotSort;
        private int[] _massiveSort;
        private DelegateCommand _startSort;
        private DelegateCommand _startSortBlocking;
        private DelegateCommand _startGenerateMassive;
        private BubbleSortAsync Sort { get; set; }
        public string NotSortMassive { get; set; }
        public string SortMassive { get; set; }
        public bool IsWorking { get; set; }
        
        public DelegateCommand StartSortCommand => _startSort ??
                                              (_startSort = new DelegateCommand(StartSort));
        public DelegateCommand StartSortWithBlockCommand => _startSortBlocking ??
                                              (_startSortBlocking = new DelegateCommand(StartSortWithBlocking));
        public DelegateCommand GenerateMassiveCommand => _startGenerateMassive ??
                                              (_startGenerateMassive = new DelegateCommand(GenerateInitialMassive));
        public MainWindowModel()
        {
            _massiveNotSort = null;
            _massiveSort = null;
            NotSortMassive = "";
            SortMassive = "";
            IsWorking = false;
            Sort = new BubbleSortAsync();
        }
        private async void StartSort(object obj)
        {
            if(_massiveNotSort == null || _massiveNotSort.Length == 0)
            {
                PrintError("не сгенерирован массив!");
            }
            else
            {
                StartShowProgress();
                var result = await Sort.ParallelBubbleSortAsync(_massiveNotSort);
                _massiveSort = result.SortedArray;
                UpdateStringSort();
                StopShowProgress();
            }
        }
        private void StartSortWithBlocking(object obj)
        {
            if (_massiveNotSort == null || _massiveNotSort.Length == 0)
            {
                PrintError("не сгенерирован массив!");
            }
            else
            {
                StartShowProgress();
                var result = Sort.ParallelBubbleSortAsyncWithBlock(_massiveNotSort);
                _massiveSort = result.Result.SortedArray;
                UpdateStringSort();
                StopShowProgress();
            }
        }
        private void GenerateInitialMassive(object obj)
        {
            MassiveGenerator generator = new MassiveGenerator();
            var massive = generator.GenerateRandomArray(100000);
            _massiveNotSort = massive;
            UpdateStringNotSort();
        }
        private void UpdateStringNotSort()
        {
            NotSortMassive = string.Join(", ", _massiveNotSort);
            OnPropertyChanged(nameof(NotSortMassive));
        }
        private void UpdateStringSort()
        {
            SortMassive = string.Join(", ", _massiveSort);
            OnPropertyChanged(nameof(SortMassive));
        }
        private void PrintError(string error)
        {
            MessageBox.Show("Произошла ошибка: " + error);
        }
        private void StartShowProgress()
        {
            IsWorking = true;
            OnPropertyChanged(nameof(IsWorking));
        }
        private void StopShowProgress()
        {
            IsWorking = false;
            OnPropertyChanged(nameof(IsWorking));
        }
    }
}
