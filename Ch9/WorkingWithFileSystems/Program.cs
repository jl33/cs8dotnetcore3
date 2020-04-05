using System.IO;
using static System.Console;
using static System.IO.Directory;
using static System.Environment;
using static System.IO.Path;
using static System.IO.DriveInfo;

namespace WorkingWithFileSystems
{
    class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine("Hello World!");

            // OutputFileSystemInfo();
            // WorkWithDrives();
            // WorkWithDirectories();
            WorkWithFiles();
        }

        static void OutputFileSystemInfo(){
            WriteLine("{0,-33} {1}","Path.PathSeparator",PathSeparator);
            WriteLine("{0,-33} {1}","Path.DirectorySeparatorChar",DirectorySeparatorChar);
            WriteLine("{0,-33} {1}", "Directory.GetCurrentDirectory()",GetCurrentDirectory());
            WriteLine("{0,-33} {1}","Environment.CurrentDirectory",CurrentDirectory);
            WriteLine("{0,-33} {1}","Environment.SystemDirectory",SystemDirectory);
            WriteLine("{0,-33} {1}","Environment.OSVersion",OSVersion);
            WriteLine("{0,-33} {1}","Environment.ProcessorCount",ProcessorCount);
            WriteLine("{0,-33} {1}","Path.GetTempPath()",GetTempPath());
            WriteLine("GetFolderPath(SpecialFolder");
            WriteLine("{0,-33} {1}","  .System",GetFolderPath(SpecialFolder.System));
            WriteLine("{0,-33} {1}","  .ApplicationData",GetFolderPath(SpecialFolder.ApplicationData));
            WriteLine("{0,-33} {1}","  .MyDocuments",GetFolderPath(SpecialFolder.MyDocuments));
            WriteLine("{0,-33} {1}","  .Personal",GetFolderPath(SpecialFolder.Personal));
        }

        static void WorkWithDrives(){
            WriteLine("{0,-30} | {1,-10} | {2,-7} | {3,18} | {4,18}",
              "Name","Type","Format","Size(Bytes)","Free space");
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if(drive.IsReady){
                    WriteLine("{0,-30} | {1,-10} | {2,-7} | {3,18:N0} | {4,18:N0}",
                      drive.Name,drive.DriveType,drive.DriveFormat,drive.TotalSize,drive.AvailableFreeSpace);
                }
            }
        }

        static void WorkWithDirectories(){
            var newFolder=Combine(GetFolderPath(SpecialFolder.Personal),
              "Code","Ch9","NewFolder");
            WriteLine($"Working with: {newFolder}");
            WriteLine($"Does it exist? {Exists(newFolder)}");
            WriteLine("Creating it ...");
            CreateDirectory(newFolder);
            WriteLine($"Does it exist? {Exists(newFolder)}");
            Write("Confirm the directory exists, and then press ENTER: ");
            ReadLine();
            WriteLine("Deleteing it ...");
            Delete(newFolder,recursive: true);
            WriteLine($"Does it exist? {Exists(newFolder)}");
        }

        static void WorkWithFiles(){
            var dir=Combine(GetFolderPath(SpecialFolder.Personal),
              "Code","Ch9","OutputFiles");
            CreateDirectory(dir);
            string textFile=Combine(dir,"Dummy.txt");
            string backupFile=Combine(dir,"Dummy.bak");
            WriteLine($"Working with: {textFile}");
            WriteLine($"Does it exist? {File.Exists(textFile)}");
            StreamWriter textWriter=File.CreateText(textFile);
            textWriter.WriteLine("Hello C#!");
            textWriter.Close();
            WriteLine($"Does it exist? {File.Exists(textFile)}");

            File.Copy(sourceFileName: textFile,destFileName: backupFile, overwrite: true);
            WriteLine($"Does {backupFile} exist? {File.Exists(backupFile)}");
            Write("Confirm the files exist, and then press ENTER: ");
            ReadLine();

            File.Delete(textFile);
            WriteLine($"Does it exist? {File.Exists(textFile)}");
            WriteLine($"Reading contents of {backupFile}:");
            StreamReader textReader=File.OpenText(backupFile);
            WriteLine(textReader.ReadToEnd());
            textReader.Close();

            WriteLine($"Folder name: {GetDirectoryName(textFile)}");
            WriteLine($"File Name: {GetFileName(textFile)}");
            WriteLine("File Name without Etension: {0}",
              GetFileNameWithoutExtension(textFile));
            WriteLine($"File Extension: {GetExtension(textFile)}");
            WriteLine($"Random File Name: {GetRandomFileName()}");
            WriteLine($"Temporary File Name: {GetTempFileName()}");

            var info = new FileInfo(backupFile);
            WriteLine($"{backupFile}:");
            WriteLine($"Contains {info.Length} bytes");
            WriteLine($"Last accessed {info.LastAccessTime}");
            WriteLine($"Has readonly set to {info.IsReadOnly}");
            WriteLine($"is the backup file compressed? {0}",
              info.Attributes.HasFlag(FileAttributes.Compressed));

        }


    }
}
