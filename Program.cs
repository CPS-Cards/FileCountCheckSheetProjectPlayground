using System;
using System.IO;

namespace FileCountCheckSheetProjectPlayground
{
    class Program
    {
        public const string SLAFile = @"S:\Planner\Daily Reporting\Daily reporting data.tab";
        static void Main(string[] args)
        {
            int jobNumber = 5495;
            var sla = GetJobSLANumber(jobNumber);
            var day = GetDay(sla);

            Console.WriteLine(day);
            Console.ReadLine();
        }

        public static int GetJobSLANumber(int number)
        {
            var file = File.ReadAllLines(SLAFile);

            for (int i = 1; i < file.Length - 1; i++)
            {
                var parts = file[i].Split('\t');
                if (Int32.Parse(parts[0]) == number) return Int32.Parse(parts[3]);
            }
            return -1;
        }
        public static string GetDay(int offset)
        {
            var today = DateTime.Now;
            today = today.AddDays(offset);
            var result = today.DayOfWeek.ToString();
            if (result.ToString() == "Saturday" || result.ToString() == "Sunday")
                result = "Monday";
            return result;
        }
    }
}
