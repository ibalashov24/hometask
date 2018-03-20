namespace Problem2.Tests
{
    using System;
    using System.IO;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    [ExcludeFromCodeCoverage]
    public class DifferentMapTests
    {
        public Game.Game IninializeGame(string mapFileName)
        {
            var inputStream = new StreamReader(mapFileName);

            var width = int.Parse(inputStream.ReadLine());
            var height = int.Parse(inputStream.ReadLine());
            var currentGameMap = new Game.Map.GameMap(inputStream, width, height);

            var currentGame = new Game.Game(currentGameMap);

            return currentGame;
        }

        [DataTestMethod]
        [DataRow("Map1")]
        [DataRow("Map2")]
        [DataRow("Map3")]
        [DataRow("Map4")]
        public void PerformMapTests(string inputFile)
        {
            var currentGame = IninializeGame("TestMaps/" + inputFile + ".in");

            var loop = new ManualEventCaller();
            currentGame.Register(loop);

            var commandStream = new StreamReader("TestMaps/" + inputFile + "Command.in");

            var commandString = commandStream.ReadLine();
            while (commandString != null)
            {
                var command = commandString.Split(' ');

                switch (command[0])
                {
                    case "right":
                        loop.MoveToRight();
                        break;
                    case "down":
                        loop.MoveToBottom();
                        break;
                    case "up":
                        loop.MoveToTop();
                        break;
                    case "left":
                        loop.MoveToLeft();
                        break;
                    default:
                        Assert.Fail("Invalid command file format");
                        break;
                }

                var newCoords = currentGame.GetPlayerPosition();
                var expectedCoords = (int.Parse(command[1]), int.Parse(command[2]));

                Assert.AreEqual(expectedCoords, newCoords);

                commandString = commandStream.ReadLine();
            }
        }

        [DataTestMethod]
        [DataRow("Map5", false, 99)]
        [DataRow("Map6", true, 99)]
        public void CheckScreenChangingOnBigMaps(
            string inputFile,
            bool moveUpDown,
            int stepCount)
        {
            var currentGame = IninializeGame($"TestMaps/{inputFile}.in");
            var loop = new ManualEventCaller();
            currentGame.Register(loop);

            for (int i = 0; i < stepCount; ++i)
            {
                if (moveUpDown)
                {
                    loop.MoveToBottom();
                }
                else
                {
                    loop.MoveToRight();
                }
            }

            var currentCoordinates = currentGame.GetPlayerPosition();
            Assert.AreEqual(
                moveUpDown ? (0, stepCount) : (stepCount, 0), currentCoordinates);

            for (int i = 0; i < stepCount; ++i)
            {
                if (moveUpDown)
                {
                    loop.MoveToTop();
                }
                else
                {
                    loop.MoveToLeft();
                }
            }

            currentCoordinates = currentGame.GetPlayerPosition();
            Assert.AreEqual((0, 0), currentCoordinates);
        }

        [TestMethod]
        [ExpectedException(typeof(Game.Map.Exception.InvalidCellTypeInMapException))]
        public void ReadingMapWithInvalidCellTypeShouldCauseException()
        {
            var currentGame = IninializeGame("TestMaps/MapWithInvalidCells.in");
        }
    }
}