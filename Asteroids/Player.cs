using Raylib_cs;
using System.Numerics;
using Ruptus_kirjasto;

namespace Asteroids
{
    internal class Player
    {
        //pelaaja
        Rectangle rect;
        Vector2 position;
        Vector2 size = new Vector2(30,30);
        float speed;

        //suunta ja kääntyminen
        Vector2 direction;
        float turnSpeed;

        //radiaani
        float angleRad = 0f;
        //astetita
        float angleDeg;

        public Player(Vector2 position, float speed, float turnSpeed) 
        {
            //pelaajan paikka
            this.position = position;
            //suunta
            direction = new Vector2(0,-1);
            rect = new Rectangle(position,size);

            //pelaajan liikkumis- ja kääntönopeus
            this.speed = speed;
            this.turnSpeed = turnSpeed;

            //muuttaa radiaanin asteiksi
            angleDeg = angleRad * Raylib.RAD2DEG;
        }

        /// <summary>
        /// Liikuttaa ja Kääntää pelaajaa
        /// </summary>
        public void TurnMove()
        {
            Console.WriteLine(position);
            //tämä liikuttaa pelaaja eteenpäin
            if (Raylib.IsKeyDown(KeyboardKey.W))
            {
                position += direction * speed * Raylib.GetFrameTime();
            }
            //nämä kääntää pelaajan
            if (Raylib.IsKeyDown(KeyboardKey.D))
            {
                angleRad += turnSpeed * Raylib.GetFrameTime();
                angleDeg = angleRad * Raylib.RAD2DEG;

                Matrix3x2 rotation = Matrix3x2.CreateRotation(angleRad);
                direction = Vector2.Transform(Vector2.UnitX, rotation);
            }
            if (Raylib.IsKeyDown(KeyboardKey.A))
            {
                angleRad -= turnSpeed * Raylib.GetFrameTime();
                angleDeg = angleRad * Raylib.RAD2DEG;

                Matrix3x2 rotation = Matrix3x2.CreateRotation(angleRad);
                direction = Vector2.Transform(Vector2.UnitX, rotation);
            }

            rect.X = position.X; 
            rect.Y = position.Y;
        }

        public void DrawPlayer()
        {
            Raylib.DrawRectanglePro(rect, rect.Size / 2f, angleDeg, Color.Red);
        }
    }
}
