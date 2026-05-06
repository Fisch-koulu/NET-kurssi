using System.Numerics;
using System.Threading.Tasks;
using Raylib_cs;

namespace Tanks
{
    internal class Tanks
    {
        public Vector2 tankPos;
        Vector2 tankDir = new Vector2(1, 0);

        Vector2 tankSize = new Vector2(50, 50);
        Vector2 turretSize = new Vector2(20, 20);
        float tankSpeed = 100f;
        public Rectangle rec;
        public Rectangle recB;
        Color tankColor;

        Vector2 turretPos;

        double lastShootTime = 0;
        double shootInterval = 1;
        
        //get ja set on public, jos ei kerro näkyvyysmäärettä. Muut luokat voivat katsoa, muttei voi muuttaa.
        public Vector2 TankDir { get { return tankDir; } private set { tankDir = value; } }
        
        public Tanks(Vector2 tankPos, Color tankColor) 
        { 
            this.tankColor = tankColor;
            this.tankPos = tankPos;
        }

        /// <summary>
        /// piirtää tankit
        /// </summary>
        public void Draw()
        {
            //tankin body
            Vector2 tankTopLeft = tankPos - tankSize / 2.0f; //tankin piirtokohta on keskellä (eli position on keskellä)
            rec = new Rectangle(tankTopLeft, tankSize);
            //Raylib.DrawRectangleV(tankTopLeft, tankSize, tankColor);
            
            
            Raylib.DrawRectangleRec(rec, tankColor);

            // tankin tykki (turret) sijoitetaan käyttämällä suuntia (positioned using directions)
            //size X ja Y odotetaan olevan sama
            turretPos = tankPos + tankDir * (tankSize.X / 2.0f + turretSize.X / 2.0f);
            Vector2 turretTopLeft = turretPos - turretSize / 2.0f;
            recB = new Rectangle(turretTopLeft, turretSize);

            Raylib.DrawRectangleRec(recB, Color.DarkGray);
        }

        public void TankMove()
        {
            //tankki liikkuu
            tankPos += tankDir * tankSpeed * Raylib.GetFrameTime();
            //estää tankkien karkaamasta näytön ulkopuolelle
            //lisää/miinusta puolet tankin kooasta (en jaksa tehdä omaa muuttujaa joten hard coding)
            tankPos.X = Math.Clamp(tankPos.X, 25, 975);
            tankPos.Y = Math.Clamp(tankPos.Y, 25, 675);
        }

        //kunteelee pelaajan syötteen ja laittaa oikeaan suuntaan
        public void Input(List<Bullet> bullets, KeyboardKey upKey,  KeyboardKey downKey, KeyboardKey leftKey, KeyboardKey rightKey, KeyboardKey shootKey)
        {
            if (Raylib.IsKeyDown(upKey))
            {
                //mene oikeaan suuntaan, eli ylös
                tankDir = new Vector2(0, -1);
                TankMove();
            }
            else if (Raylib.IsKeyDown(downKey))
            {
                //mene oikeaan suuntaan, eli alas
                tankDir = new Vector2(0, 1);
                TankMove();
            }
            else if (Raylib.IsKeyDown(leftKey))
            {
                //mene oikeaan suuntaan, eli vasemalle
                tankDir = new Vector2(-1, 0);
                TankMove();
            }
            else if (Raylib.IsKeyDown(rightKey))
            {
                //mene oikeaan suuntaan, eli oikealle
                tankDir = new Vector2(1, 0);
                TankMove();
            }
            if (Raylib.IsKeyDown(shootKey))
            {
                Shoot(bullets);
            }

            Vector2 tankTopLeft = tankPos - tankSize / 2.0f; //tankin piirtokohta on keskellä (eli position on keskellä)
            rec = new Rectangle(tankTopLeft, tankSize);
        }

        public void Shoot(List<Bullet> bullets)
        {
            if (Raylib.GetTime() - lastShootTime > shootInterval)
            {
                //tankki ampuu
                bullets.Add(new Bullet(tankColor));
                bullets[bullets.Count - 1].Start(tankPos, tankDir);

                //päivitä viimeinen ampumis aika 
                lastShootTime = Raylib.GetTime();
            }
        }
    }
}
