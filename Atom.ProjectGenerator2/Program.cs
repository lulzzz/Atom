using Newtonsoft.Json;
using System;
using System.IO;
using static Atom.Models2.RandomExtension;

namespace Atom.ProjectGenerator2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var random = new Random();
            var project = random.NextProject(2,15,20000);

            var JsonSerializerSettings = new JsonSerializerSettings()
            {
                MetadataPropertyHandling = MetadataPropertyHandling.ReadAhead,
                NullValueHandling = NullValueHandling.Include,
                StringEscapeHandling = StringEscapeHandling.Default,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            var ser = JsonConvert.SerializeObject(project, JsonSerializerSettings);
            Console.WriteLine(ser);
            File.WriteAllText("1.txt", ser);

        }
    }
}
