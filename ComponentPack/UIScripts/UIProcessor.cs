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
    public static class UIProcessor
    {

        //Meta That need to be loaded in Game1
        public static Point ScreenBounds;
        public static Texture2D MouseCoursorTexture;
        public static SpriteFont BigUIFont;

        public static UIElement UICanvas = new UIElement(new Rectangle(Point.Zero, ScreenBounds));

        //control input

        public enum UIControlScheme : byte
        {
            Keyboard,
            GamePad
        }

        public static UIControlScheme controlScheme;


        private static MouseState PrevMouseState;
        private static MouseState CurrMouseState;

        private static KeyboardState PrevKeyboardState;
        private static KeyboardState CurrKeyboardState;

        private static GamePadState PrevGamePadState;
        private static GamePadState CurrGamePadState;

        private const float INPUTLAGTIME = 0.2f;
        private static float inputLagTimer;
        public static float InputLagTimer
        {
            get => inputLagTimer;
            set
            {
                inputLagTimer = Math.Clamp(value, 0, INPUTLAGTIME);
            }
        }

        //cont staticrol output
        private static Point mousePos;
        public static Point MousePos
        {
            get => mousePos;
            set
            {
                mousePos = new Point(Math.Clamp(value.X, 0, ScreenBounds.X), Math.Clamp(value.Y, 0, ScreenBounds.Y));
            }
        }
        public static bool Up;
        public static bool Down;
        public static bool Left;
        public static bool Right;

        public static bool LeftMousePressed;
        public static bool LeftMouseClicked;
        public static bool RightMousePressed;
        public static bool RightMouseClicked;

        public static bool Accept;
        public static bool Decline;

        public static bool Back;

        private static List<UIElement> uiElements = new List<UIElement>();





        /// <summary>
        /// Update for UI processor in case of mouse / keyboard controll scheme
        /// </summary>        
        public static void UpdateMe(MouseState mouseState, KeyboardState keyboardState, GamePadState gamePadState)
        {
            CurrMouseState = mouseState;
            CurrKeyboardState = keyboardState;
            CurrGamePadState = gamePadState;


            //logic for update commands

            //logic for keyboard/mouse control scheme
            switch (controlScheme)
            {
                case UIControlScheme.Keyboard:
                    #region Keyboard Direction UI commands (Up, Down, Left, Right)

                    //UP
                    if (CurrKeyboardState.IsKeyDown(Keys.Up))
                    {
                        if (PrevKeyboardState.IsKeyUp(Keys.Up) || InputLagTimer == 0)
                        {
                            InputLagTimer = INPUTLAGTIME;
                            Up = true;
                        }
                        else
                        {
                            Up = false;
                        }
                    }
                    else
                    {
                        Up = false;
                    }

                    if (CurrKeyboardState.IsKeyDown(Keys.W))
                    {
                        if (PrevKeyboardState.IsKeyUp(Keys.W) || InputLagTimer == 0)
                        {
                            InputLagTimer = INPUTLAGTIME;
                            Up = true;
                        }
                        else
                        {
                            Up = false;
                        }
                    }
                    else
                    {
                        Up = false;
                    }

                    //DOWN
                    if (CurrKeyboardState.IsKeyDown(Keys.S))
                    {
                        if (PrevKeyboardState.IsKeyUp(Keys.S) || InputLagTimer == 0)
                        {
                            InputLagTimer = INPUTLAGTIME;
                            Down = true;
                        }
                        else
                        {
                            Down = false;
                        }
                    }
                    else
                    {
                        Down = false;
                    }

                    if (CurrKeyboardState.IsKeyDown(Keys.Down))
                    {
                        if (PrevKeyboardState.IsKeyUp(Keys.Down) || InputLagTimer == 0)
                        {
                            InputLagTimer = INPUTLAGTIME;
                            Down = true;
                        }
                        else
                        {
                            Down = false;
                        }
                    }
                    else
                    {
                        Down = false;
                    }

                    //LEFT
                    if (CurrKeyboardState.IsKeyDown(Keys.A))
                    {
                        if (PrevKeyboardState.IsKeyUp(Keys.A) || InputLagTimer == 0)
                        {
                            InputLagTimer = INPUTLAGTIME;
                            Left = true;
                        }
                        else
                        {
                            Left = false;
                        }
                    }
                    else
                    {
                        Left = false;
                    }

                    if (CurrKeyboardState.IsKeyDown(Keys.Left))
                    {
                        if (PrevKeyboardState.IsKeyUp(Keys.Left) || InputLagTimer == 0)
                        {
                            InputLagTimer = INPUTLAGTIME;
                            Left = true;
                        }
                        else
                        {
                            Left = false;
                        }
                    }
                    else
                    {
                        Left = false;
                    }

                    //RIGHT

                    if (CurrKeyboardState.IsKeyDown(Keys.D))
                    {
                        if (PrevKeyboardState.IsKeyUp(Keys.D) || InputLagTimer == 0)
                        {
                            InputLagTimer = INPUTLAGTIME;
                            Right = true;
                        }
                        else
                        {
                            Right = false;
                        }
                    }
                    else
                    {
                        Right = false;
                    }

                    if (CurrKeyboardState.IsKeyDown(Keys.Right))
                    {
                        if (PrevKeyboardState.IsKeyUp(Keys.Right) || InputLagTimer == 0)
                        {
                            InputLagTimer = INPUTLAGTIME;
                            Right = true;
                        }
                        else
                        {
                            Right = false;
                        }
                    }
                    else
                    {
                        Right = false;
                    }

                    #endregion

                    #region Keyboard Mouse UI Mouse commads

                    //Left button
                    if (CurrMouseState.LeftButton == ButtonState.Pressed)
                        LeftMousePressed = true;
                    else
                        LeftMousePressed = false;


                    if (CurrMouseState.LeftButton == ButtonState.Released && PrevMouseState.LeftButton == ButtonState.Pressed)
                        LeftMouseClicked = true;
                    else
                        LeftMouseClicked = false;

                    //Right button
                    if (CurrMouseState.RightButton == ButtonState.Pressed)
                        RightMousePressed = true;
                    else
                        RightMousePressed = false;


                    if (CurrMouseState.RightButton == ButtonState.Released && PrevMouseState.RightButton == ButtonState.Pressed)
                        RightMouseClicked = true;
                    else
                        RightMouseClicked = false;

                    MousePos = CurrMouseState.Position;

                    #endregion

                    #region Accept, Decline, Back

                    //Accept
                    if (CurrKeyboardState.IsKeyUp(Keys.Enter) && PrevKeyboardState.IsKeyDown(Keys.Enter))
                    {
                        Accept = true;
                        InputLagTimer = INPUTLAGTIME;
                    }
                    else
                        Accept = false;

                    //Decline
                    if (CurrKeyboardState.IsKeyUp(Keys.Back) && PrevKeyboardState.IsKeyDown(Keys.Back))
                    {
                        Decline = true;
                        InputLagTimer = INPUTLAGTIME;
                    }
                    else
                        Decline = false;

                    //Escape back
                    if (CurrKeyboardState.IsKeyUp(Keys.Escape) && PrevKeyboardState.IsKeyDown(Keys.Escape))
                    {
                        Back = true;
                        InputLagTimer = INPUTLAGTIME;
                    }
                    else
                        Back = false;


                    #endregion
                    break;

                //logic for GamePad
                case UIControlScheme.GamePad:
                    #region GamePad Direction UI commands (Up, Down, Left, Right)

                    //Up
                    if (CurrGamePadState.DPad.Up == ButtonState.Pressed)
                    {
                        if (PrevGamePadState.DPad.Up == ButtonState.Released || InputLagTimer == 0)
                        {
                            InputLagTimer = INPUTLAGTIME;
                            Up = true;
                        }
                        else
                        {
                            Up = false;
                        }
                    }
                    else
                    {
                        Up = false;
                    }

                    if (CurrGamePadState.ThumbSticks.Left.Y == 1 && InputLagTimer == 0)
                    {
                        InputLagTimer = INPUTLAGTIME;
                        Up = true;
                    }
                    else
                        Up = false;


                    //Down
                    if (CurrGamePadState.DPad.Down == ButtonState.Pressed)
                    {
                        if (PrevGamePadState.DPad.Down == ButtonState.Released || InputLagTimer == 0)
                        {
                            InputLagTimer = INPUTLAGTIME;
                            Down = true;
                        }
                        else
                        {
                            Down = false;
                        }
                    }
                    else
                    {
                        Down = false;
                    }

                    if (CurrGamePadState.ThumbSticks.Left.Y == -1 && InputLagTimer == 0)
                    {
                        InputLagTimer = INPUTLAGTIME;
                        Down = true;
                    }
                    else
                        Down = false;

                    //Left

                    if (CurrGamePadState.DPad.Left == ButtonState.Pressed)
                    {
                        if (PrevGamePadState.DPad.Left == ButtonState.Released || InputLagTimer == 0)
                        {
                            InputLagTimer = INPUTLAGTIME;
                            Left = true;
                        }
                        else
                        {
                            Left = false;
                        }
                    }
                    else
                    {
                        Left = false;
                    }

                    if (CurrGamePadState.ThumbSticks.Left.X == -1 && InputLagTimer == 0)
                    {
                        InputLagTimer = INPUTLAGTIME;
                        Left = true;
                    }
                    else
                        Left = false;

                    //Right

                    if (CurrGamePadState.DPad.Right == ButtonState.Pressed)
                    {
                        if (PrevGamePadState.DPad.Right == ButtonState.Released || InputLagTimer == 0)
                        {
                            InputLagTimer = INPUTLAGTIME;
                            Right = true;
                        }
                        else
                        {
                            Right = false;
                        }
                    }
                    else
                    {
                        Right = false;
                    }

                    if (CurrGamePadState.ThumbSticks.Left.X == 1 && InputLagTimer == 0)
                    {
                        InputLagTimer = INPUTLAGTIME;
                        Right = true;
                    }
                    else
                        Right = false;



                    #endregion

                    #region GamePad Mouse UI Mouse command

                    if (CurrGamePadState.Buttons.A == ButtonState.Pressed)
                        LeftMousePressed = true;
                    else
                        LeftMousePressed = false;


                    if (CurrGamePadState.Buttons.A == ButtonState.Released && PrevGamePadState.Buttons.A == ButtonState.Pressed)
                        LeftMouseClicked = true;
                    else
                        LeftMouseClicked = false;

                    MousePos += CurrGamePadState.ThumbSticks.Left.ToPoint();

                    #endregion

                    #region Accept, Decline, Back

                    if (CurrGamePadState.Buttons.A == ButtonState.Released && PrevGamePadState.Buttons.A == ButtonState.Pressed && InputLagTimer == 0)
                    {
                        inputLagTimer = INPUTLAGTIME;
                        Accept = true;
                    }
                    else
                        Accept = false;

                    if (CurrGamePadState.Buttons.B == ButtonState.Released && PrevGamePadState.Buttons.B == ButtonState.Pressed && InputLagTimer == 0)
                    {
                        inputLagTimer = INPUTLAGTIME;
                        Decline = true;
                    }
                    else
                        Decline = false;

                    if (CurrGamePadState.Buttons.Back == ButtonState.Released && PrevGamePadState.Buttons.Back == ButtonState.Pressed && InputLagTimer == 0)
                    {
                        inputLagTimer = INPUTLAGTIME;
                        Back = true;
                    }
                    else
                        Back = false;

                    #endregion
                    break;
            }

            InputLagTimer -= (float)Extentions.TotalSeconds;
            //logic for UI elements

            for (int i = 0; i < uiElements.Count; i++)
            {
                if (uiElements[i].IsVisible)
                    uiElements[i].UpdateMe();
            }


            PrevGamePadState = CurrGamePadState;
            PrevMouseState = CurrMouseState;
            PrevKeyboardState = CurrKeyboardState;


        }





        public static void DrawMe(SpriteBatch sp)
        {
            for (int i = 0; i < uiElements.Count; i++)
            {
                if (uiElements[i].IsVisible)
                    uiElements[i].DrawMe(sp);
            }

            if (!LeftMousePressed)
                sp.Draw(MouseCoursorTexture, MousePos.ToVector2(), Color.White);
            else
                sp.Draw(MouseCoursorTexture, MousePos.ToVector2(), Color.LightCoral);
        }


        internal static void AddNewElement(UIElement element)
        {
            uiElements.Add(element);
        }

        public static void RemoveElement(UIElement element)
        {
            uiElements.Remove(element);
        }


    }
}
