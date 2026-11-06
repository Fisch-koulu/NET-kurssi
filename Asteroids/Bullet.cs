using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;

namespace Asteroids
{
    internal class Bullet
    {
        Vector2 position;
        Vector2 direction;
        float bulletSpeed;

        public Vector2 Position { get => position; set => position = value; }

        public Bullet(Vector2 position, Vector2 direction, float bulletSpeed) 
        {
            this.position = position;
            this.direction = direction;
            this.bulletSpeed = bulletSpeed;
        }

        public void BulletMove()
        {
            position += direction * bulletSpeed * Raylib.GetFrameTime();
        }

        public void BulletDraw()
        {
            Raylib.DrawCircleV(position, 2f, Color.White);
        }
    }
}
