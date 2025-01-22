using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace ComponentPack
{
    /// <summary>
    /// Interface that class need to implement if it's should be a part of GameObject
    /// </summary>
    public interface IComponent
    {

        /// <summary>
        /// Every Icomponent should contain a link to the parent GameObject
        /// </summary>
        public GameObject Parent
        {
            get;
            set;
        }

        /// <summary>
        /// Call every frame by parent GameObject
        /// </summary>
        public abstract void UpdateMe();

        /// <summary>
        /// Call every draw frame by parent GameObject
        /// </summary>
        /// <param name="sp"></param>
        public abstract void DrawMe(SpriteBatch sp);

        /// <summary>
        /// Call by parent GameObjec when it deleted themselves (or can be called particularly for this IComponent)
        /// </summary>
        public abstract void DeleteMe();

    }
}
