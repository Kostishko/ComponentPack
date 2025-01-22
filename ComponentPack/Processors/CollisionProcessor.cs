using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentPack
{
    public static class CollisionProcessor
    {

        private static List<CollisionBox> collisionComponents = new List<CollisionBox>();
        private static List<Collision> collisions = new List<Collision>();

        public static void UpdateMe()
        {


            //check fo rnew collisions and ongoing of previous.
            for (int i = 0; i < collisionComponents.Count; i++)
            {
                for (int j = i + 1; j < collisionComponents.Count; j++)
                {
                    if (collisionComponents[i].CollisionRectangle.Intersects(collisionComponents[j].CollisionRectangle))
                    {
                        var coll = new Collision(new List<CollisionBox>() { collisionComponents[i], collisionComponents[j] });
                        if (!IsCollidedBefore(coll)) //check if this is a new collision
                        {
                            collisions.Add(coll); //add collision to collision list
                            coll.CollisionStart();
                        }
                        else
                        {
                            coll.CollisionOngoing(); // collision ongoing is still work once even if it canceled ( in a name of optimisation)
                        }
                    }
                }
            }
            //looking if some collisions are not actual anymore
            for (int i = 0; i < collisions.Count; i++)
            {
                if (!collisions[i].IsColliding())
                {
                    collisions[i].CollisionEnded();
                    collisions.RemoveAt(i);
                }
            }

        }

        public static void AddNewComponent(CollisionBox collisionComponent)
        {
            collisionComponents.Add(collisionComponent);
        }

        public static void DeleteComponent(CollisionBox collisionComponent)
        {
            for (int i = 0; i < collisions.Count; i++)
            {
                if (collisions[i].collidedComponents.Contains(collisionComponent))
                {
                    collisions.RemoveAt(i);
                }
            }
            collisionComponents.Remove(collisionComponent);
        }

        private static bool IsCollidedBefore(Collision collision)
        {
            for (int i = 0; i < collisions.Count; i++)
            {
                if (collisions[i] == collision)
                    return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Internal class - we need it noly in the asset pack
    /// </summary>
    internal class Collision
    {
        public List<CollisionBox> collidedComponents;

        public Collision(List<CollisionBox> collidedComponents)
        {
            this.collidedComponents = collidedComponents;
        }

        /// <summary>
        /// Collisions considered equal if they contains same elements, without attaching to their order in the collisions list
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(Collision a, Collision b)
        {
            if ((a.collidedComponents[0] == b.collidedComponents[1] && a.collidedComponents[1] == b.collidedComponents[0]) || (
                   a.collidedComponents[0] == b.collidedComponents[0] && a.collidedComponents[1] == b.collidedComponents[1]))
                return true;
            else
                return false;
        }

        public static bool operator !=(Collision a, Collision b)
        {
            if ((a.collidedComponents[0] == b.collidedComponents[1] && a.collidedComponents[1] == b.collidedComponents[0]) || (
                   a.collidedComponents[0] == b.collidedComponents[0] && a.collidedComponents[1] == b.collidedComponents[1]))
                return false;
            else
                return true;
        }

        public override bool Equals([NotNullWhen(true)] object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public void CollisionStart()
        {
            collidedComponents[0].CollisionStarted(collidedComponents[1].GetParent());
            collidedComponents[1].CollisionStarted(collidedComponents[0].GetParent());
        }

        public void CollisionOngoing()
        {
            collidedComponents[0].CollisionOngoing(collidedComponents[1].GetParent());
            collidedComponents[1].CollisionOngoing(collidedComponents[0].GetParent());
        }

        public void CollisionEnded()
        {
            collidedComponents[0].CollisionEnded(collidedComponents[1].GetParent());
            collidedComponents[1].CollisionEnded(collidedComponents[0].GetParent());
        }

        public bool IsColliding()
        {
            return collidedComponents[0].CollisionRectangle.Intersects(collidedComponents[1].CollisionRectangle);
        }
    }
}
