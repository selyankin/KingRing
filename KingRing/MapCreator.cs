using System;
using System.Collections.Generic;
using System.Linq;
using KingRing.GameObjects;
using KingRing.Interfaces;

namespace KingRing
{
    public static class MapCreator
    {
        private static readonly Dictionary<string, Func<ICell>> Factory = new Dictionary<string, Func<ICell>>
        {
            ["Player"] = () => new Player(),
            ["Barrier"] = () => new Barrier(),
            ["Monster"] = () => new Monster(),
            ["Wall"] = () => new Wall(),
            ["Water"] = () => new Water(),
            ["Tree"] = () => new Tree()
        };

        public static ICell[,] CreateMap(string map, string separator = "\r\n")
        {
            var rows = map.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);

            if (rows.Select(z => z.Length).Distinct().Count() != 1)
                throw new Exception($"Wrong test map '{map}'");

            var result = new ICell[rows[0].Length, rows.Length];
            for (var x = 0; x < rows[0].Length; x++)
            for (var y = 0; y < rows.Length; y++)
                result[x, y] = CreateCreatureBySymbol(rows[y][x]);
            return result;
        }

        private static ICell CreateCreatureBySymbol(char symbol)
        {
            switch (symbol)
            {
                case 'P':
                    return Factory["Player"]();
                case 'B':
                    return Factory["Barrier"]();
                case 'M':
                    return Factory["Monster"]();
                case 'W':
                    return Factory["Wall"]();
                case 'O':
                    return Factory["Water"]();
                case 'T':
                    return Factory["Tree"]();
                case ' ':
                    return null;
                default:
                    throw new Exception($"wrong character for ICreature {symbol}");
            }
        }
    }
}