using System.Numerics;
using Raylib_cs;

namespace Ruptus_kirjasto
{
    public class WrapAround
    {
        Vector2 position;
        Rectangle drawSource;
        Vector2 size;
        Texture2D texture;

        public Vector2 Position { get => position; set => position = value; }

        public WrapAround(Vector2 position, Texture2D t) 
        {
            drawSource = new Rectangle(0, 0, t.Width, t.Height);
            size = new Vector2(drawSource.Width/2, drawSource.Height/2);
        }

        /// <summary>
        /// Tarkistaa, meneekö objekti yli reunan.
        /// </summary>
        /// <returns></returns>
        public bool OutOfBoundsCheck()
        {
            //tarkista meneekö objekti näytöstä yli/pois
            //X-akseli
            if (position.X < 0)
            {
                position.X = Raylib.GetRenderWidth();
                return true;
            }
            else if (position.X > Raylib.GetScreenWidth())
            {
                position.X = 0;
                return true;
            }
            //Y-akseli
            if (position.Y < 0)
            {
                position.Y = Raylib.GetRenderHeight();
                return true;
            }
            else if (position.Y > Raylib.GetScreenHeight())
            {
                position.Y = 0;
                return true;
            }
            //jos ei mee, palauta false
                return false;
        }

        /// <summary>
        /// Piirtää kuvan toiselle puolelle, kun menee yli reunan.
        /// </summary>
        /// <returns></returns>
        public bool OutOfBoundsDraw(Sprite sprite, Vector2 position, float rotationRadians)
        {
            //tarkista meneekö kuva yli näytön.
            //X-akseli
            if (position.X - size.X < 0)
            {
                //MirrorDraw(Raylib.GetScreenWidth(), 0);
                sprite.DrawRotated(position, rotationRadians);
                return true;
            }
            else if (position.X + size.X > Raylib.GetScreenWidth())
            {
                //MirrorDraw(-Raylib.GetScreenWidth(), 0);
                return true;
            }
            //Y-akseli
            if (position.Y - size.Y < 0)
            {
                //MirrorDraw(0, Raylib.GetScreenHeight());
                return true;
            }
            else if (position.Y + size.Y > Raylib.GetScreenHeight())
            {
                //MirrorDraw(0, -Raylib.GetScreenHeight());
                return true;
            }
            //jos ei mee, palauta false
            return false;
        }
    }
}
