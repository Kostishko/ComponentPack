using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ComponentPack
{

    /// <summary>
    /// Drawing component
    /// </summary>
    public class Sprite : IComponent
    {

        private GameObject parent;
        public GameObject Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        public Texture2D Texture;
        private Rectangle sourceRectangle;
        public Rectangle SourceRectangle
        {
            get { return sourceRectangle; }
        }

        public Rectangle DestinationRectangle;
        public Color Tint;
        public Vector2 Origin;
        public SpriteEffects SpriteEffect;
        public Extentions.DrawLayers Layer; 

        /// <summary>
        /// Sprite for drawning
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="texture"></param>
        /// <param name="sourceRectangle">Part of Texture2D which should be taken</param>
        /// <param name="destinationRectangle">Where that should be drawned and control width/height of the picture</param>
        public Sprite(GameObject parent, Texture2D texture, Rectangle sourceRectangle, Rectangle destinationRectangle)
        {
            Parent = parent;
            Texture = texture;
            this.sourceRectangle = sourceRectangle;
            this.DestinationRectangle = destinationRectangle;
            Origin = new Vector2(destinationRectangle.Width/2, destinationRectangle.Height/2);
            SpriteEffect = SpriteEffects.None;
            Parent.AttachComponent(this);
            Tint = Color.White;
        }

        public void UpdateMe()
        {
            DestinationRectangle.Location = Parent.Transform.Position.ToPoint();
        }

        public void DrawMe(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, DestinationRectangle, SourceRectangle, Tint, 
                Parent.Transform.Rotation, Origin, SpriteEffect, 
                (float)Layer / (float)Extentions.DrawLayers.maxLayer);
        }

        public void DeleteMe()
        {

        }

        public GameObject GetParent()
        {
            return Parent;
        }

        public void SetSourceRectangleLocation(Point newLocation)
        {
            sourceRectangle.Location = newLocation;
        }

    }
}
