using System;
using System.Collections.Generic;
using System.Text;

namespace Atom.Models
{
    public enum ResourceType
    {
        None = 0,
        Time = 1 << 0,
        /// <summary>
        /// Эксклюзивные.
        /// </summary>
        Dedicated = 1 << 1,
        /// <summary>
        /// Возообновляемые.
        /// </summary>
        Renewable = 1 << 2,
        /// <summary>
        /// Невозобновляемы.
        /// </summary>
        Nonrenewable = 1 << 3,
        DoublyConstrained = 1 << 4,
        PartiallyRenewable = 1 << 5,
        Cumulative = 1 << 6,
        Continuous = 1 << 7,
    }
}
