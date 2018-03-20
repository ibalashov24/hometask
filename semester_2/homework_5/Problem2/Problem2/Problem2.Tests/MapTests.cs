namespace Problem2.Tests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Threading;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    [ExcludeFromCodeCoverage]
    public class MapTests
    {
        private static Game.Map.IMap testMap;
        private static int mapHeight;
        private static int mapWidth;

        [ClassInitialize]
        public static void InitClass(TestContext context)
        {
            var inputStream = new StreamReader("SimpleMap.in");

            MapTests.mapWidth = int.Parse(inputStream.ReadLine());
            MapTests.mapHeight = int.Parse(inputStream.ReadLine());

            MapTests.testMap = new Game.Map.GameMap(inputStream, mapWidth, mapHeight);
        }
        
        [TestMethod]
        public void WidthAndHeightShouldBeReadCorrectly()
        {
            Assert.AreEqual(
                MapTests.mapHeight,
                MapTests.testMap.Height,
                "Different heights!!!");
            Assert.AreEqual(
                MapTests.mapWidth,
                MapTests.testMap.Width,
                "Different width!!!");
        }

        [TestMethod]
        public void MapClassShouldRememberBoardCellCorrectly()
        {
            var checkStream = new StreamReader("SimpleMapCheck.in");

            for (int i = 0; i < MapTests.mapHeight; ++i)
            {
                var currentString = checkStream.ReadLine().TrimEnd(
                    new char[] { ' ', '\n'});

                var currentStringInMap = "";
                for (int j = 0; j < MapTests.mapWidth; ++j)
                {
                    switch (MapTests.testMap.GetCell(i, j))
                    {
                        case Game.Map.CellType.Border:
                            currentStringInMap += '#';
                            break;
                        case Game.Map.CellType.Free:
                            currentStringInMap += '.';
                            break;
                        default:
                            Assert.Fail();
                            break;
                    }
                }

                Assert.AreEqual(currentString, currentStringInMap);
            }
        }

        [TestMethod]
        public void MapShouldProceedNonExistentCellsCorrectly()
        {
            Assert.AreEqual(Game.Map.CellType.NotExist, MapTests.testMap.GetCell(-1, -1));
            Assert.AreEqual(Game.Map.CellType.NotExist, MapTests.testMap.GetCell(-1, 0));
            Assert.AreEqual(Game.Map.CellType.NotExist, MapTests.testMap.GetCell(0, -1));
            Assert.AreEqual(Game.Map.CellType.NotExist, MapTests.testMap.GetCell(-10000, -10000));
            Assert.AreEqual(Game.Map.CellType.NotExist, MapTests.testMap.GetCell(MapTests.mapHeight, MapTests.mapWidth));
            Assert.AreEqual(Game.Map.CellType.NotExist, MapTests.testMap.GetCell(
                100 * MapTests.mapHeight, 
                10000 * MapTests.mapWidth));
            Assert.AreEqual(Game.Map.CellType.NotExist, MapTests.testMap.GetCell(MapTests.mapHeight, MapTests.mapWidth - 1));
            Assert.AreEqual(Game.Map.CellType.NotExist, MapTests.testMap.GetCell(MapTests.mapHeight - 1, MapTests.mapWidth));
        }

        [TestMethod]
        public void MapShouldProceedEmptyMapCorrectly()
        {
            var stream = StreamReader.Null;
            var testEmptyMap = new Game.Map.GameMap(stream, 0, 0);

            Assert.AreEqual(0, testEmptyMap.Width);
            Assert.AreEqual(0, testEmptyMap.Height);
            Assert.AreEqual(
                Game.Map.CellType.NotExist, testEmptyMap.GetCell(0, 0));
        }
    }
}
