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


namespace ComponentPack
{
    internal class MainCharacter : GameObject
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


        }

        public void UpdateMe(KeyboardState prevState, KeyboardState currState)
        {
            base.UpdateMe();

            switch(currentState)
            {
                case CharacterStates.JumpUp:
                    CharacterAnimation.Play("JumpUp");
                    break;
                case CharacterStates.JumpDown:
                    CharacterAnimation.Play("JumpDown");
                    break;
                case CharacterStates.NewJump:
                    break;
                case CharacterStates.PowerJump:
                    break;
                case CharacterStates.Death:
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

        public void BubbleTouch (GameObject collision)
        {
            if(collision is SpeechBubble speech)
            {
                if(currentState == CharacterStates.JumpDown && 
                    CharacterCollision.CollisionRectangle.Top < speech.BubbleCollision.CollisionRectangle.Top)
                {
                    currentState = CharacterStates.NewJump;
                }
            }

            if (collision is AngryBubble angry)
            {
                if (currentState == CharacterStates.JumpDown)
                {
                    currentState = CharacterStates.Death;
                }
            }

            
        }




    }
}

