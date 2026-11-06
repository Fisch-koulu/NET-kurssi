using Raylib_cs;
using System.Numerics;
using Ruptus_kirjasto;
using System.Diagnostics;

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

        //pelaaja
        Player player;
        Texture2D texture;

        public void Run()
        {
            Raylib.InitWindow(600, 600, "Asteroids");
            //tee asteroidit
            for (int i = 0; i < 3;  i++)
            {
                asteroidit.Add(CreateAsteroids());
            }
            //pelaaja
            texture = Raylib.LoadTexture("Images/ship_blue.png");
            player = new Player(texture, Raylib.GetScreenCenter(), 100f, 4f, KeyboardKey.K);

            while (Raylib.WindowShouldClose() == false)
            {
                Update();
                Draw();
            }
            //vapauta kuva
            Raylib.UnloadTexture(texture);
            //sulje ikkuna
            Raylib.WindowShouldClose();
        }

        public void Update()
        {
            //liikuttaa asteroideja
            for (int i = 0; i < asteroidit.Count; i++)
            {
                asteroidit[i].TransformMove();
            }
            //liikuta pelaajaa
            player.Update();
            
            player.TurnMove();
            player.ShootUpdate();

            //ammus
            for (int i = 0; i < player.bullets.Count; ++i)
            {
                player.bullets[i].BulletMove();
            }
            CheckCollision();
        }

        /// <summary>
        /// Tekee uuden asteroidin.
        /// </summary>
        public AsteroidRock CreateAsteroids(float size = 30f)
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
                paikka,         //paikka
                size,    //ympyrän säde
                randSuunta,     //suunta
                rand.Next(20, 40)); //nopeus
        }

        /// <summary>
        /// 
        /// </summary>
        public void CheckCollision()
        {
            //tarkistaa pelaajan ja asteroidin törmäys
            /*for (int a = asteroidit.Count - 1;  a >= 0; --a)
            {
                if (Raylib.CheckCollisionCircles(asteroidit[a].Position, 30f, player.POSITION, 20f)) 
                {
                    GameOver();
                }
            }*/
            //tarkista viimeisin ammus
            for (int b = player.bullets.Count - 1; b >= 0; b--)
            {
                //katso kaikki asteroidit läpi
                for (int a = asteroidit.Count - 1; a >= 0; a--)
                {
                    Bullet checkBullet = player.bullets[b];
                    AsteroidRock checkAsteroid = asteroidit[a];

                    if (Raylib.CheckCollisionCircles(checkBullet.Position, 2f, checkAsteroid.Position, 30f))
                    {
                        //poista ammus
                        player.bullets.RemoveAt(b);

                        //luo 2 uuttaa asteroidia
                        asteroidit.Add(CreateAsteroids());

                        //skippaa loput asteroidit
                        break;
                    }
                }
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

            //piirrä peli
            for (int i = 0; i < asteroidit.Count; i++)
            {
                asteroidit[i].Draw();
            }
            player.DrawPlayer();
            for (int i = 0; i < player.bullets.Count; ++i)
            {
                player.bullets[i].BulletDraw();
            }

            //lopeta piirtäminen.
            Raylib.EndDrawing();
        }
    }
}
