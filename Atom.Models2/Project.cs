using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;

namespace Atom.Models2
{
    [Serializable]
    public class Project
    {
        public DateTime _date = new DateTime();
      
        
        public List<Group> Groups { get; set; } = new List<Group>();
        public List<Work> Works { get => Groups.SelectMany(x => x.Works).ToList(); }
        public List<Stage> Stages { get; set; } = new List<Stage>();

        //List<WorkLink> WorkLinks { get; set; }
        public Dictionary<Stage, Work> StageLinks { get; set; } = new Dictionary<Stage, Work>();


        public Timetable CreteTimetable()
        {
            return new Timetable()
            {
                Stages = this.Stages,
                Works = this.Works,
                LinkStageWork = this.StageLinks
            };
        }

        public void CompleteWork(string Id, DateTime dateTime)
        {
            for(var i=0; i< Groups.Count; i++)
            {
                var work = Groups[i].Works.SingleOrDefault(x=>x.Id == Id);
                work.Finish = dateTime;
            }
        }

    

        public void RandomCompleteWorks(DateTime dateTime)
        {
            Random random = new Random();
            for (var i = 0; i < Groups.Count; i++)
            {
                for (var j = 0; j < Groups[i].Works.Count; j++)
                {
                    if(Groups[i].Works[j].Start + new TimeSpan((int)Groups[i].Works[j].Duration,0,0,0) < dateTime)
                    {
                        var ww = random.Next(0, 2);
                        if (ww == 1)
                        {
                            Groups[i].Works[j].Finish = Groups[i].Works[j].Start + new TimeSpan((int)Groups[i].Works[j].Duration, 0, 0, 0);
                        }
                    }                  
                }
            }
        }

        public void UpdateDate(DateTime date)
        {
            _date = date;
        }

        public List<Work> GetBadWorks()
        {
            var works = new List<Work>();
            for (var j = 0; j < Groups.Count; j++)
            {
                for (var i = 0; i < Groups[j].Works.Count; i++)
                {
                    var it = Groups[j].Works[i];
                    if (it.Finish < _date)
                    {
                        works.Add(it);
                    }                    
                }
            }
            return works;
        }

        public DateTime GetMinBound(List<Work> works)
        {
            var min = works.Min(x=>x.Start);
            return min;
        }

        public DateTime GetMaxBound(List<Work> works)
        {
            var max = works.Min(x => x.Finish);
            return max;
        }

        //public Timetable NewTimetable()
        //{
        //    var w = GetBadWorks();
        //    var min = GetMinBound(w);
        //    var interval = _date - min;
        //    var max = GetMaxBound(w);
        //    var sum = Summary(w, interval);
        //}

        //public double Summary(List<Work> w, TimeSpan min)
        //{



        //    for(var i=0; i<w.Count; i++)
        //    {
        //        w[i].Start = w[i].Start + min;
        //    }           
        //}

      
    }

    public class Group
    {
        public List<Work> Works { get; set; } = new List<Work>();
    }

    public class StageLink
    {
        public string IdStage { get; set; } 
        public string IdWork { get; set; }
    }
}
