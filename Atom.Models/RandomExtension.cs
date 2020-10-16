using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atom.Models
{
    public static class RandomExtension
    {
        public static Project NextProject(this Random random, int deep, params int[] widght)
        {
            Project project = random.NextProject();
            if (deep == 0)
            {
                for (var i = 0; i < widght[widght.Length - deep -1]; i++)
                {
                    project.AddChild(random.NextProject());
                }
            }
            else
            {
                for (var i = 0; i < widght[widght.Length - deep - 1 ]; i++)
                {
                    project.AddChild(random.NextProject(deep - 1, widght));
                }
            }
            return project;
        }


        public static Project NextProject(this Random random, int deep, int widght)
        {
            Project project = random.NextProject();            
            if (deep == 0)
            {            
                for(var i=0; i<widght; i++)
                {
                    project.AddChild(random.NextProject());
                }
            }
            else
            {
                for (var i = 0; i < widght; i++)
                {
                    project.AddChild(random.NextProject(deep - 1, widght));
                }       
            }           
            return project;
        }

        public static Project NextProject(this Random random)
        {
            Project project = new Project()
            {
                Id = Guid.NewGuid().ToString(),
                Name = $"{random.Next(0, int.MaxValue)}",                
                SignalOptions = new SignalOptions(),
                RequeredResources = random.NextResources()
            };           
            return project;
        }

        public static Resource NextResource(this Random random)
        {
            var Resource = new Resource();
            var i = random.Next(0, ResourceSample.Names.Count-1);
            Resource.Name = ResourceSample.Names[i];
            Resource.Value = random.NextDouble();
            return Resource;
        }

        public static ResourceStorage NextResources(this Random random)
        {
            var Resources = new ResourceStorage();
            var n = random.Next(1, ResourceSample.Names.Count - 1);
            for (var i = 0; i < n; i++)
            {
                Resources.Add(random.NextResource());
            }
            return Resources;
        }

        //public static Link NextLinks(this Random random, Project project, int count)
        //{
        //    var s1 = project.SubProjects.First();
        //    var s2 = project.SubProjects.Last();
        //    for (var i = 0; i < count; i++)
        //    {
        //        var index1 = random.Next(0, s1.Value.SubProjects.Count);
        //        var index2 = random.Next(0, s2.Value.SubProjects.Count);
        //        var link1 = new Link(s1.Value.SubProjects.ElementAt(index1).Value, s2.Value.SubProjects.ElementAt(index2).Value, LinkType.After);
        //        var link2 = new Link(s2.Value.SubProjects.ElementAt(index1).Value, s1.Value.SubProjects.ElementAt(index2).Value, LinkType.Before);
        //        project.AddLink(link1);
        //        project.AddLink(link2);
        //    }

        //}

        public static class ResourceSample
        {
            public static List<string> Names = new List<string>()
            {
                "строитель",
                "деньги",
                "машина",
                "электричество",
                "холодная вода",
                "горячая вода",
                "еда"
            };
        }
    }
}
