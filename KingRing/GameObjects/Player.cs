using System.Windows.Forms;
using KingRing.Interfaces;

namespace KingRing.GameObjects
{
    public class Player : ICell
    {
        public static int Health { get; private set; } = 3;

        public CreatureCommand Act(int x, int y)
        {
            var command = new CreatureCommand();
            switch (Game.KeyPressed)
            {
                case Keys.S when y < Game.Height - 1 && CheckNearCell(x, y + 1):
                    command.DeltaY = 1;
                    break;
                case Keys.W when y > 0 && CheckNearCell(x, y - 1):
                    command.DeltaY = -1;
                    break;
                case Keys.A when x > 0 && CheckNearCell(x - 1, y):
                    command.DeltaX = -1;
                    break;
                case Keys.D when x < Game.Width - 1 && CheckNearCell(x + 1, y):
                    command.DeltaX = 1;
                    break;
            }

            return command;
        }

        private static bool CheckNearCell(int x, int y)
        {
            if (!(Game.Map[x, y] is Barrier))
                return !(Game.Map[x, y] is Wall) && !(Game.Map[x, y] is Tree) && !(Game.Map[x, y] is Water);
            Health--;
            return false;
        }

        public bool DeadInConflict(ICell conflictedObject)
        {
            if (conflictedObject is Monster || conflictedObject is Barrier)
                Health--;
            return Health <= 0;
        }

        public string GetImageFileName()
        {
            return "player.png";
        }

        public int GetDrawingPriority()
        {
            return 2;
        }

        public static void RestoreHealth()
        {
            Health++;
        }
    }
}