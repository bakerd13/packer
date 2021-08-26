using System;
using System.Collections.Generic;
using System.IO;

namespace PLPUnPack
{
    class Program
    {
        static void Main(string[] args)
        {
            string strExeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string strWorkPath = System.IO.Path.GetDirectoryName(strExeFilePath);

            if (args.Length == 0)
            {
                Console.WriteLine("No arguments supplied");
                Exit();
            }

            var unpack = args[0] == "-u" ? true : false;

            //var fullpath = Path.Combine(strWorkPath, args[0]);
            var fullpath = args[1];

            if (!Directory.Exists(fullpath))
            {
                Console.WriteLine("Directory Does Not Exist");
                Exit();
            }

            // Recursively get file names for all files in a directory.
            // ... Use EnumerateFiles to accommodate large result count.
            foreach (string file in Directory.EnumerateFiles(fullpath, "*.*", SearchOption.AllDirectories))
            {
                var ext = GetNewFileExtension(file, unpack);

                if (!string.IsNullOrEmpty(ext))
                {
                    var filePath = Path.GetDirectoryName(file);
                    var pFileName = Path.GetFileNameWithoutExtension(file);
                    var fileToMove = Path.Combine(filePath, pFileName);
                    var newFile = fileToMove + "." + ext;

                    Console.WriteLine("moving: " + file);
                    File.Move(file, newFile);
                }
            }

            Exit();
        }

        private static string GetNewFileExtension(string file, bool unpack)
        {
            var fileDict = new Dictionary<string, string>();

            if (unpack)
            {
                fileDict.Add(".jdab", "js");
                fileDict.Add(".xdab", "jsx");
                fileDict.Add(".jsondab", "json");
                fileDict.Add(".sldab", "sln");
                fileDict.Add(".projdab", "csproj");
                fileDict.Add(".userdab", "user");
                fileDict.Add(".txtdab", "txt");
                fileDict.Add(".cdab", "cs");
                fileDict.Add(".rdab", "razor");
            }
            else
            {
                fileDict.Add(".js", "jdab");
                fileDict.Add(".jsx", "xdab");
                fileDict.Add(".json", "jsondab");
                fileDict.Add(".sln", "sldab");
                fileDict.Add(".csproj", "projdab");
                fileDict.Add(".user", "userdab");
                fileDict.Add(".txt", "txtdab");
                fileDict.Add(".cs", "cdab");
                fileDict.Add(".razor", "rdab");
            }



            var ext = Path.GetExtension(file);

            if (fileDict.ContainsKey(ext))
            {
                var newExt = fileDict[ext];
                return newExt;
            }
            else
            {
                return string.Empty;
            }
        }

        static void Exit()
        {
            Console.WriteLine("Press andy key to continue......");
            Console.ReadLine();
        }
    }
}
