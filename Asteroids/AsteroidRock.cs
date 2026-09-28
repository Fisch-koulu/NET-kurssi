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
            position += direction * speed * Raylib.GetFrameTime();
        }

        public void Draw()
        {
            Raylib.DrawCircleV(position, radius, Color.Blue);
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
            if (position.X > Raylib.GetScreenWidth()) 
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
            if (position.Y > Raylib.GetScreenHeight()) 
            {
                position.Y = 0;
                return true; 
            }
            //jos ei mee, palauta false
            return false;
        }
    }
}
