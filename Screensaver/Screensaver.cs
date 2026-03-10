using System.Numerics;
using Raylib_cs;

namespace Screensaver
{
    internal class Screensaver
    {
        static void Main(string[] args)
        {
            int screenWidth = 800;
            int screenHeight = 800;

            Raylib.InitWindow(screenWidth, screenHeight, "Screensaverrr");
            Raylib.SetTargetFPS(30);

            //Vektorit
            Vector2 a = new Vector2(Raylib.GetScreenWidth() / 2, 40);
            Vector2 b = new Vector2(40, Raylib.GetScreenHeight() / 2);
            Vector2 c = new Vector2(Raylib.GetScreenWidth() - 40, Raylib.GetScreenHeight() / 4);

            //Suuntavektorit
            Vector2 dirA = new Vector2(40, 40);
            Vector2 dirB = new Vector2(40, -40);
            Vector2 dirC = new Vector2(-40, 40);

            //nopeaus
            float speed = 1;

            while (Raylib.WindowShouldClose() == false)
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.Black);

                //viivojen piirto
                Raylib.DrawLineV(a, b, Color.Green);
                Raylib.DrawLineV(b, c, Color.Yellow);
                Raylib.DrawLineV(c, a, Color.SkyBlue);

                //Vektorit liikkuvat
                a = a + dirA * speed * Raylib.GetFrameTime();
                b = b + dirB * speed * Raylib.GetFrameTime();
                c = c + dirC * speed * Raylib.GetFrameTime();

                //Vektorit ei voi mennä yli
                //X
                if (a.X < 0 || a.X > screenWidth) { dirA.X = dirA.X * -1f; }
                if (b.X < 0 || b.X > screenWidth) { dirB.X = dirB.X * -1f; }
                if (c.X < 0 || c.X > screenWidth) { dirC.X = dirC.X * -1f; }
                //Y
                if (a.Y < 0 || a.Y > screenHeight) { dirA.Y = dirA.Y * -1f; }
                if (b.Y < 0 || b.Y > screenHeight) { dirB.Y = dirB.Y * -1f; }
                if (c.Y < 0 || c.Y > screenHeight) { dirC.Y = dirC.Y * -1f; }

                Raylib.EndDrawing();
            }
        }
    }
}
