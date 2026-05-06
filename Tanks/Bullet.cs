using System.Numerics;
using Raylib_cs;

namespace Tanks
{
    internal class Bullet
    {
        Vector2 bulletPos = new Vector2(100, 100);
        float bulletSize = 10;
        Vector2 bulletDir = new Vector2(1, 0);
        float bulletSpeed = 200f;

        Vector2 startingPos = new Vector2(-10, -10);
        public Vector2 StartingPos {  get { return startingPos; } }

        Color color;

        public Vector2 BulletPos { get { return bulletPos; } set { bulletPos = value; } }

        public Bullet(Color color)
        {
            this.color = color;
        }

        public void Start(Vector2 pos, Vector2 dir)
        {
            bulletDir = dir;
            bulletPos = pos + dir * bulletSize * 4;
        }

        public void Draw()
        {
            Raylib.DrawCircleV(bulletPos, bulletSize, color);
        }

        public void BulletMove()
        {
            bulletPos += bulletDir * bulletSpeed * Raylib.GetFrameTime();
        }

        public bool Hit(Rectangle rec)
        {
            if (Raylib.CheckCollisionPointRec(bulletPos, rec))
            {
                //bullet menee näytön ulos
                Reset();
                return true;
            }
                return false;
        }

        /// <summary>
        /// laittaa bulletin näytön ulkopuolelle
        /// </summary>
        public void Reset()
        {
            bulletPos = startingPos;
            bulletDir = Vector2.Zero;
        }
    }
}
