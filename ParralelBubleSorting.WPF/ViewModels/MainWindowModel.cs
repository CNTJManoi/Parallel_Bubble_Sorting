using ParralelBubleSorting.WPF.Command;
using ParralelBubleSorting.WPF.Logic;
using ParralelBubleSorting.WPF.Models.Status;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace ParralelBubleSorting.WPF.ViewModels
{
    internal class MainWindowModel : ViewModelBase
    {
        #region Fields
        private int[] _massiveNotSort;
        private int[] _massiveSort;
        private DispatcherTimer _timer;
        private DelegateCommand _startSortWithResultOne;
        private DelegateCommand _startSortBlockingWithResultOne;
        private DelegateCommand _startSortWithResultAll;
        private DelegateCommand _startSortBlockingWithResultAll;
        private DelegateCommand _startGenerateMassiveCommand;
        private DelegateCommand _startClearCommand;
        private DelegateCommand _clearCommand;
        #endregion

        #region Properties
        private SortAlgorithms Sort { get; set; }
        public string NotSortMassive { get; set; }
        public string SortMassive { get; set; }
        public bool IsWorking { get; set; }
        public string InputCountMassive { get; set; }
        public string StatusOneTask { 
            get 
            { 
                return IProgress.StatusOneTask; 
            } 
            set 
            { 
                IProgress.StatusOneTask = value;
                OnPropertyChanged(nameof(StatusOneTask));
            } 
        }
        public string StatusTwoTask
        {
            get
            {
                return IProgress.StatusTwoTask;
            }
            set
            {
                IProgress.StatusTwoTask = value;
                OnPropertyChanged(nameof(StatusTwoTask));
            }
        }
        public string StatusThreeTask
        {
            get
            {
                return IProgress.StatusThreeTask;
            }
            set
            {
                IProgress.StatusThreeTask = value;
                OnPropertyChanged(nameof(StatusThreeTask));
            }
        }
        public ISortingProgress IProgress;
        #endregion

        #region Commands

        public DelegateCommand StartSortWithOneResultCommand => _startSortWithResultOne ??
                                              (_startSortWithResultOne = new DelegateCommand(StartSortWithOneResult));
        public DelegateCommand StartSortWithBlockOneResultCommand => _startSortBlockingWithResultOne ??
                                              (_startSortBlockingWithResultOne = new DelegateCommand(StartSortWithBlockingOneResult));
        public DelegateCommand StartSortWithAllResultCommand => _startSortWithResultAll ??
                                              (_startSortWithResultAll = new DelegateCommand(StartSortWithAllResult));
        public DelegateCommand GenerateMassiveCommand => _startGenerateMassiveCommand ??
                                              (_startGenerateMassiveCommand = new DelegateCommand(GenerateInitialMassive));
        public DelegateCommand ClearCommand => _clearCommand ??
                                              (_clearCommand = new DelegateCommand(Clear));
        #endregion

        public MainWindowModel()
        {
            _massiveNotSort = null;
            _massiveSort = null;
            NotSortMassive = "";
            SortMassive = "";
            IsWorking = false;
            IProgress = new SortingProgress();
            Sort = new SortAlgorithms(IProgress);
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += OnTimerTick;
            _timer.Start();
        }

        //Параллельно ожидая пока закончится какой-то один из трех
        private async void StartSortWithOneResult(object obj)
        {
            if(_massiveNotSort == null || _massiveNotSort.Length == 0)
            {
                PrintError("не сгенерирован массив!");
            }
            else
            {
                StartShowProgress();
                var result = await Sort.RunOneSortAlgorithm(_massiveNotSort);
                if(result.Length > 1 && result[0] != 0)_massiveSort = result;
                UpdateStringSort();
                StopShowProgress();
            }
        }

        //Также как прошлый, но с блокировкой интерфейса
        private async void StartSortWithBlockingOneResult(object obj)
        {
            if (_massiveNotSort == null || _massiveNotSort.Length == 0)
            {
                PrintError("не сгенерирован массив!");
            }
            else
            {
                StartShowProgress();
                var result = await Sort.RunAllSortAlgorithm(_massiveNotSort);
                if (result.Length > 1 && result[0] != 0) _massiveSort = result;
                UpdateStringSort();
                StopShowProgress();
            }
        }

        //Параллельно ожидая пока закончится все три
        private async void StartSortWithAllResult(object obj)
        {
            if (_massiveNotSort == null || _massiveNotSort.Length == 0)
            {
                PrintError("не сгенерирован массив!");
            }
            else
            {
                StartShowProgress();
                var result = await Sort.RunAllSortWithContinue(_massiveNotSort);
                _massiveSort = result;
                UpdateStringSort();
                StopShowProgress();
            }
        }

        //Также как прошлый, но с блокировкой интерфейса
        //private void StartSortWithBlockingAllResult(object obj)
        //{
        //    if (_massiveNotSort == null || _massiveNotSort.Length == 0)
        //    {
        //        PrintError("не сгенерирован массив!");
        //    }
        //    else
        //    {
        //        StartShowProgress();
        //        var result = Sort.RunSortingAlgorithmsWithBlock(_massiveNotSort, Models.TypeSort.SortWithAllWait);
        //        _massiveSort = result.Result;
        //        UpdateStringSort();
        //        StopShowProgress();
        //    }
        //}

        private void GenerateInitialMassive(object obj)
        {
            MassiveGenerator generator = new MassiveGenerator();

            int count = GetInputInt();
            if (count != -1)
            {
                var massive = generator.GenerateRandomArray(count);
                _massiveNotSort = massive;
                UpdateStringNotSort();
            }
        }

        private void UpdateStringNotSort()
        {
            NotSortMassive = string.Join(", ", _massiveNotSort);
            OnPropertyChanged(nameof(NotSortMassive));
        }
        private int GetInputInt()
        {
            try
            {
                int inputNumber = int.Parse(InputCountMassive);
                return inputNumber;
            }
            catch (ArgumentNullException e)
            {
                MessageBox.Show("Вы не ввели число!\n" + e.Message);
                return -1;
            }
            catch (FormatException e)
            {
                MessageBox.Show("Вы ввели неверное число!\n"+ e.Message);
                return -1;
            }
        }
        private void UpdateStringSort()
        {
            if(_massiveSort != null) SortMassive = string.Join(", ", _massiveSort);
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
        private void UpdateStatusStrings()
        {
            OnPropertyChanged(nameof(StatusOneTask));
            OnPropertyChanged(nameof(StatusTwoTask));
            OnPropertyChanged(nameof(StatusThreeTask));
        }
        private void OnTimerTick(object sender, EventArgs e)
        {
            UpdateStatusStrings();
        }
        private void Clear(object obj)
        {
            SortMassive = "";
            NotSortMassive = "";
            InputCountMassive = "";
            _massiveNotSort = null;
            _massiveSort = null;
            OnPropertyChanged(nameof(SortMassive));
            OnPropertyChanged(nameof(NotSortMassive));
            OnPropertyChanged(nameof(InputCountMassive));
        }
    }
}
