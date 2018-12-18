using System;
using System.IO;
using System.Linq;
using KingRing.GameObjects;
using NUnit.Framework;
using KingRing.Interfaces;

namespace KingRing.Tests
{
    [TestFixture]
    public class MapCreatorTest
    {
        private readonly ICell[,] map = MapCreator.CreateMap(File.ReadAllText("D:\\Michael\\Desktop\\Programming\\KingRing\\KingRing\\Maps\\MapHaveAllObjects.txt"));

        [Test]
        public void MapHaveEmptyCells()
        {
            Assert.True(map[0, 1] is null, "Test 1");
            Assert.True(map[1, 1] is null, "Test 2");
            Assert.True(map[3, 3] is null, "Test 3");
        }

        [Test]
        public void MapHavePlayer()
        {
            Assert.True(map[0, 0] is Player, "Test 1");
        }

        [Test]
        public void MapHaveWater()
        {
            Assert.True(map[2, 1] is Water, "Test 1");
            Assert.True(map[3, 1] is Water, "Test 2");
        }

        [Test]
        public void MapHaveTree()
        {
            Assert.True(map[4, 0] is Tree, "Test 1");
            Assert.True(map[3, 2] is Tree, "Test 2");
        }

        [Test]
        public void MapHaveBarrier()
        {
            Assert.True(map[2, 2] is Barrier, "Test 1");
        }

        [Test]
        public void MapHaveWall()
        {
            Assert.True(map[0, 4] is Wall, "Test 1");
            Assert.True(map[3, 0] is Wall, "Test 2");
        }

        [Test]
        public void MapHaveMonsters()
        {
            Assert.True(map[0, 2] is Monster, "Test 1");
            Assert.True(map[4, 3] is Monster, "Test 2");
        }

        [Test]
        public void MapIsNotSquare()
        {
            var wrongMap = @"
P  T
TWW";

            Assert.Throws<Exception>(() => MapCreator.CreateMap(wrongMap));
        }

        [Test]
        public void MapHaveWrongSymbols()
        {
            var wrongMap = @"
XXXX
YYYY";

            Assert.Throws<Exception>(() => MapCreator.CreateMap(wrongMap));
        }
    }
}