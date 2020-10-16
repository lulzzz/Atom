using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Atom.Models
{
    public class Flow
    {
        public string ResourceName { get; set; }

        Point[] _points;

        public Flow(params Point[] points)
        {
            _points = points;
        }

        public object GetValue(DateTime date)
        {
            var max = _points.Where(x => x.date > date).Min();
            var min = _points.Where(x => x.date < date).Max();
            var lenght = max.date - date;
            var result = (max.value - min.value) / (max.date.Ticks - min.date.Ticks) * lenght.Ticks;
            return result; 
        }
    }

    public record Point
    {
        public DateTime date { get; set; }
        public double value { get; set; }
    }

}
