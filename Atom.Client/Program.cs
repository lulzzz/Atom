using System;
using System.IO;
using Akka.Actor;
using Atom.ActorModel;
using Mono.Options;
using Newtonsoft.Json;

namespace Atom.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var config = "Configuration.json";
            var OptionSet = new OptionSet
            {
                {"c|config=", "config", x=>config = x },
                {"start", "start", Start, false }
            };
            OptionSet.Parse(args);
                       

            Start("");
            while (true)
            {
                try
                {
                    var arg = Console.ReadLine();
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(e.Message);
                    Console.ResetColor();
                }
            }
        }

        public void ReadProject()
        {

        }
        public void CommandProject()
        {

        }

        public static void Start(string arg)
        {
      
            Console.WriteLine($"Try create system...");
            ActorSystemRefs.ActorSystem = ActorSystem.Create(DreamConfig.AkkaActorSystemName, DreamConfig.Hocon);
            SystemActors.Manager = ActorSystemRefs.ActorSystem
               .ActorOf(Props.Create(() => new ClientProjectActor()), "Manager");
            //SystemActors.Manager.Tell(new InitilizeDBByStorage());
            Console.WriteLine($"ActorSystem is created.");
        }


    }
}
