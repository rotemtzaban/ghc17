using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HashCodeCommon
{
    public static class ZipCreator
    {
        public static void CreateCodeZip(string outputDirectory)
        {
            var tmpDirectoryName = "tmp";

            var solutionPath = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))));
            var tmpFolder = Path.Combine(solutionPath, tmpDirectoryName);
            if (Directory.Exists(tmpFolder))
                Directory.Delete(tmpFolder, true);
            Directory.CreateDirectory(tmpFolder);
            foreach (var codeFile in Directory.EnumerateFiles(solutionPath, "*", SearchOption.AllDirectories))
            {
                var relative = codeFile.Substring(solutionPath.Length + 1);
                if (relative.StartsWith(tmpDirectoryName) || relative.StartsWith("Output") || relative.StartsWith("packages") || relative.StartsWith(".vs"))
                    continue;

                int indexSubString = relative.IndexOf("\\");
                var projectDir = relative.Substring(indexSubString == -1 ? 0 : indexSubString + 1);
                if (projectDir.StartsWith("obj") || projectDir.StartsWith("bin") || projectDir.StartsWith("Resources") || relative.StartsWith(tmpDirectoryName))
                    continue;
                var target = Path.Combine(tmpFolder, relative);
                var dir = Path.GetDirectoryName(target);
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                File.Copy(codeFile, target);
            }

            var targetZip = Path.Combine(outputDirectory, "Code.zip");

            if (File.Exists(targetZip))
                File.Delete(targetZip);
            ZipFile.CreateFromDirectory(tmpFolder, targetZip);

            Directory.Delete(tmpFolder, true);

            Console.WriteLine("finish create zip");
        }
    }
}
