using System.Numerics;
using Raylib_cs;

namespace Tanks
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Program tanksGame = new Program();
            tanksGame.RunGame();
        }
        //tankkien aloitus paikat
        static Vector2 tank1Pos = new Vector2(900, 600);
        static Vector2 tank2Pos = new Vector2(100, 100);

        //tankkien värit
        static Color tankColor1 = Color.Orange;
        static Color tankColor2 = Color.Green;

        //tankit itse
        Tanks tank1 = new Tanks(tank1Pos, tankColor1);
        Tanks tank2 = new Tanks(tank2Pos, Color.Green);

        //seinät
        public List<Wall> walls = new List<Wall>();
        Wall wall1 = new Wall(100, 200, 300, 100);
        //ammukset
        public List<Bullet> bullets = new List<Bullet>();

        //pisteet
        int pisteet2 = 0;
        int pisteet1 = 0;
        Vector2 pistePos1;
        Vector2 pistePos2;

        public void RunGame()
        {
            Raylib.InitWindow(1000, 700, "Tanks");
            Raylib.SetTargetFPS(60);

            while (Raylib.WindowShouldClose() == false)
            {
                UpdateGame();
                DrawGame();
            }
        }

        private void UpdateGame()
        {
            //lue pelaajan syöte (liikkkuminen)
            tank1.Input(bullets, KeyboardKey.W, KeyboardKey.S, KeyboardKey.A, KeyboardKey.D, KeyboardKey.E);
            tank2.Input(bullets, KeyboardKey.Up, KeyboardKey.Down, KeyboardKey.Left, KeyboardKey.Right, KeyboardKey.RightControl);
            
            //pisteet
            pistePos1 = new Vector2(Raylib.GetScreenWidth() / 4 * 3, 50);
            pistePos2 = new Vector2(Raylib.GetScreenWidth() / 4, 50);


            //katso collion ja liikuta pelaaja
            foreach (Bullet b in bullets)
            { HandleBullet(b, tank1, tank2); }
            foreach (Bullet b in bullets) 
            { b.BulletPos = wall1.CheckCollidedBullet(b, b.BulletPos); }

            tank1.tankPos = wall1.CheckCollidedTank(tank1.rec, tank1.tankPos, tank1.TankDir);
            tank2.tankPos = wall1.CheckCollidedTank(tank2.rec, tank2.tankPos, tank2.TankDir);
        }

        private void HandleBullet(Bullet bullet, Tanks tank1, Tanks tank2)
        {
            if (bullet.Hit(tank1.rec))
            {
                ResetGame();
                //koska osu tank1, tank2 saa pisteen
                pisteet2++;
                return;
            }
            else if (bullet.Hit(tank2.rec))
            {
                ResetGame();
                //koska osu tank2, tank1 saa pisteen
                pisteet1++;
                return;
            }

            bullet.BulletMove();
            bullet.Draw();
        }

        /// <summary>
        /// resetoi pelin (ei pisteiä)
        /// </summary>
        public void ResetGame()
        {
            tank1.tankPos = tank1Pos;
            tank2.tankPos = tank2Pos;
            
        }

        private void DrawGame()
        {
            Raylib.BeginDrawing();
            //background
            Raylib.ClearBackground(Raylib.GetColor(0x270022ff));

            //foreground items
            tank1.Draw();
            tank2.Draw();

            wall1.Draw();

            //UI & text
            DrawPoint(pisteet1, pistePos1, tankColor1);
            DrawPoint(pisteet2, pistePos2, tankColor2);

            Raylib.EndDrawing();
        }
        //pirtää piste UI:n
        public void DrawPoint(int pisteet, Vector2 pPos, Color color)
        {
            Raylib.DrawText($"{pisteet}", (int)pPos.X, (int)pPos.Y, 30, color);
        }
    }
}
