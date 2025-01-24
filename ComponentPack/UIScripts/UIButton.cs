using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace ComponentPack.UIScripts
{
    public class UIButton : UIElement
    {

        private Texture2D buttonTexture;
        private Rectangle? sourceRectangle;
        private string buttonName;
        private string ButtonText;
        private SpriteFont buttonTextFont;

        public event EventHandler<string> Clicked;

        private bool IsSetted = false;
        private Point prevMousePos;

        public enum ButtonStates : byte
        {
            active,
            inactive,
            highlighted,
            pressed
        }

        public ButtonStates State;

        public UIButton(Point position, UIElement parent, Rectangle rectangle, Texture2D texture, Rectangle? sourceRectangle, SpriteFont buttonFont, string buttonName, string buttonText) : base(position, parent, rectangle)
        {
            this.buttonName = buttonName;
            this.ButtonText = buttonText;
            this.buttonTexture = texture;
            buttonTextFont = buttonFont;
            this.sourceRectangle = sourceRectangle;

        }

        public UIButton(Point position, Rectangle rectangle, Texture2D texture, Rectangle? sourceRectangle, SpriteFont buttonFont, string buttonName, string buttonText) : base(position, rectangle)
        {
            this.buttonName = buttonName;
            this.ButtonText = buttonText;
            this.buttonTexture = texture;
            buttonTextFont = buttonFont;
            this.sourceRectangle = sourceRectangle;

        }

        public override void UpdateMe()
        {


            if (!IsSetted)
            {
                if (IsActive)
                {

                    if (!rectangle.Contains(UIProcessor.MousePos))
                    {
                        State = ButtonStates.active;
                    }

                    if (rectangle.Contains(UIProcessor.MousePos) && !UIProcessor.LeftMousePressed)
                    {
                        State = ButtonStates.highlighted;
                    }

                    if (rectangle.Contains(UIProcessor.MousePos) && UIProcessor.LeftMousePressed)
                    {
                        State = ButtonStates.pressed;
                    }

                }
                else
                {
                    State = ButtonStates.inactive;
                }

                if (rectangle.Contains(UIProcessor.MousePos) && UIProcessor.LeftMouseClicked)
                {
                    if (State != ButtonStates.inactive)
                    {
                        Clicked?.Invoke(this, buttonName);
                        Debug.WriteLine("I'm cliecked! " + buttonName);
                    }
                }
            }
            else
            {
                if (UIProcessor.MousePos != prevMousePos)
                    IsSetted = false;
            }

            prevMousePos = UIProcessor.MousePos;
        }

        public override void DrawMe(SpriteBatch spriteBatch)
        {
            //Drawning the button background in different states
            switch (State)
            {
                case ButtonStates.active:
                    spriteBatch.Draw(buttonTexture, rectangle, sourceRectangle, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
                    spriteBatch.DrawString(UIProcessor.BigUIFont, ButtonText,
                new Vector2(rectangle.X + rectangle.Width / 2 - UIProcessor.BigUIFont.MeasureString(ButtonText).X / 2,
                rectangle.Y + rectangle.Height / 2 - UIProcessor.BigUIFont.MeasureString(ButtonText).Y / 3),
                Color.Yellow);
                    break;
                case ButtonStates.highlighted:
                    spriteBatch.Draw(buttonTexture, rectangle, sourceRectangle, Color.Gold, 0, Vector2.Zero, SpriteEffects.None, 0);
                    spriteBatch.DrawString(UIProcessor.BigUIFont, ButtonText,
                new Vector2(rectangle.X + rectangle.Width / 2 - UIProcessor.BigUIFont.MeasureString(ButtonText).X / 2,
                rectangle.Y + rectangle.Height / 2 - UIProcessor.BigUIFont.MeasureString(ButtonText).Y / 3),
                Color.Yellow);
                    break;
                case ButtonStates.inactive:
                    spriteBatch.Draw(buttonTexture, rectangle, sourceRectangle, Color.BlueViolet, 0, Vector2.Zero, SpriteEffects.None, 0);
                    spriteBatch.DrawString(UIProcessor.BigUIFont, ButtonText,
                new Vector2(rectangle.X + rectangle.Width / 2 - UIProcessor.BigUIFont.MeasureString(ButtonText).X / 2,
                rectangle.Y + rectangle.Height / 2 - UIProcessor.BigUIFont.MeasureString(ButtonText).Y / 3),
                Color.BlueViolet);
                    break;
                case ButtonStates.pressed:
                    spriteBatch.Draw(buttonTexture, rectangle, sourceRectangle, Color.Gray, 0, Vector2.Zero, SpriteEffects.None, 0);
                    spriteBatch.DrawString(UIProcessor.BigUIFont, ButtonText,
                new Vector2(rectangle.X + rectangle.Width / 2 - UIProcessor.BigUIFont.MeasureString(ButtonText).X / 2,
                rectangle.Y + rectangle.Height / 2 - UIProcessor.BigUIFont.MeasureString(ButtonText).Y / 3),
                Color.Yellow);
                    break;
            }
            //text drawning




        }

        //initiate click through a Button Menu
        public void ClickMe()
        {
            Clicked?.Invoke(this, ButtonText);
            Debug.WriteLine("I'm cliecked! " + buttonName);
        }

        public void SetState(ButtonStates state)
        {
            this.State = state;
            IsSetted = true;
        }
    }
}
