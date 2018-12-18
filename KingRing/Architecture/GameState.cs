using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using KingRing.Interfaces;

namespace KingRing.Architecture
{
    public class GameState
    {
        public const int ElementSize = 32;
        public List<CreatureAnimation> Animations = new List<CreatureAnimation>();

        public void BeginAct()
        {
            Animations.Clear();
            for (var x = 0; x < Game.Width; x++)
                for (var y = 0; y < Game.Height; y++)
                {
                    var creature = Game.Map[x, y];
                    if (creature == null) continue;
                    var command = creature.Act(x, y);

                    if (x + command.DeltaX < 0 || x + command.DeltaX >= Game.Width || y + command.DeltaY < 0 ||
                        y + command.DeltaY >= Game.Height)
                        throw new Exception($"The object {creature.GetType()} falls out of the game field");

                    Animations.Add(
                        new CreatureAnimation
                        {
                            Command = command,
                            Creature = creature,
                            Location = new Point(x * ElementSize, y * ElementSize),
                            TargetLogicalLocation = new Point(x + command.DeltaX, y + command.DeltaY)
                        });
                }
            Animations = Animations.OrderByDescending(z => z.Creature.GetDrawingPriority()).ToList();
        }

        public void EndAct()
        {
            var creaturesPerLocation = GetCandidatesPerLocation();
            for (var x = 0; x < Game.Width; x++)
            for (var y = 0; y < Game.Height; y++)
            {
                Game.Map[x, y] = SelectWinnerCandidatePerLocation(creaturesPerLocation, x, y);
            }
        }

        private static ICell SelectWinnerCandidatePerLocation(List<ICell>[,] creatures, int x, int y)
        {
            var candidates = creatures[x, y];
            var aliveCandidates = candidates.ToList();

            foreach (var candidate in candidates)
                foreach (var rival in candidates)
                    if (rival != candidate && candidate.DeadInConflict(rival))
                        aliveCandidates.Remove(candidate);

            if (aliveCandidates.Count > 1)
                throw new Exception(
                    $"Creatures {aliveCandidates[0].GetType().Name} and {aliveCandidates[1].GetType().Name} claimed the same map cell");

            return aliveCandidates.FirstOrDefault();
        }

        private List<ICell>[,] GetCandidatesPerLocation()
        {
            var creatures = new List<ICell>[Game.Width, Game.Height];
            for (var x = 0; x < Game.Width; x++)
                for (var y = 0; y < Game.Height; y++)
                    creatures[x, y] = new List<ICell>();

            foreach (var e in Animations)
            {
                var x = e.TargetLogicalLocation.X;
                var y = e.TargetLogicalLocation.Y;
                creatures[x, y].Add(e.Creature);
            }

            return creatures;
        }
    }
}