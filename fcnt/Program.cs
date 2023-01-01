using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace fcnt
{
    /// <summary>
    /// FCNT (Short for File Counter)
    /// This program counts all files with the specified extensions.
    /// 
    /// The code may be inefficient but i don't care
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            if(args.Length == 0)
            {
                Console.WriteLine("Specify Extensions (Type 'STOP' to stop): ");
                while (true)
                {
                    string extension = Console.ReadLine();

                    if(extension.Length < 1) { continue; }

                    if (extension.ToLower() == "stop")
                    {
                        break;
                    }

                    if (extension[0] != '.')
                    {
                        extension = $".{extension}";
                    }
                    Array.Resize(ref args, args.Length + 1);
                    args[args.Length - 1] = extension;
                }
            }

            Dictionary<string, int> fileCount = new Dictionary<string, int>();

            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.*", SearchOption.AllDirectories).Where(file =>
            {
                foreach (var ext in args)
                {
                    if (file.ToLower().EndsWith(ext))
                        return true;
                }

                return false;
            }).ToArray();

            foreach (var fPath in files)
            {
                foreach(var extension in args)
                {
                    if(fPath.EndsWith(extension))
                    {
                        fileCount[extension]++;
                    }
                }
            }

            foreach(var keyValue in fileCount)
            {
                // I do casting here because after some errors i dont trust C#
                float ratio = ((float)keyValue.Value / (float)files.Length) * 100;
                Console.WriteLine($"{keyValue.Key}: {keyValue.Value} files ({ratio.ToString("F2")}%)");
            }
            Console.ReadKey();
        }
    }
}
