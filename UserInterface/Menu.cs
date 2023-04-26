using FileUtils;
using Parallel_Bubble_Sorting;
using Parallel_Bubble_Sorting.Algorithm;
using Parallel_Bubble_Sorting.Models;
using Repository;

namespace UserInterface;

internal class Menu
{
    public Menu()
    {
        Sort = new BubbleSort();
        Generator = new MassiveGenerator();
        Checker = new MassiveChecker();
        Informant = new ServiceInformant();
        Writer = new FileWriter();
    }

    private ServiceInformant Informant { get; }
    private BubbleSort Sort { get; }
    private MassiveChecker Checker { get; }
    private MassiveGenerator Generator { get; }
    private FileWriter Writer { get; }

    public void Start()
    {
        var array = Generator.GenerateRandomArray(InputInformationAboutMassiveSize());
        var copyArray = new int[array.Length];
        copyArray = array.Select(x => x).ToArray();
        var numberThreads = InputInformationAboutThreads();
        //Обычная сортировка
        Informant.PrintWaitingMessage();
        StartNormalSorting(array);
        //Параллельная сортировка
        Informant.PrintWaitingMessage();
        //StartParallelSorting(copyArray, numberThreads);
        Console.ReadKey();
    }

    private int InputInformationAboutThreads()
    {
        Informant.PrintRequestInputThreads();
        var number = -1;
        var notCorrect = true;
        while (notCorrect)
            try
            {
                number = int.Parse(Console.ReadLine());
                notCorrect = false;
            }
            catch (FormatException e)
            {
                Console.WriteLine("Введите корректную строку! Ошибка: " + e.Message);
            }

        return number;
    }

    private int InputInformationAboutMassiveSize()
    {
        Informant.PrintRequestInputSize();
        var number = -1;
        var notCorrect = true;
        while (notCorrect)
            try
            {
                number = int.Parse(Console.ReadLine());
                notCorrect = false;
            }
            catch (FormatException e)
            {
                Console.WriteLine("Введите корректную строку! Ошибка: " + e.Message);
            }

        return number;
    }

    private void StartNormalSorting(int[] array)
    {
        Writer.WriteArrayInFile(array, "NotSortNormal.txt");
        var resultNormal = Sort.NormalBubbleSortStart(array);
        Writer.WriteArrayInFile(array, "SortNormal.txt");
        var statusNormal = Checker.CheckArrayForSorting(resultNormal.SortedArray);
        Informant.PrintResultAlgorithm(resultNormal, TypeSorting.Normal, statusNormal);
    }

    //private void StartParallelSorting(int[] array, int numberThreads)
    //{
    //    Writer.WriteArrayInFile(array, "NotSortParallel.txt");
    //    var resultParallel = Sort.ParallelBubbleSortStart(array, numberThreads);
    //    Writer.WriteArrayInFile(array, "SortParallel.txt");
    //    var statusParallel = Checker.CheckArrayForSorting(resultParallel.SortedArray);
    //    Informant.PrintResultAlgorithm(resultParallel, TypeSorting.Parallel, statusParallel);
    //}
}