namespace Repository;

public class MassiveGenerator
{
    public int[] GenerateRandomArray(int n)
    {
        if (n <= 0) throw new ArgumentOutOfRangeException(nameof(n));
        var arr = new int[n];
        Random rand = new();
        for (var i = 0; i < n; i++) arr[i] = rand.Next(100000);

        return arr;
    }
}