using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System.Collections;
using ComponentPack.GameScripts;
using System.Reflection.Metadata;


namespace ComponentPack
{
    public class MainCharacter : GameObject
    {
        public Sprite CharacterSprite;
        public AnimationController CharacterAnimation;
        public Dictionary<string, AnimationSequence> CharacterAnimations;
        public CollisionBox CharacterCollision;
        public SoundComponent CharacterSound;
        public Dictionary<string,  SoundEffect> CharacterSounds;

        public enum CharacterStates
        {
            JumpUp,
            JumpDown,
            NewJump,
            PowerJump,
            Death
        }

        public CharacterStates currentState;

        //character variables
        public float fallSpeed = 100f;
        public float moveSpeed = 100f;
        public float jumpSpeed = 100f;
        public float timeToUp = 1f;
        public float timerToUp = 0f;
        
        

                


        public MainCharacter(Vector2 position, float rotation, ContentManager content) : base (position, rotation) 
        {
            CharacterSprite = new Sprite(this, content.Load<Texture2D>(""), new Rectangle(0, 0, 0, 0), new Rectangle(0, 0, 0, 0));

            CharacterAnimations = new Dictionary<string, AnimationSequence>();
            CharacterAnimations.Add("JumpUp", new AnimationSequence(Point.Zero, 4));
            CharacterAnimations.Add("JumpDown", new AnimationSequence(Point.Zero, 4));
            CharacterAnimations.Add("Death", new AnimationSequence(Point.Zero, 1));

            CharacterAnimation = new AnimationController(this, CharacterSprite, CharacterAnimations, 6f);

            CharacterCollision = new CollisionBox(this, new Rectangle(0, 0, 10, 10));

            CharacterSounds = new Dictionary<string, SoundEffect>();
            CharacterSounds.Add("Jump", content.Load<SoundEffect>(""));
            CharacterSounds.Add("Death", content.Load<SoundEffect>(""));
            CharacterSound = new SoundComponent(this, CharacterSounds);

            //Collisions
            CharacterCollision.collisionOngoing += AngryBubbleTouch;
            CharacterCollision.collisionStart += SpeechBubbleTouch;
            CharacterCollision.collisionStart += ThoughtsBubbleTouch; 


        }

        public void UpdateMe(KeyboardState prevState, KeyboardState currState)
        {
            base.UpdateMe();

            switch(currentState)
            {
                case CharacterStates.JumpUp:
                    CharacterAnimation.Play("JumpUp");

                    //left rigt movement
                    if (currState.IsKeyDown(Keys.A))
                    {
                        CharacterSprite.SpriteEffect = SpriteEffects.None;
                        Transform.Position.X -= moveSpeed * (float)Extentions.TotalSeconds;
                    }

                    if (currState.IsKeyDown(Keys.D))
                    {
                        CharacterSprite.SpriteEffect = SpriteEffects.FlipHorizontally;
                        Transform.Position.X += moveSpeed * (float)Extentions.TotalSeconds;
                    }

                    //flying up
                    Transform.Position.Y -= jumpSpeed * (float)Extentions.TotalSeconds;

                    //jump time
                    if (timerToUp<=0)
                    {
                        currentState = CharacterStates.JumpDown;
                    }
                    else
                    {
                        timerToUp = timeToUp;
                    }

                    break;
                case CharacterStates.JumpDown:

                    CharacterAnimation.Play("JumpDown");

                    //movement in faling
                    if (currState.IsKeyDown(Keys.A))
                    {
                        CharacterSprite.SpriteEffect = SpriteEffects.None;
                        Transform.Position.X -= moveSpeed * (float)Extentions.TotalSeconds;
                    }
                    if (currState.IsKeyDown(Keys.D))
                    {
                        CharacterSprite.SpriteEffect = SpriteEffects.FlipHorizontally;
                        Transform.Position.X += moveSpeed * (float)Extentions.TotalSeconds;
                    }


                    //flying up
                    Transform.Position.Y += fallSpeed * (float)Extentions.TotalSeconds;

                    break;
                case CharacterStates.NewJump:
                    break;
                case CharacterStates.PowerJump:
                    break;
                case CharacterStates.Death:
                    CharacterAnimation.Play("Death");
                    break;
            }


        }

        public override void DrawMe(SpriteBatch sp)
        {
            base.DrawMe(sp);

            switch (currentState)
            {
                case CharacterStates.JumpUp:
                    break;
                case CharacterStates.JumpDown:
                    break;
                case CharacterStates.NewJump:
                    break;
                case CharacterStates.PowerJump:
                    break;
                case CharacterStates.Death:
                    break;
            }

        }

        public void SpeechBubbleTouch (object o, GameObject collision)
        {
            if(collision is SpeechBubble speech)
            {
                if(currentState == CharacterStates.JumpDown && 
                    CharacterCollision.CollisionRectangle.Top < speech.BubbleCollision.CollisionRectangle.Top)
                {
                    currentState = CharacterStates.NewJump;
                    speech.ExploudMe();
                }
            }                     
        }

        public void AngryBubbleTouch (object o, GameObject collision)
        {
            if(collision is  AngryBubble angry)
            {
                if(currentState!= CharacterStates.Death)
                {
                    currentState = CharacterStates.Death;
                    angry.ExploudMe();
                }
            }
        }

        public void ThoughtsBubbleTouch(object o, GameObject collision)
        {
            if (collision is ThoughtsBubble thoughts)
            {
                if (currentState == CharacterStates.JumpDown &&
                   CharacterCollision.CollisionRectangle.Top < thoughts.BubbleCollision.CollisionRectangle.Top)
                {
                    currentState = CharacterStates.PowerJump;
                    thoughts.ExploudMe();
                }
            }
        }



    }
}

