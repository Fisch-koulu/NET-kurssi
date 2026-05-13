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
            BorderCheck();
        }

        public void BulletMove()
        {
            bulletPos += bulletDir * bulletSpeed * Raylib.GetFrameTime();
        }

        /// <summary>
        /// Katsoo osuiko toiseen tankiin.
        /// </summary>
        /// <param name="rec">toisen tankkin hitbox</param>
        /// <returns>palauttaa true tai false, riippuen osuiko toiseen tankiin.</returns>
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
        /// tarkistaa, menikö pallo kentän yli ja "poistaa", jos meni.
        /// </summary>
        public void BorderCheck()
        {
            if (bulletPos.X <= 0 || bulletPos.X >= 1000) 
            {
                Reset();
                return; }
            else if (bulletPos.Y <= 0 || bulletPos.Y >= 700) 
            { 
                Reset();
                return; }
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
