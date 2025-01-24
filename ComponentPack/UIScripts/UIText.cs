using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ComponentPack.UIScripts
{
    public class UIText : UIElement
    {

        private SpriteFont font;
        private string text;


        public UIText(Point position, Rectangle rectangle, UIElement parent, SpriteFont font, string text) : base(position, parent, rectangle)
        {
            this.font = font;


            if (this.font.MeasureString(text).X >= rectangle.Width)
            {
                string currString = "";
                string currWord = "";
                for (int i = 0; i < text.Length; i++)
                {
                    if (!Char.IsWhiteSpace(text[i]))
                    {
                        currWord += text[i];
                        if (font.MeasureString(currString + currWord).X > rectangle.Width)
                        {
                            text += currString + "\n";
                            currString = "";
                        }
                    }
                    else
                    {
                        if (font.MeasureString(currString + currWord).X < rectangle.Width)
                        {
                            currString += currWord;
                            currWord = "";
                        }
                        else
                        {
                            text += currString + "\n";
                            currString = currWord;
                            currWord = "";
                        }
                    }
                }
                this.text = text;

            }
            else
            {
                this.text = text;
            }

        }




        public override void DrawMe(SpriteBatch spriteBatch)
        {
            if (IsVisible)
                spriteBatch.DrawString(font, text, Position.ToVector2(), Color.White);

        }








    }
}
