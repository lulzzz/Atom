using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Atom.Models
{
    public class Timetable
    {
        public Dictionary<Project, TimetableProjectOptions> Tasks = new Dictionary<Project, TimetableProjectOptions>();


        public Timetable()
        {

        }
    
        public void ExportTimetable(string path)
        {
            using(StreamWriter sw = new StreamWriter(path))
            {
                for(var i=0; i< Tasks.Count; i++)
                {
                    var item = Tasks.ElementAt(i);
                    sw.WriteLine($"{item.Key.Id};{item.Key.Name};{item.Value.Start};{item.Value.Finish};{item.Value.Duration}");          
                }
                sw.Close();
            }
        }

        public void Display()
        {
            for (var i = 0; i < Tasks.Count; i++)
            {
                var item = Tasks.ElementAt(i);
                Console.WriteLine($"{item.Key.Id};{item.Key.Name};{item.Value.Start};{item.Value.Finish};{item.Value.Duration}");
            }         
        }
    }

    public class TimetableProjectOptions
    {
        public TimetableProjectOptions()
        {

        }
        public object FuzzyStart { get; set; }
        public object FuzzyFinish { get; set; }
        public object FuzzyDuration { get; set; }

        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
