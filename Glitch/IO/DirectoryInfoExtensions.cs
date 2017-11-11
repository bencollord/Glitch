using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glitch
{
    public static class DirectoryInfoExtensions
    {
        public static FileInfo CreateFile(this DirectoryInfo folder, string fileName) => new FileInfo(Path.Combine(folder.FullName, fileName));
    }
}
