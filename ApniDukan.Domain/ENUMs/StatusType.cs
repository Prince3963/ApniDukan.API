using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApniDukan.Domain.ENUMs
{
    public enum StatusType
    {
        Todo = 1,
        InProgress = 2,
        ReadyToPickUp = 3,
        Completed = 4,
        Cancelled = 5,
        Rejected = 6,
    }
}
