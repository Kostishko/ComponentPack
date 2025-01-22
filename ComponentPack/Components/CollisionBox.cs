using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ComponentPack
{
    public class CollisionBox : IComponent
    {

        private GameObject parent;
        public GameObject Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        


        private Rectangle collisionRec;
        public Rectangle CollisionRectangle
        {
            get => collisionRec;
        }

        private Point collisionDefaultSize;
        private Vector2 startOriginPoint;
        private Vector2 originPoint;



        //events for each stage of the collision
        public event EventHandler<GameObject> collisionStart;
        public event EventHandler<GameObject> collisionOngoing;
        public event EventHandler<GameObject> collisionEnd;


        public CollisionBox(GameObject parent,  Rectangle collisionRec) 
        {
            Parent = parent;            
            this.collisionRec = collisionRec;

            collisionDefaultSize = collisionRec.Size;
            originPoint = this.collisionRec.Size.ToVector2() / 2;            
            this.collisionRec.Location = parent.Transform.Position.ToPoint() - originPoint.ToPoint();

            CollisionProcessor.AddNewComponent(this);
        }

        public void UpdateMe()
        {
            collisionRec.Location = Parent.Transform.Position.ToPoint()  - originPoint.ToPoint();            
        }


        public void DrawMe(SpriteBatch sp)
        {

        }


        public void DeleteMe()
        {
            CollisionProcessor.DeleteComponent(this);
        }

        internal void CollisionStarted(GameObject collided)
        {
            collisionStart?.Invoke(this, collided);
        }

        internal void CollisionOngoing(GameObject collided)
        {
            collisionOngoing?.Invoke(this, collided);
        }

        internal void CollisionEnded(GameObject collided)
        {
            collisionEnd?.Invoke(this, collided);
        }

        public GameObject GetParent()
        {
            return Parent;
        }


    }
}
