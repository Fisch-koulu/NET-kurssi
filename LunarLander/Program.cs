using Raylib_cs;
using System.Numerics;
using Ruptus_kirjasto;

namespace LunarLander
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Program lunargame = new Program();
            lunargame.Run();
        }
        //pelin muuttujat
        //ship
        Texture2D shipTexture;
        Vector2 fSize = new Vector2(40,50);

        //liikkuminen
        Vector2 shipPos; //paikka
        Vector2 shipVelocity; //nopeus
        Vector2 engineForce;

        Vector2 gravityForce;

        //alusta
        Vector2 landPos;
        Vector2 landSize;

        //polttoaine
        float fuel = 20;
        bool engineOn = false;

        //äänet
        Sound engineSound;
        Music spaceMusic;

        //peli voitettu?
        bool gameWin = false;
        bool gameLost = false;

        //reset
        Vector2 startPos;
        Vector2 startVel;
        float startFuel;

        //pelin funktiot
        public void Run()
        {
            //ikkunan tekeminen
            Raylib.InitWindow(600, 500, "lunar lander"); //kuinka iso ikkuna on
            Raylib.SetTargetFPS(60); //pelin fps

            //äänet päälle
            Raylib.InitAudioDevice();
            engineSound = Raylib.LoadSound("rocket_engine.mp3");
            spaceMusic = Raylib.LoadMusicStream("space_waves.mp3");
            //soita tausta musiikki.
            Raylib.PlayMusicStream(spaceMusic);
            Raylib.SetMusicVolume(spaceMusic, 50f);
            Raylib.SetSoundVolume(engineSound, 50f);

            //ship tai alus
            //lataa kuva ennen pääsilmukkaa tai kuva ladataan turhaan monta kertaa ja ottaa paljon muistia
            shipTexture = Raylib.LoadTexture("ship.png");

            //liikkumiseen aloitukset
            shipPos = new Vector2(Raylib.GetScreenWidth() / 2, shipTexture.Height);
            shipVelocity = Vector2.Zero;
            engineForce = new Vector2(0, -90);
            gravityForce = new Vector2(0, 60);

            //Reset arvioit, jotta voi aloittaa pelin uudelleen ilman pelistä poistumista
            startPos = shipPos;
            startVel = shipVelocity;
            startFuel = fuel;

            //alusta
            landPos = new Vector2(Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight());
            landSize = new Vector2(200, 40);

            while (Raylib.WindowShouldClose() == false)
            {
                Update();
                Draw();
            }
            //sulkee ikkunan
            Raylib.WindowShouldClose();
            //Vapautetaan ladatut tiedostot ja suljetaan äänilaite.
            Raylib.UnloadSound(engineSound);
            Raylib.UnloadMusicStream(spaceMusic);
            Raylib.CloseAudioDevice();
        }

        public void Update()
        {
            //soita seuraava pätkä musiikista.
            Raylib.UpdateMusicStream(spaceMusic);

            if (shipPos.Y >= landPos.Y - landSize.Y / 2 - shipTexture.Height / 2)
            {
                if (shipVelocity.Y <= 40)
                {
                    gameWin = true;
                } else
                {
                    gameLost = true;
                }
                engineOn = false;
                shipPos.Y = landPos.Y - landSize.Y / 2 - shipTexture.Height / 2;

            } else 
            {
                //alas liikkuminen.
                Vector2 acceloration = gravityForce;

                //jos painetaan nappia ja polttoaine ei ole loppu, alus voi nousta.
                if (Raylib.IsKeyDown(KeyboardKey.M) && fuel > 0) 
                {
                    engineOn = true;

                    //ylös liikkuminen.
                    acceloration += engineForce;
                    //hiljalleen poistetaan polttoainetta.
                    fuel -= 5 * Raylib.GetFrameTime();

                    //katso, jos ääniefekti soi, ettei loppaa alkua.
                    if (!Raylib.IsSoundPlaying(engineSound))
                    {
                        //soita ääniefekti.
                        Raylib.PlaySound(engineSound);
                    }

                } else
                {
                    engineOn = false;
                    //lopeta ääniefektin soittaminen.
                    Raylib.StopSound(engineSound);
                }
            
                shipVelocity += acceloration * Raylib.GetFrameTime();
                shipPos += shipVelocity * Raylib.GetFrameTime();
            }

            if (Raylib.IsKeyDown(KeyboardKey.R))
            {
                Reset();
            }

            Console.WriteLine(shipVelocity);
        }

        /// <summary>
        /// piirtää pelin.
        /// </summary>
        public void Draw()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);

            //pelin piirtäminen
            //alusta
            Drawing.DrawCentered(landPos, landSize, Color.Gray);
            //engine
            if (engineOn)
            {
                Raylib.DrawRectangleV(shipPos + new Vector2(-20, shipTexture.Height / 2), fSize, Color.Yellow);
            }
            //alus
            Drawing.TextureCentered(shipTexture, shipPos);

            //ui
            Raylib.DrawText("Fuel" + fuel, 10, 10, 20, Color.White);
            if (gameWin)
            {
                Raylib.DrawText("You won. Press R to restart.", 100, 100, 20, Color.White);
            } 
            if (gameLost)
            {
                Raylib.DrawText("You lost. Press R to restart.", 100, 100, 20, Color.White);
            }

            //lopettaa piirtämisen.
            Raylib.EndDrawing();
        }

        /// <summary>
        /// laittaa pelin alkutilanteeseen.
        /// </summary>
        public void Reset()
        {
            //laittaa pelin voitoin tai hävion tilat pois päältä.
            gameLost = false; gameWin = false;
            //resetoi aluksen.
            shipPos = startPos;
            shipVelocity = startVel;
            fuel = startFuel;
        }
    }
}
