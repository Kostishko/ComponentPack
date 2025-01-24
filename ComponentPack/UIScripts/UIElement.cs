using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace ComponentPack
{
    public class UIElement
    {

        public Rectangle rectangle; // Public just for test
        private Point position;
        public Point Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
                rectangle.Location = position;
            }
        }
        internal event EventHandler<UIPositionArg> ChangeMyPosition;

        protected Point centerPoint
        {
            get { return rectangle.Center; }
        }
       
        public bool IsActive = true; //think about the default 
        private bool isVisible = true;
        public bool IsVisible
        {
            get => isVisible;
            set
            {
                isVisible = value;
                IsActive = false;
                IsVisibleChanged?.Invoke(this, IsVisible);
            }
        }

        public event EventHandler<bool> IsVisibleChanged;

        private UIElement parent; //parent UI element 
        protected UIElement Parent
        {
            get
            {
                if (parent == this)
                {
                    return UIProcessor.UICanvas; // return placeholder UI element with a rectangle at 0,0 and bound's sizes
                }
                else
                {
                    return parent;
                }
            }
            set => parent = value;
        }
        //private Rectangle bounds; //if we do not have a parent element - we still need to know some bounds to align for

        /// <summary>
        /// attach the UI element to General Canvas
        /// </summary>
        /// <param name="position"></param>
        /// <param name="rectangle"></param>
        public UIElement(Point position, Rectangle rectangle)
        {
            this.rectangle = rectangle;
            parent = UIProcessor.UICanvas;
            Position = parent.Position + position;
            //ChangeMyPosition += AdjustMyPosition;
            //this.bounds = bounds;
            UIProcessor.AddNewElement(this);
        }

        /// <summary>
        /// Attack this UI element to particualr parent UI object
        /// </summary>
        /// <param name="position"></param>
        /// <param name="parent"></param>
        /// <param name="rectangle"></param>
        public UIElement(Point position, UIElement parent, Rectangle rectangle)
        {
            this.rectangle = rectangle;
            this.parent = parent;
            Position = parent.Position + position;
            //ChangeMyPosition += AdjustMyPosition;
            UIProcessor.AddNewElement(this);
            this.parent.IsVisibleChanged += this.ParentChangedIsVisibl;
        }

        /// <summary>
        /// This constructor is for UI canvas - the general object that cover whole screen.
        /// </summary>
        /// <param name="rectangle"></param>
        public UIElement(Rectangle rectangle)
        {
            this.rectangle = rectangle;
            //UIProcessor.AddNewElement(this); //I guess we don't need to do anything with this canvas
        }



        public virtual void UpdateMe()
        {

        }

        public virtual void DrawMe(SpriteBatch spriteBatch)
        {


        }

        private void ParentChangedIsVisibl(object o, bool visible)
        {
            IsVisible = visible;
            IsActive = visible;
        }

      
    }


    internal class UIPositionArg : EventArgs
    {
        Point position;
        internal UIPositionArg(Point position)
        {
            this.position = position;
        }
    }
}
