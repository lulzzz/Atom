using System;
using System.Collections.Generic;
using System.Text;

namespace Atom.Models
{
    public class RandomTimetableCreator : ITimetableCreator
    {
        public Timetable Create(Project project)
        {
            var random = new Random();
            Timetable timetable = new Timetable();
            foreach(var item in project.SubProjects)
            {             
                var start = DateTime.Now;            
                var duration = new TimeSpan(random.Next());
                var finish = start + duration;
                var time = new TimetableProjectOptions()
                {
                    Start = start,
                    Duration = duration,
                    Finish = finish
                };
                timetable.Tasks.Add(item.Value, time);
            }          
            return timetable;
        }

       
    }
}
