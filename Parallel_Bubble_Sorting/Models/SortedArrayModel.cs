namespace Parallel_Bubble_Sorting.Models;

public class SortedArrayModel
{
    public SortedArrayModel(TimeSpan elapsedTime, int[] sortedArray)
    {
        if (sortedArray.Length == 0)
            throw new ArgumentException("Массив не может быть пустой коллекцией.", nameof(sortedArray));
        ElapsedTime = elapsedTime;
        SortedArray = sortedArray ?? throw new ArgumentNullException(nameof(sortedArray));
    }

    public TimeSpan ElapsedTime { get; set; }
    public int[] SortedArray { get; set; }
}