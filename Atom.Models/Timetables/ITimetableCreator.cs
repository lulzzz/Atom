using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Atom.Models
{
    public interface ITimetableCreator
    {
        public Timetable Create(Project project);
    }
}
