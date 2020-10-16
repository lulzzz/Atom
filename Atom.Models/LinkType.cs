using System;
using System.Collections.Generic;
using System.Text;

namespace Atom.Models
{
    public enum LinkType
    {
        Unknown,
        Circle,
        After,
        Before,
        Parallel,        
        Parent,
        Child,
        Alternative
    }
}
