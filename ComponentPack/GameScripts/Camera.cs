using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Microsoft.VisualBasic;
using ComponentPack;

namespace ComponentPack
{
    /// <summary>
    /// Camera that follow for particular game object (may be we want to add some empty gameobject to fly around the level before attach the camera to character)
    /// </summary>
    internal class Camera
    {

        public Vector2 position;
        public float zoom;

        //Camera target object
        private GameObject cameraTarget;

        //Size of a game screen
        public Vector2 screenSize;

        //Camera can't cross the border.
        public Vector2 leftUpperBorder;
        public Vector2 rightBottomBorder;

        //Camera smooth moving 
        public Vector2 _velocity;
        private float speed = 10;

        //Shaking effect variables
        public Vector2 savedPos;
        private float shakeTimer;
        private float shakeTime;
        public Random rng;
        private int shakePower;

        // Add properties for the Y-axis boundaries
        private float topBoundary;
        private float bottomBoundary;

        /// <summary>
        /// Camera 
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="leftUpBorder"></param>
        /// <param name="rightBottBorder"></param>
        /// <param name="screenSize">Size and proportions</param>
        public Camera(Vector2 pos, Vector2 leftUpBorder, Vector2 rightBottBorder, Vector2 screenSize)
        {
            this.position = pos;
            leftUpperBorder = leftUpBorder;
            rightBottomBorder = rightBottBorder;
            this.screenSize = screenSize;
            zoom = 1;
            rng = new Random();
            shakeTimer = 0;
            shakeTime = 1.5f;
        }

        /// <summary>
        /// Mehtod that return the matrix (used for spriteBatch Begin method in a Game1)
        /// </summary>
        /// <returns></returns>
        public Matrix GetCam()
        {
            Matrix temp;
            temp = Matrix.CreateTranslation(new Vector3(position.X, position.Y, 0));
            temp *= Matrix.CreateScale(zoom);
            return temp;
        }

        public void UpdateMe()
        {

          
            if (cameraTarget != null)
            {
                float targetX = -cameraTarget.Transform.Position.X + screenSize.X / (2 * zoom) - 100;
                float targetY = -cameraTarget.Transform.Position.Y + (screenSize.Y / 2f) / zoom;

                position.Y = MathHelper.Lerp(position.Y, targetY, 0.04f);
               // _velocity.Y = position.Y - targetY;


            }
        }

        public void Shaking(Vector2 cameraTarget)
        {
            if (shakeTimer > 0)
            {
                var temp = rng.Next(-shakePower, shakePower);
                //shaking in borders of bounds
                if (cameraTarget.X + temp + screenSize.X / (2 * zoom) < rightBottomBorder.X
                    && cameraTarget.X + temp + screenSize.X / (2 * zoom) > leftUpperBorder.X)
                {
                    position.X += temp;
                }
                else
                {
                    position.X -= temp;
                }
                if (cameraTarget.Y + temp + screenSize.Y / (2 * zoom) < rightBottomBorder.Y
                    && cameraTarget.Y + temp + screenSize.Y / (2 * zoom) > leftUpperBorder.Y)
                {
                    position.Y += temp;
                }
                else
                {
                    position.Y -= temp;
                }
                shakeTimer -= 0.1f * (float)Extentions.TotalSeconds;
            }
        }

        /// <summary>
        /// Initialise the shaking with power (unfortunatelly timer of shaking is needed)
        /// </summary>
        /// <param name="power"></param>
        public void StartShaking(int power)
        {
            shakeTimer = shakeTime;
            shakePower = power;
        }



        /// <summary>
        /// Change zoom
        /// </summary>
        /// <param name="zoom"></param>
        public void SetZoom(float zoom)
        {
            this.zoom = zoom;
        }

        /// <summary>
        /// Get zoom
        /// </summary>
        /// <returns></returns>
        public float GetZoom()
        {
            return zoom;
        }


        /// <summary>
        /// Get current camera terget
        /// </summary>
        /// <returns></returns>
        public GameObject GetCameraTarget()
        {
            return cameraTarget;
        }

        /// <summary>
        /// Set current camera target
        /// </summary>
        /// <param name="cameraTarget"></param>
        public void SetCameraTarget(GameObject cameraTarget)
        {
            this.cameraTarget = cameraTarget;
            //following for the target in allowed borders
            if (cameraTarget.Transform.Position.X + screenSize.X / (2 * zoom) < rightBottomBorder.X &&
                cameraTarget.Transform.Position.X + screenSize.X / (2 * zoom) > leftUpperBorder.X)
            {

                position.X = -cameraTarget.Transform.Position.X - 50 + screenSize.X / (2 * zoom);
            }

            if (cameraTarget.Transform.Position.Y + screenSize.Y / (2 * zoom) < rightBottomBorder.Y &&
                cameraTarget.Transform.Position.Y + screenSize.Y / (2 * zoom) > leftUpperBorder.Y)
            {
                position.Y = -cameraTarget.Transform.Position.Y + 50 + screenSize.Y / (2 * zoom);
            }
        }

        public float GetVelocityX()
        {
            return _velocity.X;
        }

        public float GetVelocityY()
        {
            return _velocity.Y;
        }

    }
}