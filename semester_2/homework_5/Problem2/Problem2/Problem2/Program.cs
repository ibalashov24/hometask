namespace Problem2
{
    using System;
    using System.IO;

    internal class Program
    {
        public static void Main(string[] args)
        {
            int mapWidth;
            int mapHeight;
            Game.Map.IMap map;

            using (StreamReader reader = new StreamReader("map.txt"))
            {
                mapWidth = int.Parse(reader.ReadLine());
                mapHeight = int.Parse(reader.ReadLine());
                map = new Game.Map.GameMap(reader, mapWidth, mapHeight);
            }

            var game = new Game.Game(map);

            var mainLoop = new Game.GameEventLoop();
            game.Register(mainLoop);

            mainLoop.Run();
        }
    }
}