using System;

namespace logParser
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isValid = false;
            ExportData exportData = new ExportData();
            for (int i = 0; i < args.Length - 1; i++)
            {

                switch (args[i])
                {
                    case "--log-dir":
                        {
                            exportData.logFilePath = args[i + 1];
                            i++;
                            isValid = true;
                            continue;
                        }
                    case "--log-level":
                        {
                            exportData.Levels.Add(args[i + 1].ToUpper()); ;
                            i++;
                            isValid = true;
                            continue;
                        }
                    case "--csv":
                        {
                            exportData.xcvExportPath = args[i + 1];
                            i++;
                            continue;
                        }
                }
            }

            if (isValid)
            {
                exportData.getFilesData();
            }
            else if (args[0] == "'--help'")
            {
                Console.WriteLine("Usage: logParser --log-dir <dir> --log-level <level> --csv <out>\n --log-dir   Directory to parse recursively for .log files\n    --csv       Out file-path (absolute/relative)");
            }
            else { 
                Console.WriteLine("Please enter valid input.");
            }
        }
    }
}
