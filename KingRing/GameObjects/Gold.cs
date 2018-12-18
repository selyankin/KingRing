using System;
using KingRing.Interfaces;

namespace KingRing.GameObjects
{
    class Gold : ICell
    {
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand();
        }

        public bool DeadInConflict(ICell conflictedObject)
        {
            if (conflictedObject is Player)
                Game.Score += new Random().Next(1, 30);
            return true;
        }

        public int GetDrawingPriority()
        {
            return 4;
        }

        public string GetImageFileName()
        {
            return "gold.png";
        }
    }
}
