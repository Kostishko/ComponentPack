using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ComponentPack
{

    /// <summary>
    /// Animation controller
    /// </summary>
    public class AnimationController : IComponent
    {

        #region IComponent variables
        private GameObject parent;
        public GameObject Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        #endregion


        private Sprite spriteComponent;
        private Dictionary<string, AnimationSequence> animations;

        private AnimationSequence currentAnimation;
        public string currentAnimationName;
        private int currentFrame;
        public float speed;
        private float currentTimer;

        public bool IsLooped;

        public enum AnimationStates : byte
        {
            play,
            pause,
            stop,
        }

        private AnimationStates animationState;

        public event EventHandler<string> AnimationFin;

        public AnimationController(GameObject parent, Sprite spriteComponent, Dictionary<string, AnimationSequence> animations, float animationSpeed) 
        {
            Parent = parent;
            this.spriteComponent = spriteComponent;
            this.animations = animations;
            this.speed = 1 / animationSpeed;
            currentTimer = 1 / animationSpeed;
            animationState = AnimationStates.stop;
            Parent.AttachComponent(this);
        }


        public void UpdateMe()
        {
            switch (animationState)
            {
                case AnimationStates.play:
                    if (currentTimer > 0)
                    {
                        currentTimer -= (float)Extentions.TotalSeconds;
                    }
                    else
                    {
                        currentTimer = speed;
                        if (currentFrame + 1 >= currentAnimation.frameCount)
                        {
                            if (IsLooped)
                            {
                                currentFrame = 0;
                                spriteComponent.SetSourceRectangleLocation(currentAnimation.startFramePos);
                            }
                            else
                            {
                                animationState = AnimationStates.stop;
                            }
                            AnimationFin?.Invoke(this, currentAnimationName);
                        }
                        else
                        {
                            currentFrame++;
                            spriteComponent.SetSourceRectangleLocation(new Point(spriteComponent.SourceRectangle.X + spriteComponent.SourceRectangle.Width,
                                spriteComponent.SourceRectangle.Y));
                        }
                    }

                    break;
                case AnimationStates.pause:
                    break;
                case AnimationStates.stop:
                    break;
            }
        }

        public void DrawMe(SpriteBatch spriteBatch)
        {

        }

        public void DeleteMe()
        {

        }

        public void Play(string animationName)
        {
            animationState = AnimationStates.play;
            currentAnimationName = animationName;

            if (currentAnimation != animations[animationName])
            {
                currentAnimation = animations[animationName];
                currentFrame = 0;
                spriteComponent.SetSourceRectangleLocation(currentAnimation.startFramePos);

            }
        }

        /// <summary>
        /// Worj only if this animation controller haa been created with one AnimationSequence (not a Dictionary of animations). 
        /// </summary>
        public void Play()
        {
            if (animations.ContainsKey("animDefault"))
            {
                Play("animDefault");
                currentAnimationName = "animDefault";


            }
        }

        public void Stop()
        {
            spriteComponent.SetSourceRectangleLocation(currentAnimation.startFramePos);
            animationState = AnimationStates.stop;

        }

        public void Pause()
        {
            animationState = AnimationStates.pause;
        }

        public void UnPause()
        {
            animationState = AnimationStates.play;
        }

        public void SetAnimationSpeed(float speed)
        {
            this.speed = speed;
        }



        public GameObject GetParent()
        {
            return parent;
        }
    }

    /// <summary>
    /// Data about one animation sequence - start position on spritesheet andamount of frames
    /// On a spritesheet animation shouldn't be break in two rows
    /// </summary>
    public struct AnimationSequence
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="startPos">Start position of animation on a spritesheet</param>
        /// <param name="frameCount">Frame amount for the animation sequence</param>
        public AnimationSequence(Point startPos, int frameCount)
        {
            startFramePos = startPos;
            this.frameCount = frameCount;
        }

        public Point startFramePos;
        public int frameCount;

        public static bool operator ==(AnimationSequence a, AnimationSequence b)
        {
            if (a.frameCount == b.frameCount && a.startFramePos == b.startFramePos)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool operator !=(AnimationSequence a, AnimationSequence b)
        {
            if (a.frameCount != b.frameCount && a.startFramePos != b.startFramePos)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool Equals([NotNullWhen(true)] object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }

}
