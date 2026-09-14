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
            position += direction * speed * Raylib.GetFrameTime();
        }

        public void Draw()
        {
            Raylib.DrawCircleV(position, radius, Color.Blue);
        }
    }
}
