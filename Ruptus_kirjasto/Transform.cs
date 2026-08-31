using System.Numerics;
using Raylib_cs;

namespace Ruptus_kirjasto
{
    internal class Transform
    {
        private Vector2 position;   //paikka
        private Vector2 direction;  //suunta
        private float speed;    //nopeus

        //muu koodi voi katsoa ja muuttaa
        public Vector2 Position { get { return position; } set { position = value; } } 
        public Vector2 Direction { get { return direction; } set { direction = value; } }

        public Transform(Vector2 position, Vector2 direction, float speed)
        {
            this.position = position;
            this.direction = direction;
            this.speed = speed;
        }

        /// <summary>
        /// Liikuttaa objektia.
        /// </summary>
        public void Move()
        {
            position += direction * speed * Raylib.GetFrameTime();
            //olisin laittanut ettei voi mennä näytöltä ohi tässä, mutta tämä ei ole vielä collderia
            //esim: position.X = Math.Clamp(position.X, (0+size/2), (Raylib.GetScreebWidth()-size/2))
        }

        /// <summary>
        /// Liikkuu näppäinten mukaan.
        ///</summary>
        public void MoveInput(KeyboardKey upKey, KeyboardKey downKey, KeyboardKey leftKey, KeyboardKey rightKey)
        {
            if (Raylib.IsKeyDown(upKey))
            {
                direction = new Vector2(0, -1);
            }
            else if (Raylib.IsKeyDown(downKey))
            {
                direction = new Vector2(0, 1);
            }
            //else if varmistaa, ettei jää jumiin, kun painaa vastakkaisia samaan aikaan.
            if (Raylib.IsKeyDown(leftKey))
            {
                direction = new Vector2(-1, 0);
            }
            else if (Raylib.IsKeyDown(rightKey))
            {
                direction = new Vector2(1, 0);
            }

            //lopuksi liikutaan
            Move();
        }
    }
}
