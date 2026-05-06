using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;

namespace Tanks
{
    internal class Wall
    {
        Rectangle wallArea;
        Vector2 size;
        Vector2 position;
        Color color = Color.Black;

        public Wall(int posX, int posY, int width, int height) 
        {
            position = new Vector2(posX, posY);
            size = new Vector2(width, height);
            wallArea = new Rectangle(position, size);
        }

        public void Draw()
        {
            Raylib.DrawRectangleRec(wallArea, color);
        }

        public Vector2 CheckCollidedTank(Rectangle rec, Vector2 pos, Vector2 dir)
        {
            if (Raylib.CheckCollisionRecs(rec, wallArea))
            {
                Rectangle sisalla = Raylib.GetCollisionRec(rec, wallArea);

                pos.X -= dir.X * sisalla.Width;
                pos.Y -= dir.Y * sisalla.Height;
            }
            return pos;
        }

        public Vector2 CheckCollidedBullet(Bullet bullet, Vector2 pos)
        {
            if (Raylib.CheckCollisionPointRec(pos, wallArea))
            {
                bullet.Reset();
                pos = new Vector2(-10, -10);
            }
            return pos;
        }
    }
}
