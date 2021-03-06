﻿using KingRing.Interfaces;

namespace KingRing.GameObjects
{
    public class Tree : ICell
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
            return 2;
        }

        public string GetImageFileName()
        {
            return "tree.png";
        }
    }
}
