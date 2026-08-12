using System.Numerics;
using Raylib_cs;
using Screensaver;

class Program
{
    public static void Main()
    {
        Raylib.InitWindow(800, 600, "Starfield");
        Raylib.SetTargetFPS(60);

        //luo satunnaislukugeneraattori.
        Random rng = new Random();

        Color[] palette = new Color[] {
            Color.Red, 
            Color.Purple, 
            Color.Green, 
            Color.Blue, 
            Color.Pink,
            Color.Orange
        };

        //taulukko tähtiä, jossa on 400 paikkaa.
        //nämä on tähti luokan olioita.
        Star[] stars = new Star[300]; //käytetään new avainsanaa, jos varastoidaan enemmän kuin 1.
        //List<float> paikat = new List<float>(400);
        
        //for silmukka käy läpi kaikki taulukon luvut
        //for (enne; alussa; lopussa)
        for(int i = 0; i < stars.Length; i++)
        {
            stars[i] = new Star();
            //satunnain aloituspaikka (-20 - ikkunan leveys)
            stars[i].position.X = rng.Next(-20, Raylib.GetScreenWidth());
            stars[i].position.Y = rng.Next(-10, Raylib.GetScreenHeight());
            stars[i].size = rng.Next(1, 20);

            stars[i].color = palette[rng.Next(0, palette.Length)];
        }

        while(!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing(); //piirrä tämän jälkeen
            Raylib.ClearBackground(Color.Black);

                //käy koko posX taulun läpi.
                //kasvattaa jokaista lukua.
                for (int i = 0; i < stars.Length; i++)
                {
                    stars[i].position.X += stars[i].size * 10 * Raylib.GetFrameTime();
                    //tarkistaa meneekö posX yli näytön. jos menee, asettaa luvuksi -20
                    if (stars[i].position.X >= Raylib.GetScreenWidth())
                    {
                        stars[i].position.X = -20;
                    }
                    Raylib.DrawRectangle((int)stars[i].position.X, (int)stars[i].position.Y,
                        stars[i].size, stars[i].size, stars[i].color);
                }

            Raylib.EndDrawing(); //piirrää tätä ennen
        }
    }
}