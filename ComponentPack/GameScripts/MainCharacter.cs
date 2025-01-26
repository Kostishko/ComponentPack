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
        public float fallSpeed = 150f;
        public float moveSpeed = 300f;
        public float jumpSpeed = 550f;
        public float timeToUp = 1.5f;
        public float timerToUp = 0f;

        //Death high
        public float deathHighY;
        
        

                


        public MainCharacter(Vector2 position, float rotation, ContentManager content) : base (position, rotation) 
        {
            CharacterSprite = new Sprite(this, content.Load<Texture2D>("Sprites/superhero_sprites"), new Rectangle(0, 0, 390, 630), new Rectangle(0, 0, 65,105));

            CharacterAnimations = new Dictionary<string, AnimationSequence>();
            CharacterAnimations.Add("NewJump", new AnimationSequence(Point.Zero, 3));
            CharacterAnimations.Add("JumpDown", new AnimationSequence(new Point(780,630), 2));
            CharacterAnimations.Add("JumpUp", new AnimationSequence(new Point(390, 630), 2));
            CharacterAnimations.Add("Death", new AnimationSequence(new Point(0,630), 1));           

            CharacterSprite.Origin = new Vector2(0, 0);

            CharacterAnimation = new AnimationController(this, CharacterSprite, CharacterAnimations, 3f);
            CharacterAnimation.AnimationFin += (s, a) =>
            {
                if (a == "NewJump")
                {
                    currentState = CharacterStates.JumpUp;
                    timerToUp = timeToUp;
                }

                if (a == "Death")
                {
                    Game1.StateOfGame = Game1.StateGame.GameOver;
                }
            };

            CharacterCollision = new CollisionBox(this, new Rectangle(0, 0, 65, 105));

            CharacterSounds = new Dictionary<string, SoundEffect>();
            CharacterSounds.Add("Jump", content.Load<SoundEffect>("Sounds/JumpSound"));
            CharacterSounds.Add("Death", content.Load<SoundEffect>("Sounds/DeathSound"));
            CharacterSound = new SoundComponent(this, CharacterSounds);

            //Collisions
            CharacterCollision.collisionOngoing += AngryBubbleTouch;
            CharacterCollision.collisionStart += SpeechBubbleTouch;
            CharacterCollision.collisionStart += ThoughtsBubbleTouch;

            //death conditions
            deathHighY = Transform.Position.Y + 500;


        }

        public void UpdateMe(KeyboardState prevState, KeyboardState currState)
        {
            base.UpdateMe();

            if(Transform.Position.Y > deathHighY)
            {
                currentState = CharacterStates.Death;
            }
            switch(currentState)
            {
                case CharacterStates.JumpUp:
                    CharacterAnimation.Play("JumpUp");

                    //left rigt movement
                    if (currState.IsKeyDown(Keys.A))
                    {
                        CharacterSprite.SpriteEffect = SpriteEffects.FlipHorizontally;
                       
                        Transform.Position.X -= moveSpeed * (float)Extentions.TotalSeconds;
                    }

                    if (currState.IsKeyDown(Keys.D))
                    {
                        CharacterSprite.SpriteEffect = SpriteEffects.None;
                        Transform.Position.X += moveSpeed * (float)Extentions.TotalSeconds;
                    }

                    //flying up
                    Transform.Position.Y -= jumpSpeed * (float)Extentions.TotalSeconds;

                    //jump time
                    if (timerToUp<=0)
                    {
                        currentState = CharacterStates.JumpDown;
                    }
                    timerToUp -= (float)Extentions.TotalSeconds;

                    break;
                case CharacterStates.JumpDown:

                    CharacterAnimation.Play("JumpDown");

                    //movement in faling
                    if (currState.IsKeyDown(Keys.A))
                    {
                        CharacterSprite.SpriteEffect = SpriteEffects.FlipHorizontally;
                        Transform.Position.X -= moveSpeed * (float)Extentions.TotalSeconds;
                    }
                    if (currState.IsKeyDown(Keys.D))
                    {
                        CharacterSprite.SpriteEffect = SpriteEffects.None;                        
                        Transform.Position.X += moveSpeed * (float)Extentions.TotalSeconds;
                    }


                    //flying up
                    Transform.Position.Y += fallSpeed * (float)Extentions.TotalSeconds;

                    break;
                case CharacterStates.NewJump:
                    CharacterAnimation.Play("NewJump");

                    break;
                case CharacterStates.PowerJump:
                    break;
                case CharacterStates.Death:
                    CharacterAnimation.SetAnimationSpeed(1 / 20f);
                    CharacterAnimation.Play("Death");
                    break;
            }


        }

        public override void DrawMe(SpriteBatch sp)
        {
            base.DrawMe(sp);

            DebugManager.DebugRectangle(CharacterCollision.CollisionRectangle);

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
                    CharacterCollision.CollisionRectangle.Top < speech.BubbleCollision.CollisionRectangle.Top
                    && CharacterCollision.CollisionRectangle.Bottom<speech.BubbleCollision.CollisionRectangle.Bottom)
                {
                    currentState = CharacterStates.NewJump;
                    deathHighY = Transform.Position.Y + 500;
                    CharacterSound.PlayNow("Jump");
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
                    CharacterSound.PlayNow("Death");
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

