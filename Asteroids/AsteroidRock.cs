using Raylib_cs;
using System.Numerics;
using Ruptus_kirjasto;

namespace Asteroids
{
    internal class AsteroidRock
    {
        //mitä asteroid tarvitsee:
        Vector2 position;
        Vector2 direction;
        float radius;
        float speed;

        public AsteroidRock(Vector2 position, float radius, Vector2 direction, float speed)
        {
            this.position = position;
            this.radius = radius;
            this.direction = direction;
            this.speed = speed;
        }

        public void TransformMove()
        {
            OutOfBoundsCheck();
            OutOfBoundsDraw();
            position += direction * speed * Raylib.GetFrameTime();
        }

        public void Draw()
        {
            Raylib.DrawCircleV(position, radius, Color.Blue);
        }
        public void MirrorDraw(int x, int y)
        {
            Raylib.DrawCircleV(position + new Vector2(x,y), radius, Color.Red);
        }

        public bool OutOfBoundsCheck()
        {
            //tarkista meneekö pallo näytöstä pois
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

        public bool OutOfBoundsDraw()
        {
            //tarkista meneekö pallo näytöstä pois
            //X-akseli
            if (position.X - radius < 0) 
            {
                MirrorDraw(Raylib.GetScreenWidth(),0);
                return true; 
            }
            else if (position.X + radius > Raylib.GetScreenWidth()) 
            {
                MirrorDraw(-Raylib.GetScreenWidth(), 0);
                return true;
            }
            //Y-akseli
            if (position.Y - radius < 0) 
            {
                MirrorDraw(0, Raylib.GetScreenHeight());
                return true; 
            }
            else if (position.Y + radius > Raylib.GetScreenHeight()) 
            {
                MirrorDraw(0, -Raylib.GetScreenHeight());
                return true; 
            }
            //jos ei mee, palauta false
            return false;
        }
    }
}
