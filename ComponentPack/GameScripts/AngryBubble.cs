using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace ComponentPack.GameScripts
{
    public class AngryBubble : Bubble
    {

        public AngryBubble(Vector2 position, float rotation, ContentManager content) : base(position, rotation, content)
        {
            BubbleSprite = new Sprite(this, content.Load<Texture2D>(""), new Rectangle(0, 0, 0, 0), new Rectangle(0, 0, 0, 0));

            BubbleAnimations = new Dictionary<string, AnimationSequence>();
            BubbleAnimations.Add("Idle", new AnimationSequence(Point.Zero, 3));
            BubbleAnimations.Add("Exploud", new AnimationSequence(Point.Zero, 3));

            BubbleAnimController = new AnimationController(this, BubbleSprite, BubbleAnimations, 6f);

            BubbleCollision = new CollisionBox(this, new Rectangle(0, 0, 10, 10));

            BubbleAnimController.IsLooped = true;
            BubbleAnimController.Play("Idle");
        }



    }
}
