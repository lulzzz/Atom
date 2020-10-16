using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atom.Models
{
    public class Link
    {
        public Project _projectTo { get; set; }
        public Project _projectFrom { get; set; }

        public LinkType _linkType { get; set; }

        public Link( Project projectFrom, Project projectTo, LinkType linkType = LinkType.Unknown)
        {
            _projectTo = projectTo;
            _projectFrom = projectFrom;
            _linkType = linkType;
        }
    }

    public class LinkMatrix
    {
        List<string> projects;
        LinkType[,] Matrix { get; set; }

        public LinkMatrix(Project project)
        {
            var links = project.Links;
            var n = project.SubProjects.Count;

           
            var pp = project.SubProjects.Select(x => x.Value.Id).ToList();
            pp.Add(project.Id);
            Matrix = new LinkType[pp.Count, pp.Count];


            for (var k=0; k < links.Count; k++)
            {
                var type = links[k]._linkType;
                var index1 = pp.IndexOf(links[k]._projectFrom.Id);
                var index2 = pp.IndexOf(links[k]._projectTo.Id);
                switch (type)
                {
                    case LinkType.Parent:                             
                            Matrix[index1, index2] = type;
                            Matrix[index2, index1] = LinkType.Child;
                            break;
                    case LinkType.Child:                
                        Matrix[index1, index2] = type;
                        break;             
                    case LinkType.Circle:                    
                        Matrix[index1, index2] = type;
                        break;                   
                    case LinkType.Before:                  
                        Matrix[index1, index2] = type;
                        Matrix[index2, index1] = LinkType.After;
                        break;               
                    case LinkType.After:
                        Matrix[index1, index2] = type;
                        Matrix[index2, index1] = LinkType.Before;
                        break;                
                }    
            }
            projects = pp;
        }

        public void Display()
        {
            Console.Write("\t");
            for (var i = 0; i< projects.Count; i++)
            {
                Console.Write(projects[i].Substring(0,5)+"..."+"\t");
            }
            Console.WriteLine();
            for (var j = 0; j < projects.Count; j++)
            {
                Console.Write(projects[j].Substring(0, 5) + "..." + "\t");
                for (var k = 0; k < projects.Count; k++)
                {
                    Console.Write(Matrix[j,k] + "\t");
                }
                Console.WriteLine();
            }
        }
    }
}
