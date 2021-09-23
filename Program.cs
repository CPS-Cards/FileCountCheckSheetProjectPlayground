using System;
using System.Collections.Generic;
using System.IO;

namespace FileCountCheckSheetProjectPlayground
{
    class Program
    {
        public const string SLAFile = @"S:\Planner\Daily Reporting\Daily reporting data.tab";

        protected static readonly Dictionary<string, string> DayDictionary = new Dictionary<string, string>()
        {
            { "Monday", "Blue" }, { "Tuesday", "Green" }, { "Wednesday", "Black" }, { "Thursday", "Red" }, { "Friday", "Yellow" }, { "Default", string.Empty }
        };

        static void Main(string[] args)
        {
            DayTest();
        }

        public static void DayTest()
        {
            Console.WriteLine("Input a Job Number:");
            int jobNumber = Int32.Parse(Console.ReadLine());
            var sla = GetJobSLANumber(jobNumber);
            var day = GetDay(sla);
            var color = GetColor(day);

            Console.WriteLine($"{day} - {color}");
            Console.ReadLine();
        }

        public static int GetJobSLANumber(int number)
        {
            var file = File.ReadAllLines(SLAFile);

            for (int i = 1; i < file.Length - 1; i++)
            {
                var parts = file[i].Split('\t');
                if (parts[3] == string.Empty) continue;
                if (Int32.Parse(parts[0]) == number)
                    return Int32.Parse(parts[3]);
            }
            return -1;
        }
        public static string GetDay(int offset)
        {
            if (offset < 0) return "Default";
            var today = DateTime.Now;
            today = today.AddDays(offset);
            var result = today.DayOfWeek.ToString();
            if (result.ToString() == "Saturday" || result.ToString() == "Sunday")
                result = "Monday";
            return result;
        }

        public static string GetColor(string day)
        {
            return DayDictionary[day];
        }

        public void STEPH_CODE()
        {
            /*
             
             */
        }

        
    }
}
