using System.Numerics;
using Raylib_cs;

namespace Ruptus_kirjasto
{
    public class Sprite
    {
        Texture2D image;
        Rectangle drawSource;
        Vector2 origin;

        public Sprite(Texture2D image)
        {
            this.image = image;

            //kuvan koko ei muutu, niin voi laskea etukäteen
            drawSource = new Rectangle(0, 0, image.Width, image.Height);
            origin = new Vector2(image.Width / 2, image.Height / 2);
        }

        /// <summary>
        /// Piirtää sprite kuvan käännettynä.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="rotationRadians"></param>
        public void DrawRotated(Vector2 position, float rotationRadians)
        {
            //muuttaa asteiksi
            float rotationDeg = Raylib.RAD2DEG * rotationRadians;

            //laske piirtopaikka
            Rectangle drawDestination = new Rectangle(position, image.Width, image.Height);

            //piirrä kuva käännettynä
            Raylib.DrawTexturePro(
                image, drawSource, drawDestination,
                origin, rotationDeg, Color.White);
        }
    }
}
