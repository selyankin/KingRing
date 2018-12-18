using System;
using KingRing.Interfaces;

namespace KingRing.GameObjects
{
    public class PlayerPosition
    {
        public int X;
        public int Y;
    }

    public class Monster : ICell
    {
        public PlayerPosition FindDigger()
        {
            for (var i = 0; i < Game.Width; i++)
            for (var j = 0; j < Game.Height; j++)
            {
                if (Game.Map[i, j] is Player)
                {
                    return new PlayerPosition { X = i, Y = j };
                }
            }

            return null;
        }

        public bool FindObstacle(int x, int y)
        {
            return Game.Map[x, y] is Player || Game.Map[x, y] == null || Game.Map[x, y] is Gold;
        }

        public CreatureCommand Act(int x, int y)
        {
            var command = new CreatureCommand();
            var diggerPosition = FindDigger();

            if (diggerPosition == null)
                return command;

            var distance = Math.Sqrt(Math.Pow(diggerPosition.X - x, 2) + Math.Pow(diggerPosition.Y - y, 2));
            if (distance > 4)
                return command;

            if (y > 0 && FindObstacle(x, y - 1) && diggerPosition.Y < y)
                command.DeltaY = -1;
            else if (y < Game.Height - 1 && FindObstacle(x, y + 1) && diggerPosition.Y > y)
                command.DeltaY = 1;
            else if (x < Game.Width - 1 && FindObstacle(x + 1, y) && diggerPosition.X > x)
                command.DeltaX = 1;
            else if (x > 0 && FindObstacle(x - 1, y) && diggerPosition.X < x)
                command.DeltaX = -1;

            return command;
        }

        public bool DeadInConflict(ICell conflictedObject)
        {
            return conflictedObject is Player || conflictedObject is Water || conflictedObject is Barrier;
        }

        public string GetImageFileName()
        {
            return "monster.png";
        }

        public int GetDrawingPriority()
        {
            return 3;
        }
    }
}
