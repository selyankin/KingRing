using System.Windows.Forms;
using KingRing.Interfaces;

namespace KingRing
{
    public static class Game
    {
        public static ICell[,] Map;
        public static int Width => Map.GetLength(0);
        public static int Height => Map.GetLength(1);
        public static Keys KeyPressed;
        public static int Score;

        public static void CreateMap(string map)
        {
            Map = MapCreator.CreateMap(map);
        }
    }
}