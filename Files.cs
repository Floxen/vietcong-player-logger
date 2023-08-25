using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCLogger
{
    internal class Files
    {
        public static long getFilesize(string pathToFile)
        {
            return new FileInfo(pathToFile).Length;
        }

        public static string getLastFileLine(string pathToFile)
        {
            if (!File.Exists(pathToFile)) return null; return File.ReadLines(pathToFile).Last();
        }
    }
}
