using System;
using Final.Game.Directing;
using Final.Game.Services;

namespace Final
{
    public class Program
    {
        static void Main(string[] args)
        {
            Director director = new Director(SceneManager.VideoService);
            director.StartGame();
        }
    }
}
