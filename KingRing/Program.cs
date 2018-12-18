using System.IO;
using System.Windows.Forms;
using KingRing.Architecture;

namespace KingRing
{
    class Program
    {
        static void Main()
        {
            var map = File.ReadAllText("D:\\Michael\\Desktop\\Programming\\KingRing\\KingRing\\Maps\\BigMap.txt");
            Game.CreateMap(map);
            Application.Run(new KingRingWindow());
        }
    }
}