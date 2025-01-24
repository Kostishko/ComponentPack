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
    public class UIPanel : UIElement
    {

        private Texture2D backgroundTexture;

        public UIPanel(Point position, Rectangle rectangle, UIElement parent, Texture2D background) : base(position, parent, rectangle)
        {
            backgroundTexture = background;
        }

        public UIPanel(Point position, Rectangle rectangle, UIElement parent) : base(position, parent, rectangle)
        {

        }



        public override void UpdateMe()
        {

        }

        public override void DrawMe(SpriteBatch spriteBatch)
        {
            if (backgroundTexture != null)
            {
                spriteBatch.Draw(backgroundTexture, rectangle, null, Color.White);
            }
        }

    }
}
