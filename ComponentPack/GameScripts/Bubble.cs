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
    public abstract class Bubble : GameObject
    {
        public  Sprite BubbleSprite;
        public AnimationController BubbleAnimController;
        public Dictionary<string, AnimationSequence> BubbleAnimations;
        public CollisionBox BubbleCollision;

        public event EventHandler Exploded;

        public bool IsActive;

        public Bubble(Vector2 position, float rotation, ContentManager content) : base (position, rotation)
        {
            IsActive = false; 
        }

        public void LoadMe(Vector2 newPositon)
        {
            Transform.Position = newPositon;
        }

        public void ExploudMe()
        {
            Exploded?.Invoke(this, EventArgs.Empty);
            IsActive = false;
        }

        public override void DrawMe(SpriteBatch sp)
        {
            base.DrawMe(sp);
            DebugManager.DebugRectangle(BubbleCollision.CollisionRectangle);
        }
        



    }
}
