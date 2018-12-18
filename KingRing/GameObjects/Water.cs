using KingRing.Interfaces;

namespace KingRing.GameObjects
{
    public class Water : ICell
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
            return 4;
        }

        public string GetImageFileName()
        {
            return "water.png";
        }
    }
}
