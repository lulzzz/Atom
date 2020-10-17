using System;
using System.Collections.Generic;
using System.Text;

namespace Atom.Models2
{
    public class Runner
    {
        public void Run(Project project)
        {
            Timetable timetable = new Timetable();
            timetable.LinkStageWork = project.StageLinks;
            timetable.Stages = project.Stages;
            timetable.Works = project.Works;
            var Timetables = new List<Timetable>();

            var t = project.CreteTimetable(); 

            
                Timetables.Add(t);

            //for (int i = 0; i < Timetable.Count; i++)
            //    Timetable.Add(new Lessоn(groups[i], teachers[i]));

            var solver = new Solver();//создаем решатель

            //solver.FitnessFunctions.Add(FitnessFunctions.Windows);//будем штрафовать за окна
            //solver.FitnessFunctions.Add(FitnessFunctions.LateLesson);//будем штрафовать за поздние пары

        
            var res = solver.Solve(Timetables, new DateTime(2020,6,5));
            Console.ReadLine();     
        }
    }
}
