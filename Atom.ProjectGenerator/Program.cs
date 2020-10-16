using Atom.Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using static Atom.Models.RandomExtension;

namespace Atom.ProjectGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Nastya!!");
                    
            Random random = new Random();
            //var project = random.NextProject(0,200000);

            var project = random.NextProject(2, 2, 10, 0 );

            

            var c = project.GetRequeredResource();

            project.ExistingResource = new ResourceStorage();
            project.ExistingResource.Add(new Resource("деньги", 3.0d));

            project.AddLink(new Link(project.SubProjects.ElementAt(0).Value, project.SubProjects.ElementAt(1).Value, linkType: LinkType.Before));

            var def = project.GetResourceSheet();

            var m =project.GetLinkMatrix();
            m.Display();


            JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings()
            {
                MetadataPropertyHandling = MetadataPropertyHandling.ReadAhead,
                NullValueHandling = NullValueHandling.Include,
                StringEscapeHandling = StringEscapeHandling.Default,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            var ser = JsonConvert.SerializeObject(project, JsonSerializerSettings);
            Console.WriteLine(ser);
            File.WriteAllText("1.txt", ser);

            Console.WriteLine();
            RandomTimetableCreator randomTimetableCreator = new RandomTimetableCreator();
            var time = randomTimetableCreator.Create(project);
            time.Display();
            time.ExportTimetable("timetable.csv");
        }

     
    }
}
