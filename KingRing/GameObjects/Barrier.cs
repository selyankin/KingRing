﻿using KingRing.Interfaces;

namespace KingRing.GameObjects
{
    public class Barrier : ICell
    {
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand();
        }

        public bool DeadInConflict(ICell conflictedObject)
        {
            return false;
        }

        public int GetDrawingPriority()
        {
            return 1;
        }

        public string GetImageFileName()
        {
            return "barrier.png";
        }
    }
}
