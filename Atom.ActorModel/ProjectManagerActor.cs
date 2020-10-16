using Akka.Actor;
using Atom.ActorModel.Messages;
using Atom.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Atom.ActorModel
{
    public class ProjectManagerActor : ReceiveActor
    {
        Queue<(IActorRef, AskResourceMessage)> AskProjects;     
        ProjectManagerConfiguration ProjectManagerConfiguration { get; set; }
        Project _project { get; set; }

        Timetable timetable { get; set; }
        public ProjectManagerActor()
        {
            Receive<FinishProjectMessage>(Handle);
            Receive<StartProjectMessage>(Handle);
            Receive<Project>(Handle);
            Receive<AskResourceMessage>(Handle);
        }
        public void Handle(AskResourceMessage message)
        {
            AskProjects.Enqueue((Sender, message));

        }
        public void Handle(FinishProjectMessage message)
        {

        }
        public void Handle(StartProjectMessage message)
        {
            Sender.Tell(new AskResourceMessage());
        }
        public void Handle(Project message)
        {
            _project = message;
        }

    }
}
