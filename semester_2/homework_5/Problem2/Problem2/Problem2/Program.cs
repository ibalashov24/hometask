namespace Problem2
{
    using System;
    using System.IO;

    internal class Program
    {
        public static void Main(string[] args)
        {
            StreamReader reader = new StreamReader("map.txt");
            var mapWidth = int.Parse(reader.ReadLine());
            var mapHeight = int.Parse(reader.ReadLine());
            var map = new Game.Map.GameMap(reader, mapWidth, mapHeight);

            var game = new Game.Game(map);

            var mainLoop = new Game.EventLoop();
            game.Register(mainLoop);

            mainLoop.Run();
        }
    }
}