using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;


namespace ComponentPack
{
    public class Background : GameObject
    {
        public Sprite background;

        public Background(Vector2 position, float rotation, ContentManager content) : base(position, rotation)
        {
            background = new Sprite(this, content.Load<Texture2D>("Sprites/city"), new Rectangle(0, 0, 2304, 1296), new Rectangle(0, 0, 2304, 1296));

        }




    }
}
