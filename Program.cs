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

        public static Dictionary<string, int> jobDayOffset;

        static void Main(string[] args)
        {
            GetJobOffset();
            //DayTest();
            if (DateTime.Now.Hour % 4 == 0 && DateTime.Now.Minute % 30 == 0) GetJobOffset();    //I don't know how else to simulate a hard reset of our dictionary
            DayAlternative();   // just wanted to try a more efficient way of doing this
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
        
        public static void DayAlternative()
        {
            Console.WriteLine("Type in a job number");
            var jobNumber = Console.ReadLine();
            var sla = GetAlternativeJobSLANumber(jobNumber);
            var day = GetDay(sla);

            Console.WriteLine($"{day} - {GetColor(day)}");
        }

        private static int GetAlternativeJobSLANumber(string number)
        {
            return jobDayOffset[number];
        }

        private static void GetJobOffset()
        {
            if (jobDayOffset == null)
                jobDayOffset = new Dictionary<string, int>();   //We're going to do a hard reset of the object here, I think we could get away with looking through the file for new keys though
            else
                jobDayOffset.Clear();
            
            var file = File.ReadAllLines(SLAFile);

            for (int i = 1; i < file.Length - 1; i++)
            {
                var parts = file[i].Split('\t');
                if (parts[3] != string.Empty)
                    jobDayOffset.Add(parts[0], Int32.Parse(parts[3]));
                else
                    jobDayOffset.Add(parts[0], -1);
            }
        }

        private static void GetSoftResetJobOffset()
        {
            //note here we do not clear the dictionary
            var file = File.ReadAllLines(SLAFile);

            for (int i = 0; i < file.Length - 1; i++)
            {
                var line = file[i].Split('\t');
                if(!jobDayOffset.ContainsKey(line[0]))
                {
                    if (line[3] != string.Empty)
                        jobDayOffset.Add(line[0], Int32.Parse(line[3]));
                    else

                        jobDayOffset.Add(line[0], -1);
                }
            }
        }

        public void STEPH_CODE()
        {
            /*
             
             */
        }   
    }
}
