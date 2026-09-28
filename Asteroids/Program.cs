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
        //asteroidit
        List<AsteroidRock> asteroidit = new List<AsteroidRock>();
        public void Run()
        {
            Raylib.InitWindow(600, 600, "Asteroids");
            for (int i = 0; i < 3;  i++)
            {
                asteroidit.Add(CreateAsteroids());
            }

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
            for (int i = 0; i < asteroidit.Count; i++)
            {
                asteroidit[i].TransformMove();
            }
        }

        /// <summary>
        /// Tekee uuden asteroidin.
        /// </summary>
        public AsteroidRock CreateAsteroids()
        {
            //random luku
            Random rand = new Random();
            //sunnan arpominen
            Vector2 randSuunta = new Vector2(
                rand.NextSingle() * 2f - 1f,
                rand.NextSingle() * 2f - 1f);
            randSuunta = Vector2.Normalize(randSuunta);
            //paikan arpominen
            Vector2 paikka = new Vector2();
            paikka.X = rand.Next(Raylib.GetScreenWidth());
            paikka.Y = rand.Next(Raylib.GetScreenHeight());

            //tee Asteroidi
            return new AsteroidRock(
                paikka,     //paikka
                30f,        //ympyrän säde
                randSuunta, //suunta
                (float)(rand.Next(20, 40))); //nopeus
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
            for (int i = 0; i < asteroidit.Count; i++)
            {
                asteroidit[i].Draw();
            }

            //lopeta piirtäminen.
            Raylib.EndDrawing();
        }
    }
}
