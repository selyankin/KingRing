using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using KingRing.GameObjects;
using NUnit.Framework;
using KingRing.Interfaces;

namespace KingRing.Tests
{
    [TestFixture]
    public class PlayerTests
    {
        [Test]
        public void A()
        {
            var map = File.ReadAllText("D:\\Michael\\Desktop\\Programming\\KingRing\\KingRing\\Maps\\MapHaveAllObjects.txt");

            Game.KeyPressed = Keys.Down;
            var createdMap = MapCreator.CreateMap(map);

            var player = (Player) createdMap[0, 0];
            player.Act(0, 0);
            Assert.True(Player.Health == 3, "Test 1");
            Assert.True(createdMap[0, 0] is Player, "Test 2");
            Assert.True(createdMap[0, 2] is Monster, "Test 3");
        }
    }
}