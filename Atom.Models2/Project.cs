using System;
using System.Collections.Generic;
using System.Text;

namespace Atom.Models2
{
    public class Project
    {
        public List<Group> Groups { get; set; } = new List<Group>();
        public List<Stage> Stages { get; set; } = new List<Stage>();

        //List<WorkLink> WorkLinks { get; set; }
        public List<StageLink> StageLinks { get; set; } = new List<StageLink>();
    }

    public class Group
    {
        public List<Work> Works { get; set; } = new List<Work>();
    }

    //public class WorkLink
    //{
    //    public string IdWork1 { get; set; }
    //    public string IdWork2 { get; set; }

    //    public 
    //}
    public class StageLink
    {
        public string IdStage { get; set; } 
        public string IdWork { get; set; }
    }
}
