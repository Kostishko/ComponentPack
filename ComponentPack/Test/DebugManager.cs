using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ComponentPack
{
    static public class DebugManager
    {
        static public SpriteFont DebugFont;
        static public Texture2D DebugTexture;
        static public SpriteBatch SpriteBatch;
        static public bool isWorking;


        static public void DebugRectangle(Rectangle rec)
        {
            if (isWorking)
            {
                SpriteBatch.Draw(DebugTexture, rec, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1);
            }
        }

        static public void DebugLine(string message, Vector2 position)
        {
            if (isWorking)
            {
                SpriteBatch.DrawString(DebugFont, message, position, Color.White);
            }

        }

    }
}
