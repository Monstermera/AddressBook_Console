using System.IO;

namespace AddressBook.Service;

internal class FileService
{
    private string FilePath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\contactConsole.json";

    public void Save(string content)
    {
        using var sw = new StreamWriter(FilePath);
        sw.WriteLine(content);
    }

    public string Read()
    {
        try
        {
            using var sr = new StreamReader(FilePath);
            return sr.ReadToEnd();
        }
        catch { return null!; }
    }
}
