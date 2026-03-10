using System.Numerics;
using Raylib_cs;

namespace Pong
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Program pong = new Program();
            pong.RunGame();
        }

        //Random rng

        //Paikat pelaajille 2kpl ja palolle
        Vector2 player1;    //Vasen reuna
        Vector2 player2;    //Oikea reuna
        Vector2 ball;   // Keskelle
        Vector2 playerSize; //Pelaajille samat koot

        //värit
        Color fieldColor;   //Pelikentän väri
        Color playerColor1;  //pelaajan väri
        Color playerColor2;  //pelaajan väri
        Color shadowColor;  //varjon väri
        Color fieldColorDark; //shakkilautan neliöt

        //pisteet
        int playerPisteet1;
        int playerPisteet2;
        

        float speed = 200.0f; //pallo ja player on saman nopeita
        Vector2 suunta = new Vector2(1, 1);


        void RunGame()
        {
            Raylib.InitWindow(800, 600, "PONG");
            Raylib.SetTargetFPS(60);
            

            fieldColor = Raylib.GetColor(0x17001dff);
            shadowColor = Raylib.GetColor(0x09010dff);
            fieldColorDark = Raylib.GetColor(0x270022ff);

            playerColor1 = Raylib.GetColor(0x0ce6f2ff);
            playerColor2 = Raylib.GetColor(0xff0546ff);

            //pelaajan koko
            playerSize = new Vector2(20, Raylib.GetScreenHeight() / 4);
            //missä on
            int fromWall = 20;

            player1 = new Vector2(fromWall, //kuinka kaukana seinästä
                Raylib.GetScreenHeight() / 2 - playerSize.Y / 2);

            player2 = new Vector2(Raylib.GetScreenWidth() - fromWall - playerSize.X, //kuinka kaukana seinästä, mutta toiselle puolelle
                Raylib.GetScreenHeight() / 2 - playerSize.Y / 2);

            ball = new Vector2(Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2);

            
            while (Raylib.WindowShouldClose() == false)
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(fieldColor);
                DrawField();    //piirrä kenttä ennen pelaajia joten pelaajat on päällä

                //pelaaja likutetaan
                if (Raylib.IsKeyDown(KeyboardKey.Up)) { player2.Y -= 5 + speed * Raylib.GetFrameTime(); }
                else if (Raylib.IsKeyDown(KeyboardKey.Down)) { player2.Y += 5 + speed * Raylib.GetFrameTime(); }

                if (Raylib.IsKeyDown(KeyboardKey.W)) { player1.Y -= 5 + speed * Raylib.GetFrameTime(); }
                else if (Raylib.IsKeyDown(KeyboardKey.S)) { player1.Y += 5 + speed * Raylib.GetFrameTime(); }

                PlayerOutOfBoundsCheck(ref player2);
                PlayerOutOfBoundsCheck(ref player1);

                //piirrää nyt pelaajat ja pallo
                DrawPlayer(player1, playerColor1);
                DrawPlayer(player2, playerColor2);
                ball = DrawBall(ball, 15);

                Raylib.EndDrawing();
            }
            Raylib.CloseWindow();
        }

        //tämä on ref. se toimii samalla tavalla, kuin return, minus yksi koodi rivi (return)
        //tämä koodi checkkaa että pelaaja ei mene näkyvyän screenin yli
        private void PlayerOutOfBoundsCheck(ref Vector2 player)
        {
            if (player.Y < 0)
            {
                player.Y = 0;
            }
            if (player.Y + playerSize.Y > Raylib.GetScreenHeight())
            {
                player.Y = Raylib.GetScreenHeight() - playerSize.Y;
            }
            
        }

        //piirtää koko kentän ja pisteet
        void DrawField()
        {
            //piirrä ruudukko tai shakkilauta
            Vector2 squareSize = new Vector2(Raylib.GetScreenHeight() / 10, Raylib.GetScreenHeight() / 10);
            for (int row = 0; row < Raylib.GetScreenHeight()/squareSize.Y; row++)
            {
                for (int column = 0; column < Raylib.GetRenderWidth()/squareSize.X; column++)
                {
                    if ((column + row) % 2 == 0)
                    {
                        Raylib.DrawRectangleV(new Vector2(
                        squareSize.X * column,  //X sarakkeen mukaan
                        squareSize.Y * row),    //Y rivin mukaan
                        squareSize, 
                        fieldColorDark);
                    }
                }
            }

            //piirrä keskiviiva, keskellä menee ylhäälta alas
            Vector2 lineStart = new Vector2(Raylib.GetScreenWidth() / 2, 0);
            Vector2 lineSize = new Vector2(10, 40);
            for (int i = 0; i < Raylib.GetScreenHeight() / lineSize.Y; i++)
            {
                if (i % 2 == 0) //piirrä joka toinen viiva
                {
                    Raylib.DrawRectangleV(new Vector2(lineStart.X - lineSize.X/2,
                        lineStart.Y + lineSize.Y * i), //paikka
                        lineSize, //koko
                        Color.White);
                }
            }

            //missä pisteet ovat
            Raylib.DrawText($"{playerPisteet1}", Raylib.GetScreenWidth()/3, 20, 60, playerColor1);
            Raylib.DrawText($"{playerPisteet2}", Raylib.GetScreenWidth()-310, 20, 60, playerColor2);
        }

        void DrawPlayer(Vector2 playerPos, Color playerColor)
        {
            Vector2 shadowOffset = new Vector2(6, 6);
            //piirrä varjo
            Raylib.DrawRectangleV(playerPos + shadowOffset, playerSize, shadowColor);
            //piirrä pelaaja
            Raylib.DrawRectangleV(playerPos, playerSize, playerColor);
        }

        //nyt drawball palauttaa vector2 eli pallo liikku ilman että pitää antaa "ball" vaan metodi voi käyttää "pallo"
        Vector2 DrawBall(Vector2 pallo, float size) 
        {
            Raylib.DrawCircleV(pallo, size, Color.White);
            pallo = pallo + suunta * speed * Raylib.GetFrameTime();
            if (Raylib.CheckCollisionPointRec(pallo, new Rectangle(player2, playerSize))) 
            {
                float howMuchInside = MathF.Abs(player2.X - pallo.X);
                suunta.X *= -1;
                pallo += suunta * howMuchInside * 2f;
            }
            if (Raylib.CheckCollisionPointRec(pallo, new Rectangle(player1, playerSize))) 
            {
                float howMuchInside = MathF.Abs(player1.X + playerSize.X - pallo.X);
                suunta.X *= -1;
                pallo += suunta * howMuchInside * 2f;
            }

            if (pallo.X + size < 0) 
            { pallo = Raylib.GetScreenCenter(); playerPisteet2++; }
            if (pallo.X - size > Raylib.GetScreenWidth()) 
            { pallo = Raylib.GetScreenCenter(); playerPisteet1++; }

            if (pallo.Y - size < 0 || pallo.Y + size > Raylib.GetScreenHeight()) { suunta.Y = suunta.Y * -1f; }
            
            return pallo;
        }

    }
}
