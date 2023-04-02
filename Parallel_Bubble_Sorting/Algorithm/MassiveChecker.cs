namespace Parallel_Bubble_Sorting.Algorithm;

public class MassiveChecker
{
    public bool CheckArrayForSorting(int[] array)
    {
        if (array == null) throw new ArgumentNullException(nameof(array));
        if (array.Length == 0) throw new ArgumentException("Массив не может быть пустой коллекцией.", nameof(array));
        for (var i = 0; i < array.Length - 1; i++)
        {
            if (array[i] <= array[i + 1]) continue;
            return false;
        }

        return true;
    }
}