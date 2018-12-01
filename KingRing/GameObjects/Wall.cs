using KingRing.Interfaces;

namespace KingRing.GameObjects
{
    public class Wall : ICell
    {
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand();
        }

        public bool DeadInConflict(ICell conflictedObject)
        {
            return false;
        }
    }
}
