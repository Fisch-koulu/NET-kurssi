using System.Numerics;
using Raylib_cs;

namespace Ruptus_kirjasto
{
    public class Drawing
    {
        static public void TextureCentered(Texture2D texture, Vector2 position)
        {
            Raylib.DrawTextureV(texture, position - new Vector2(texture.Width / 2, texture.Height / 2), Color.White);
        }

        static public void DrawCentered(Vector2 position, Vector2 size, Color color)
        {
            Raylib.DrawRectangleV(position - new Vector2(size.X / 2, size.Y / 2), size, color);
        }
    }
}
