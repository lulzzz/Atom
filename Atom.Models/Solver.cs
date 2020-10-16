using System;
using System.Collections.Generic;
using System.Text;

namespace Atom.Models
{
    public class Solver
    {
        public int MaxIterations = 1000;
        public int PopulationCount = 100;//должно делиться на 4

        public List<Func<Plan, int>> FitnessFunctions = new List<Func<Plan, int>>();
    }
}
