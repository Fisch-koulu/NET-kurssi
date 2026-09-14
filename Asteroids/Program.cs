using Raylib_cs;
using System.Numerics;
using Ruptus_kirjasto;

namespace Asteroids
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //niin paljon listoja...
            Program asteroids = new Program();
            asteroids.Run();
        }
        //Muuttujat:
        //asteroid
        Vector2 ympy1 = new Vector2(300, 100);
        float rad1 = 20;
        Color color1 = Color.Pink;
        Vector2 suunta1 = new Vector2(1, 0);
        float speed = 20;

        AsteroidRock as1;
        List<AsteroidRock> asteroidit = new List<AsteroidRock>();
        public void Run()
        {
            Raylib.InitWindow(600, 600, "Asteroids");
            as1 = new AsteroidRock(ympy1, rad1, suunta1, speed);

            while (Raylib.WindowShouldClose() == false)
            {
                Update();
                Draw();
            }
            //sulje ikkuna
            Raylib.WindowShouldClose();
        }

        public void Update()
        {
            
        }

        /// <summary>
        /// Tekee uuden asteroidin.
        /// </summary>
        public void CreateAsteroids()
        {

        }



        /// <summary>
        /// Pirtää pelin.
        /// </summary>
        public void Draw()
        {
            //aloita piirtäminen.
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);

            //piirrä peli


            //lopeta piirtäminen.
            Raylib.EndDrawing();
        }
    }
}
