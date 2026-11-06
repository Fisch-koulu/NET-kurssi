using Raylib_cs;
using System.Numerics;
using Ruptus_kirjasto;

namespace Asteroids
{
    internal class Player
    {
        //pelaaja
        Texture2D texture;
        Rectangle drawSource;
        Rectangle rectDestination;
        Vector2 origin;

        Rectangle rect;
        Vector2 position;
        Vector2 size = new Vector2(30,30);

        //liikkuminen
        Vector2 velocity = Vector2.Zero;
        Vector2 acceleration = new Vector2(100,100);
        float maxSpeed;

        //suunta ja kääntyminen
        Vector2 direction;
        float turnSpeed;

        //radiaani: kaikki matemaattiset funktiot (Matrix, sin, cos) haluaa radiaanin.
        float angleRad = 0f;
        //astetita: helpompi laskea ihmiselle ja piirto funktio tarvitsee.
        float angleDeg;

        //ammukset
        public List<Bullet> bullets;
        KeyboardKey shootKey;
        double lastShootTime;
        double shootInterval = 1;
        float bulletShootSpeed = 100;

        //sprite
        Sprite sprite;

        public Player(Vector2 position, float speed, float turnSpeed, KeyboardKey shootKey) 
        {
            //pelaajan paikka
            this.position = position;
            //suunta
            direction = new Vector2(0,-1);
            rect = new Rectangle(position,size);

            //pelaajan liikkumis- ja kääntönopeus
            maxSpeed = speed;
            this.turnSpeed = turnSpeed;

            //muuttaa radiaanin asteiksi
            angleDeg = angleRad * Raylib.RAD2DEG;
            
            //ammusten luonti
            bullets = new List<Bullet>();
            this.shootKey = shootKey;

            //sprite
            sprite = new Sprite(texture);
        }

        /// <summary>
        /// liikuu suoraan
        /// </summary>
        public void Move()
        {
            float deltaTime = Raylib.GetFrameTime(); //en enää jaksanut kirjoittaa uudeleen.
            velocity += acceleration * deltaTime;
            //Console.WriteLine($"{velocity} {acceleration} {deltaTime}");
            if (velocity.Length() > maxSpeed)
            {
                velocity = Vector2.Normalize(velocity) * maxSpeed; 
            }
            //laske paikka
            position += direction * velocity * deltaTime;
            //laske piirtopaikka
            rectDestination = new Rectangle(position, texture.Width/2f, texture.Height/2f);

            //rectangle saa saman paikan
            rect.X = position.X;
            rect.Y = position.Y;
        }

        /// <summary>
        /// Liikuttaa ja Kääntää pelaajaa. W liikutaan ja A/D käännytään.
        /// </summary>
        public void TurnMove()
        {
            //käännä pelaaja oikealle
            if (Raylib.IsKeyDown(KeyboardKey.D))
            {
                angleRad += turnSpeed * Raylib.GetFrameTime();
                angleDeg = angleRad * Raylib.RAD2DEG;
            }
            //käännä pelaaja vasemalle
            else if (Raylib.IsKeyDown(KeyboardKey.A))
            {
                angleRad -= turnSpeed * Raylib.GetFrameTime();
                angleDeg = angleRad * Raylib.RAD2DEG;
            }

            //tee/käytä Matrix
            Matrix3x2 rotationMatrix = Matrix3x2.CreateRotation(angleRad);
            direction = Vector2.Transform(Vector2.UnitX, rotationMatrix);
        }

        /// <summary>
        /// Piirtää pelaajan, joka voi kääntyä.
        /// </summary>
        public void DrawPlayer()
        {
            sprite.DrawRotated(position, angleRad);
            /*trans = new Transforma(position, direction, 100f);
            trans.Move();
            position = trans.Position;
            rect.X = position.X;
            rect.Y = position.Y;
            Raylib.DrawRectanglePro(rect, rect.Size / 2f, angleDeg, Color.Red);*/
            //Raylib.DrawTexturePro(texture, drawSource, rectDestination, origin, angleDeg, Color.Red);
            Raylib.DrawLineV(position + (direction * size / 2), position + (direction * size), Color.Blue);
        }

        public void ShootUpdate()
        {
            if (Raylib.IsKeyPressed(shootKey) && Raylib.GetTime() - lastShootTime > shootInterval)
            {
                lastShootTime = Raylib.GetTime();

                bullets.Add(new Bullet(position + (direction * size / 2), direction, bulletShootSpeed));
            }
        }
    }
}
