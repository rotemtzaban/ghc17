using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace DronesProblem
{
    class Program
    {
        static void Main(string[] args)
        {
            Runner<DronesInput, DronesOutput> runner = new Runner<DronesInput, DronesOutput>(new DronesParser(), new DronesSolver(), new DronesPrinter(), new DronesScoreCalculator());

            runner.Run(DronesProblem.Properties.Resources.Example, "Example");
        }

		private static void CreateCodeZip()
		{
			var tmpDirectoryName = "tmp";

			var sourceDir = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)));
			var tmpFolder = Path.Combine(sourceDir, tmpDirectoryName);
			if (Directory.Exists(tmpFolder))
				Directory.Delete(tmpFolder, true);
			Directory.CreateDirectory(tmpFolder);
			foreach (var codeFile in Directory.EnumerateFiles(sourceDir, "*.cs", SearchOption.AllDirectories))
			{
				var relative = codeFile.Substring(sourceDir.Length + 1);
				if (relative.StartsWith("obj") || relative.StartsWith(tmpDirectoryName))
					continue;
				var target = Path.Combine(tmpFolder, relative);
				var dir = Path.GetDirectoryName(target);
				if (!Directory.Exists(dir))
					Directory.CreateDirectory(dir);
				File.Copy(codeFile, target);
			}

			var targetZip = Path.Combine(sourceDir, "out", "Code.zip");

			if (File.Exists(targetZip))
				File.Delete(targetZip);
			ZipFile.CreateFromDirectory(tmpFolder, targetZip);

			Directory.Delete(tmpFolder, true);
		}
    }
}
