using System;
using System.Collections.Generic;
using System.Text;

namespace Atom.Models2
{
    public static class RandomExtension
    {
        public static Project NextProject(this Random random, int countgroup, int countstage, int countwork)
        {
            Project proj = new Project();
            for(var i =0; i< countgroup; i++)
            {
                var gr = new Group();
                for (var j = 0; j < countwork; j++)
                {
                    var w = new Work();
                    gr.Works.Add(w);
                }
                proj.Groups.Add(gr);
            }
            for (var i = 0; i < countstage; i++)
            {
                var gr = new Stage();
                proj.Stages.Add(gr);
            }
            return proj;
        }
       
    }
}
