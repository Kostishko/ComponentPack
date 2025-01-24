
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ComponentPack
{
    internal class TestObject : GameObject
    {

        private Sprite texture;

        public TestObject (Vector2 position, float rotation, ContentManager content) : base (position, rotation)
        {
            texture = new Sprite(this, content.Load<Texture2D>(""), new Rectangle(0, 0, 16, 16), 
                new Rectangle(0, 0, 64, 64));
        }


        public override void UpdateMe()
        {
            base.UpdateMe();

        }


    }
}
