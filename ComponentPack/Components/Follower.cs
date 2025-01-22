using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ComponentPack
{
    /// <summary>
    /// Makes the GameObject follow for another GameObject
    /// </summary>
    public class Follower : IComponent
    {
        private GameObject parent;
        public GameObject Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        public GameObject Leader;

        public Vector2 ShiftPosition;
        private float distance;

        private float rotationAroundLeader;
        public float RotationAroundLeader
        {
            get { return rotationAroundLeader; }
            set { rotationAroundLeader = Extentions.ModulasClamp(value, 0, (float)Math.PI * 2); }
        }
        private float rotationOffset;

        //Rotation arounf its axis will copy rotation of the Leader
        public bool isCopyLeaderRotation;
        public bool isAutoRotateAroundParent;

        public Follower(GameObject parent, GameObject leader, Vector2 shiftPosition)
        {
            Parent = parent;
            Leader = leader;

            this.ShiftPosition =  shiftPosition;
            RotationAroundLeader = (float)Math.Atan2(ShiftPosition.Y, ShiftPosition.X);
            rotationOffset = (float)Math.Atan2(ShiftPosition.Y, ShiftPosition.X);
            distance = (float)Math.Sqrt(Math.Pow(ShiftPosition.X, 2) + Math.Pow(ShiftPosition.Y, 2));
        }

        public void UpdateMe()
        {
            if (parent != null)
            {
                if (isCopyLeaderRotation)
                {
                    Parent.Transform.Rotation = Leader.Transform.Rotation;
                }

                if (isAutoRotateAroundParent)
                {
                    RotationAroundLeader = rotationOffset +Leader.Transform.Rotation;
                   
                }

                Vector2 shiftPosition = new Vector2((float)Math.Cos(RotationAroundLeader),
                (float)Math.Sin(RotationAroundLeader)) * Math.Clamp(distance, 0, float.MaxValue);

                Parent.Transform.Position = Leader.Transform.Position + shiftPosition;
            }          
        }

        public void DrawMe(SpriteBatch spriteBatch) 
        {
        
        }

        public void DeleteMe()
        {
            Leader = null;
            Parent = null;
        }

        public GameObject GetParent()
        {
            return Parent;
        }
    }
}
