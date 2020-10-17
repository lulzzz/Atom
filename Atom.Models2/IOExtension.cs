using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Atom.Models2
{
    public static class IOExtension
    {
        public static void ExportTimetable(this Project project, string path)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine($"Id;Name;PriceEarlier;PriceLate;PriceDurationChanged;Start;Duration;DurationMin;Finish");
                sw.WriteLine("Stages");
                for (var i = 0; i < project.Stages.Count; i++)
                {
                    sw.WriteLine($"{project.Stages[i].Id};{project.Stages[i].Name};{project.Stages[i].PriceEarlier};{project.Stages[i].PriceLate};{project.Stages[i].PriceDurationChanged};{project.Stages[i].Start};{project.Stages[i].Duration};{project.Stages[i].DurationMin};{project.Stages[i].Finish}");
                }
                sw.WriteLine("Works");
                for (var i = 0; i < project.Groups.Count; i++)
                {
                    for (var j = 0; j < project.Groups[i].Works.Count; j++)
                    {
                        sw.WriteLine($"{project.Groups[i].Works[j].Id};{project.Groups[i].Works[j].Name};{project.Stages[i].PriceEarlier};{project.Stages[i].PriceLate};{project.Stages[i].PriceDurationChanged};{project.Groups[i].Works[j].Start};{project.Groups[i].Works[j].Duration};{project.Stages[i].DurationMin};{project.Groups[i].Works[j].Finish}");
                    }
                }
                sw.Close();
            }
        }

        public static void Display(this Project project)
        {
            Console.WriteLine($"Id;Name;Start;PriceEarlier;PriceLate;PriceDurationChanged;Duration;DurationMin;Finish");
            Console.WriteLine("Stages");
            for (var i = 0; i < project.Stages.Count; i++)
            {
                Console.WriteLine($"{project.Stages[i].Id};{project.Stages[i].Name};{project.Stages[i].Start};{project.Stages[i].Finish};{project.Stages[i].Duration}");
            }
            Console.WriteLine("Works");
            for (var i = 0; i < project.Groups.Count; i++)
            {
                for (var j = 0; j < project.Groups[i].Works.Count; j++)
                {
                    Console.WriteLine($"{project.Groups[i].Works[j].Id};{project.Groups[i].Works[j].Name};{project.Groups[i].Works[j].Start};{project.Groups[i].Works[j].Finish};{project.Groups[i].Works[j].Duration}");
                }
            }
        }
    }
}
