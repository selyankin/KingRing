using System.Drawing;
using KingRing.Interfaces;

namespace KingRing
{
    public class CreatureAnimation
    {
        public ICell Creature;
        public CreatureCommand Command;
        public Point Location;
        public Point TargetLogicalLocation;
    }
}