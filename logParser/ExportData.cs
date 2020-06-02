using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace logParser
{
    class ExportData
    {

        public String logFilePath;
        public String xcvExportPath;
        public List<String> Levels = new List<String>();


        string getFile(string reusltFilePath)
        {
            String filePath = reusltFilePath != null ? reusltFilePath : $"{logFilePath}/result.csv";
            
            return filePath;
        }
        public void getFilesData()
        {
            try
            {
                string reusltFilePath = getFile(xcvExportPath);
                string[] dirs = Directory.GetFiles(logFilePath, "*.log");
                using (var fileWriter = new StreamWriter(reusltFilePath))
                    try
                    {
                        int counter = 1;
                        fileWriter.WriteLine("Count, Date, Time, Logging Level, Information");
                        foreach (string dir in dirs)
                        {
                            string[] fileText = readFile(dir);
                            foreach (var line in fileText)
                            {
                                exportToCSV(fileWriter, line, ref counter);
                            }
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Something went wrong, Please try again.");
                    }
                    finally
                    {
                        fileWriter.Close();
                    }
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not find the directory " + e.ToString());
            }
        }

        public string[] readFile(String dir)
        {
            try
            {
                string[] readText = File.ReadAllLines(dir);
                return readText;
            }
            catch (Exception)
            {
                Console.WriteLine($"Error in reading file {dir}");
            }
            return null;
        }

        public void exportToCSV(StreamWriter fileWriter, String line, ref int counter)
        {
            var info = "";
            var level = "";
            var time = "";
            var date = "";

            var match = Regex.Match(line, @"([^\s]*\s\s)",
                RegexOptions.IgnoreCase);
            level = match.Groups[1].Value;

            if (Levels.Contains(level.Trim()))
            {
                match = Regex.Match(line, @"(\s+[^\s]*)",
                RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    time = match.Groups[1].Value;

                }

                match = Regex.Match(line, @"([^\s]+)",
                RegexOptions.IgnoreCase);

                if (match.Success)
                {
                    date = match.Groups[1].Value;
                }

                match = Regex.Match(line, @"(\s:.+[^\s]*)",
                RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    info = match.Groups[1].Value;
                }
                var csv = new StringBuilder();
                fileWriter.WriteLine($"{counter}, {date}, {time}, {level}, {info}");
                counter++;
            }
        }
    }
}
