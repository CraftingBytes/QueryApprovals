using System;
using System.Diagnostics;
using System.IO;

namespace QueryApprovals
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionStringFile1 = args[0];
            var connectionStringFile2 = args[1];
            var queryFile1 = args[2];
            var queryFile2 = args[3];
            string outputDir = args[4];

            var connectionString1 = File.ReadAllText(connectionStringFile1) ;
            var connectionString2 = File.ReadAllText(connectionStringFile2);
            var query1 = File.ReadAllText(queryFile1);
            var query2 = File.ReadAllText(queryFile2);

            var output1 = Path.GetFileNameWithoutExtension(connectionStringFile1)
                    + "-" + Path.GetFileNameWithoutExtension(queryFile1) + ".txt";
            var output2 = Path.GetFileNameWithoutExtension(connectionStringFile2)
                    + "-" + Path.GetFileNameWithoutExtension(queryFile2) + ".txt";

            var dataTable1 = DatabaseHelper.GetDataTable(connectionString1, query1);
            var dataTable2 = DatabaseHelper.GetDataTable(connectionString2, query2);

            string contents1 = dataTable1.ToFormattedTableString();
            string contents2 = dataTable2.ToFormattedTableString();

            string path1 = Path.Combine(outputDir, output1);
            File.WriteAllText(path1, contents1);
            string path2 = Path.Combine(outputDir, output2);
            File.WriteAllText(path2, contents2);

            if (contents1 != contents2)
            {
                var bComparePath = @"C:\Program Files\Beyond Compare 4\bcompare.exe";
                var startInfo = new ProcessStartInfo()
                {
                    FileName = bComparePath,
                    Arguments = $"{path1} {path2}"
                };
                Process.Start(startInfo);
            }
        }

    }
}
