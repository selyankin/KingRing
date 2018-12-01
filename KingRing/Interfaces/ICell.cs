using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingRing.Interfaces
{
    public interface ICell
    {
        CreatureCommand Act(int x, int y);
        bool DeadInConflict(ICell conflictedObject);
    }
}