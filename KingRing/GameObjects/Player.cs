using System;
using System.Windows.Forms;
using KingRing.Interfaces;

namespace KingRing.GameObjects
{
    public class Player : ICell
    {
        private int Health { get; set; }

        public CreatureCommand Act(int x, int y)
        {
            var command = new CreatureCommand();
            switch (Game.KeyPressed)
            {
                case Keys.Down when y < Game.Height - 1 && CheckNearCell(x, y + 1):
                    command.DeltaY = 1;
                    break;
                case Keys.Up when y > 0 && CheckNearCell(x, y - 1):
                    command.DeltaY = -1;
                    break;
                case Keys.Left when x > 0 && CheckNearCell(x - 1, y):
                    command.DeltaX = -1;
                    break;
                case Keys.Right when x < Game.Width - 1 && CheckNearCell(x + 1, y):
                    command.DeltaX = 1;
                    break;
            }

            return command;
        }

        private static bool CheckNearCell(int x, int y)
        {
            return !(Game.Map[x, y + 1] is Wall) && !(Game.Map[x, y + 1] is Tree) && !(Game.Map[x, y + 1] is Water);
        }

        public bool DeadInConflict(ICell conflictedObject)
        {
            if (conflictedObject is Monster || conflictedObject is Barrier)
                Health--;
            return Health <= 0;
        }
    }
}