namespace FileUtils;

public class FileWriter
{
    public FileWriter(string path = "D:\\")
    {
        Path = path;
    }

    private string Path { get; }

    public void WriteArrayInFile(int[] arr, string nameFile)
    {
        if (arr == null) throw new ArgumentNullException(nameof(arr));
        if (arr.Length == 0) throw new ArgumentException("Массив не может быть пустой коллекцией.", nameof(arr));

        using (var sw = new StreamWriter(Path + nameFile))
        {
            for (var i = 0; i < arr.Length; i++) sw.Write(arr[i] + ",");
        }
    }
}