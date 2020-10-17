using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atom.Models2
{
    [Serializable]
    public class Timetable : IComparable<Timetable>
    {
        public List<Work> Works { get; set; } = new List<Work>();
        public List<Stage> Stages { get; set; } = new List<Stage>();
        public Dictionary<Stage, Work> LinkStageWork { get; set; } = new Dictionary<Stage, Work>();

        public DateTime ActualDateTime { get; set; } 
        public double Cost { get; set; }

        public Random Random = new Random();
        public double ChangeStageEarlier(Stage stage, int count)
        {
            if (stage.PriceEarlier <= -1)
            {
                return double.MaxValue;
            }
            else
            {
                double sum = count * stage.PriceEarlier;
                for(var i =0; i < LinkStageWork.Count; i++)
                {
                    var date = stage.Start - new TimeSpan(count, 0, 0, 0);
                    var Links = LinkStageWork.Where(x => x.Key.Id == stage.Id).ToList();
                    for(var j =0; j< Links.Count; j++)
                    {
                        var len = (date - Links[j].Value.Start).Days;                   
                        sum += ChangeWorkLate(Links[j].Value, len);                       
                    }
                }
                Cost = sum;
                return sum;
            }
        }
        public double ChangeStageLate(Stage stage, int count)
        {
            if (stage.PriceEarlier <= -1)
            {
                return double.MaxValue;
            }
            else
            {
                double sum = count * stage.PriceLate;
                for (var i = 0; i < LinkStageWork.Count; i++)
                {
                    var date = stage.Start - new TimeSpan(count, 0, 0, 0);
                    var Links = LinkStageWork.Where(x => x.Key.Id == stage.Id).ToList();
                    for (var j = 0; j < Links.Count; j++)
                    {
                        var len = (date - Links[j].Value.Start).Days;
                        sum += ChangeWorkEarlier(Links[j].Value, len);
                    }
                }
                Cost = sum;
                return sum;
            }
        }
        public double ChangeStageDuration(Stage work, int count, DateTime now)
        {
            if (work.PriceDurationChanged <= -1)
            {
                return double.MaxValue;
            }
            if (work.DurationMin > (now - work.Start).Days - count)
            {
                return double.MaxValue;
            }
            Cost = count * work.PriceEarlier;
            return count * work.PriceEarlier;
        }


        public double ChangeWorkEarlier(Work work, int count)
        {
            if(work.PriceEarlier <= -1)
            {
                return double.MaxValue;
            }
            else
            {
                Cost = count * work.PriceEarlier;
                return count * work.PriceEarlier;
            }
        }
        public double ChangeWorkLate(Work work, int count)
        {
            if (work.PriceEarlier <= -1)
            {
                return double.MaxValue;
            }
            else
            {
                Cost = count * work.PriceLate;
                return count * work.PriceLate;
            }
        }
        public double ChangeWorkDuration(Work work, int count, DateTime now)
        {
            if (work.PriceDurationChanged <= -1)
            {
                return double.MaxValue;
            }
            if(work.DurationMin > (now - work.Start).Days - count)
            {
                return double.MaxValue;
            }          
            return count * work.PriceEarlier;           
        }

        /// <summary>
        /// Создание наследника с мутацией
        /// </summary>
        public Timetable Init(Timetable parent)
        {
            //копируем предка
            for (int i = 0; i < parent.Works.Count; i++)                
                this.Works.Add((Work)parent.Works[i].Clone());

            for (int i = 0; i < parent.Stages.Count; i++)
                this.Stages.Add((Stage)parent.Stages[i].Clone());

            for (int i = 0; i < parent.LinkStageWork.Count; i++)
                this.LinkStageWork.Add(parent.LinkStageWork.Keys.ElementAt(i), parent.LinkStageWork.Values.ElementAt(i));

            var lenghtLate = Random.Next(0, 10);
            var lenghtEarlier = Random.Next(0, 10);
            var lenghtDuration = Random.Next(0, 10);
            var lenghtWorkEarlier = Random.Next(0, 10);
            var lenghtWorkLate = Random.Next(0, 10);
            var lenghtWorkDuration = Random.Next(0, 10);

            var count1 = Random.Next(0, Works.Count);
            var count2 = Random.Next(0, Stages.Count);

            this.ChangeWorkLate(Works[count1], lenghtWorkLate);
            this.ChangeWorkEarlier(Works[count1], lenghtWorkEarlier);
            this.ChangeWorkDuration(Works[count1], lenghtWorkDuration, ActualDateTime);

            this.ChangeStageDuration(Stages[count2], lenghtDuration, ActualDateTime);
            this.ChangeStageEarlier(Stages[count2], lenghtEarlier);
            this.ChangeStageLate(Stages[count2], lenghtLate);
                      
            return this;
        }

        public int CompareTo(Timetable other)
        {
            if(this.Cost < other.Cost)
            {
                return 1;
            }
            else
            {
                return -1;
            } 
        }

        public class TimetableComp : IComparer<Timetable>
        {
            // Compares by Height, Length, and Width.
            public int Compare(Timetable x, Timetable y)
            {
                if (x.Cost.CompareTo(y.Cost) != 0)
                {
                    return 1;
                }              
                else
                {
                    return 0;
                }
            }
        }
    }
}
