using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;


namespace ComponentPack
{

    /// <summary>
    /// Game object is an Entity in Entity-Component pattern
    /// </summary>
    public class GameObject
    {
        public Transform2D Transform;
        public List<IComponent> MyComponents;

        #region Contructors
        public GameObject(Transform2D trtansform)
        {
            Transform = trtansform;
            MyComponents = new List<IComponent>();
        }

        public GameObject(Vector2 position, float rotation)
        {
            Transform = new Transform2D(rotation, position);
            MyComponents = new List<IComponent>();

        }

        public GameObject(Vector2 position)
        {
            Transform = new Transform2D(position);
            MyComponents = new List<IComponent>();
        }

        public GameObject(float rotation)
        {
            Transform = new Transform2D(rotation);
            MyComponents = new List<IComponent>();
        }
        #endregion

        public virtual void UpdateMe()
        {
            for (int i = 0; i < MyComponents.Count; i++) 
            {
                MyComponents[i].UpdateMe();
            }
        }

        public virtual void DrawMe(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < MyComponents.Count; i++)
            {
                MyComponents[i].DrawMe(spriteBatch);
            }
        }

        public void AttachComponent(IComponent component)
        {
            MyComponents.Add(component);
        }

        public virtual void DeleteMe()
        {
            for (int i = 0; i < MyComponents.Count; i++)             
            {
                MyComponents [i].DeleteMe();
            }
            MyComponents.Clear();

        }
    }


    /// <summary>
    /// Position and rotation.
    /// </summary>
    public class Transform2D
    {
        private float rotation;
        public float Rotation
        {
            get { return rotation; }
            set { rotation = Extentions.ModulasClamp(value, 0, (float)Math.PI * 2); }
        }

        public Vector2 Position;

        #region Constructors
        public Transform2D(float rotation, Vector2 position)
        {
            Rotation = rotation;
            Position = position;
        }

        public Transform2D (float rotation)
        {
            Rotation = rotation;
            Position = Vector2.Zero;
        }

        public Transform2D(Vector2 position)
        {
            Rotation = 0;
            Position = position;
        }
        #endregion
    }

}
