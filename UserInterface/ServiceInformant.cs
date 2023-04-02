using Parallel_Bubble_Sorting;
using Parallel_Bubble_Sorting.Models;

namespace UserInterface;

internal class ServiceInformant
{
    public void PrintRequestInputThreads()
    {
        Console.Write("Введите количество потоков: ");
    }

    public void PrintRequestInputSize()
    {
        Console.Write("Введите размер массива: ");
    }

    public void PrintWaitingMessage()
    {
        ColorToRed();
        Console.Write("Ожидание выполнения алгоритма...");
        ColorToWhite();
    }

    public void PrintResultAlgorithm(SortedArrayModel info, TypeSorting type, bool status)
    {
        var line = 0;
        if (type == TypeSorting.Normal) line = 2;
        else line = 3;
        var typeString = type == TypeSorting.Normal ? "Нормальная" : "Параллельная";
        ClearLine();
        var statusString = status ? "Успешно отсортирован" : "Неудачная сортировка. Обнаружена ошибка.";
        Console.WriteLine(
            $"{typeString} сортировка: время - {info.ElapsedTime.Seconds}.{info.ElapsedTime.Milliseconds}" +
            $" Статус: {statusString}");
    }

    private void ClearLine()
    {
        Console.SetCursorPosition(0, Console.CursorTop);
        Console.Write("\r" + new string(' ', Console.BufferWidth) + "\r");
    }

    private void ColorToRed()
    {
        Console.ForegroundColor = ConsoleColor.Red;
    }

    private void ColorToWhite()
    {
        Console.ForegroundColor = ConsoleColor.White;
    }
}