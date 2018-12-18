namespace KingRing.Interfaces
{
    public interface ICell
    {
        string GetImageFileName();
        int GetDrawingPriority();
        CreatureCommand Act(int x, int y);
        bool DeadInConflict(ICell conflictedObject);
    }
}