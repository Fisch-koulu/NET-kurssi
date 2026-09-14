using Raylib_cs;
using System.Numerics;

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
        //muuttujat
        //ympyrä
        Vector2 ympy1 = new Vector2(300, 100);
        float rad1 = 20;
        Color color1 = Color.Pink;

        Vector2 ympy2 = new Vector2(400, 100);
        float rad2 = 20;
        Color color2 = Color.Blue;

        Vector2 suunta1 = new Vector2(1, 0);
        Vector2 suunta2 = new Vector2(-1, 0);
        float speed = 20;

        public void Run()
        {
            Raylib.InitWindow(600, 600, "Asteroids");

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
            ympy1 += suunta1 * speed * Raylib.GetFrameTime();
            ympy2 += suunta2 * speed * Raylib.GetFrameTime();
            if (Raylib.CheckCollisionCircles(ympy1, rad1, ympy2, rad2))
            {
                suunta1.X *= -1f;
                suunta2.X *= -1f;
            }
        }

        /// <summary>
        /// Pirtää pelin.
        /// </summary>
        public void Draw()
        {
            //aloita piirtäminen.
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);

            Raylib.DrawCircleV(ympy1, rad1, color1);
            Raylib.DrawCircleV(ympy2, rad2, color2);

            //lopeta piirtäminen.
            Raylib.EndDrawing();
        }
    }
}
