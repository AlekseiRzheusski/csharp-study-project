using System.IO.Compression;
using System.Runtime.InteropServices;

namespace FileSystem;

public static class FileSystemStudy
{
    static string currentDirectory = Directory.GetCurrentDirectory();
    const string TXT_FILE_NAME = "file.txt";
    const string BINARY_FILE_NAME = "file.dat";
    static void ShowDriveInfo()
    {
        DriveInfo[] drives = DriveInfo.GetDrives();

        foreach (var drive in drives)
        {
            Console.WriteLine($"Name: {drive.Name}");
            Console.WriteLine($"Type: {drive.DriveType}");
            if (drive.IsReady)
            {
                Console.WriteLine($"AvailableFreeSpace: {drive.AvailableFreeSpace}");
                Console.WriteLine($"DriveFormat: {drive.DriveFormat}");
                Console.WriteLine($"RootDirectory: {drive.RootDirectory}");
                Console.WriteLine($"TotalFreeSpace: {drive.TotalFreeSpace}");
                Console.WriteLine($"TotalSize: {drive.TotalSize}");
                Console.WriteLine($"VolumeLabel:: {drive.VolumeLabel}");

            }
        }
    }

    static void ShowFileInfo(string path)
    {
        FileInfo fileInfo = new FileInfo(path);
        Console.WriteLine($"Name: {fileInfo.Name}");
        Console.WriteLine($"Length:{fileInfo.Length}");

        DirectoryInfo directoryInfo = fileInfo.Directory!;
        Console.WriteLine($"Directory: {directoryInfo.Name}");
        Console.WriteLine($"Root: {directoryInfo.Root}");
        Console.WriteLine($"LastAccessTime: {directoryInfo.LastAccessTime}");
        Console.WriteLine($"LastWriteTime: {directoryInfo.LastWriteTime}");
    }

    static void WriteFile(string path, IEnumerable<string> lines)
    {
        //параметр append указывает, надо ли добавлять в конец файла данные или же перезаписывать файл.
        //Если равно true, то новые данные добавляются в конец файла. Если равно false, то файл перезаписываетсяя заново
        using (StreamWriter writer = new StreamWriter(path, false))
        {
            foreach (var line in lines)
            {
                writer.WriteLine(line);
            }
        }
        Console.WriteLine();
        Console.WriteLine("A new file was created");
        ShowFileInfo(path);
    }

    static void ReadFile(string path)
    {
        Console.WriteLine();
        Console.WriteLine("Read File: {0}", path);
        using (StreamReader reader = new StreamReader(path))
        {
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
        }
    }

    static void WriteBinaryFile(string path, IEnumerable<string> lines)
    {
        // FileMode:
        // Append: если файл существует, то текст добавляется в конец файл. Если файла нет, то он создается. Файл открывается только для записи.
        // Create: создается новый файл. Если такой файл уже существует, то он перезаписывается
        // CreateNew: создается новый файл. Если такой файл уже существует, то приложение выбрасывает ошибку
        // Open: открывает файл. Если файл не существует, выбрасывается исключение
        // OpenOrCreate: если файл существует, он открывается, если нет - создается новый
        // Truncate: если файл существует, то он перезаписывается. Файл открывается только для записи.
        using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.OpenOrCreate)))
        {
            foreach (var line in lines)
            {
                writer.Write(line);
            }
            Console.WriteLine();
            Console.WriteLine("A new file was created");
            ShowFileInfo(path);
        }
    }

    static void ReadBinaryFile(string path)
    {
        Console.WriteLine();
        Console.WriteLine("Read File: {0}", path);
        using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
        {
            //метод PeekChar считывает следующий символ и возвращает его числовое представление. 
            //Если символ отсутствует, то метод возвращает -1
            while (reader.PeekChar() > -1)
            {
                string line = reader.ReadString();
                Console.WriteLine(line);
            }
        }
    }

    static void CreateDirectoryAndFiles(string filesPath)
    {
        if (!Directory.Exists(filesPath))
        {
            Directory.CreateDirectory(filesPath);
        }

        string[] lines =
        {
            "first line",
            "second line",
            "third line"
        };
        WriteFile($"{filesPath}/{TXT_FILE_NAME}", lines);
        WriteBinaryFile($"{filesPath}/{BINARY_FILE_NAME}", lines);
    }

    static void ReadFiles(string filesPath)
    {
        ReadFile($"{filesPath}/{TXT_FILE_NAME}");
        ReadBinaryFile($"{filesPath}/{BINARY_FILE_NAME}");
    }

    static void MoveFiles(string filesPath, string newFilesPath)
    {
        if (!Directory.Exists(newFilesPath))
        {
            Directory.CreateDirectory(newFilesPath);
        }

        FileInfo txtFileInfo = new FileInfo($"{filesPath}/{TXT_FILE_NAME}");
        FileInfo binaryFileInfo = new FileInfo($"{filesPath}/{BINARY_FILE_NAME}");

        if (txtFileInfo.Exists)
        {
            //Если файл по новому пути уже существует, то с помощью дополнительного параметра можно указать,
            //надо ли перезаписать файл (при значении true файл перезаписывается)
            txtFileInfo.MoveTo($"{newFilesPath}/{TXT_FILE_NAME}", true);
        }
        if (binaryFileInfo.Exists)
        {
            binaryFileInfo.MoveTo($"{newFilesPath}/{BINARY_FILE_NAME}", true);
        }
    }

    public static void Run()
    {
        string filesPath = $"{currentDirectory}/files";
        string newFilesPath = $"{currentDirectory}/files1";
        try
        {
            ShowDriveInfo();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception: {0}", ex.Message);
        }

        CreateDirectoryAndFiles(filesPath);
        ReadFiles(filesPath);
        MoveFiles(filesPath, newFilesPath);

        ZipFile.CreateFromDirectory(newFilesPath, $"{filesPath}/files.zip");

        Console.WriteLine("Press any key to procees, after that all of the created directories and files will be removed.");
        Console.ReadKey();
        if (Directory.Exists(filesPath))
        {
            //true означает что нужно удалить папку с содержимым
            Directory.Delete(filesPath, true);
        }
        if (Directory.Exists(newFilesPath))
        {
            Directory.Delete(newFilesPath, true);
        }
    }
}