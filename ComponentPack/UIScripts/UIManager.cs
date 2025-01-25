using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;    
using System.Collections.Generic;



namespace ComponentPack
{
    internal class UIManager
    {
        #region UI meta data
        //metadata UI
        Point screenBorders;
        SpriteFont titleFont, bigFont, smallFont;


        UIPanel currentPanel;
        public UIPanel CurrentPanel
        {
            get => currentPanel;
            set
            {
                currentPanel.IsVisible = false;
                currentPanel = value;
                currentPanel.IsVisible = true;
            }
        }

        public static Texture2D coursorTarget;

        #endregion

        #region UI elements

        //main menu
        public UIText mainMenuText;
        public UIButton newGameButton;
        public UIButton continueButton;
        public UIButton settingsButton;
        public UIButton creatorsButton;
        public UIButton recordsButton;
        public UIButton instructionButton;
        public UIButton exitButton;
        public UIPanel mainMenuPanel;

        //settings        
        public UIText settingsText;
        public UIText musicVolumeText;
        public UIText soundVolumeText;
        public UIButton upSound;
        public UIButton downSound;
        public UIButton upMusic;
        public UIButton downMusic;
        public UIButton backSettingsButton;
        public UIPanel settingsPanel;

        //creators
        private UIText creatorsTitleText;
        private UIText creatorsText;
        public UIButton BackCreatorsButton;
        public UIPanel creatorsPanel;

        //score records
        public List<UIText> records;
        public UIButton backRecordsButton;
        public UIPanel scorePanel;
        public UIText scoreTitle;

        //gameplay interface
        public UIPanel gameplayLayoutPanel;
        public Texture2D heartTexture;

        //instructions
        public UIText instructionsTitle;
        public UIText instructionsText;
        public UIButton backInstructionButton;
        public UIPanel instructionPanel;


        //pause
        public UIPanel pausePanel;
        public UIText pauseTitleText;
        public UIButton pauseBackToGameButton;
        public UIButton pauseBackToMainMenuButton;

        //Won
        public UIPanel winPanel;
        public UIText winTitleText;
        public UIText winDescriptionText;
        public UIButton winBackToMainMenuButton;

        //lose
        public UIPanel losePanel;
        public UIText loseTitleText;
        //public UIText loseDescriptionText;
        public UIButton loseBackToMainMenuButton;

        #endregion



        public UIManager(ContentManager content, Point screenBorders, Game1 game)
        {
            //metadata
            this.screenBorders = screenBorders;
            titleFont = content.Load<SpriteFont>("Fonts/TitleFont");
            bigFont = content.Load<SpriteFont>("Fonts/UIFontBig");
            smallFont = content.Load<SpriteFont>("Fonts/UIFontSmall");
            coursorTarget = content.Load<Texture2D>("Sprites/CursorTarget");

            #region main menu
            //main menu initialisation
            mainMenuPanel = new UIPanel(new Point(UIProcessor.ScreenBounds.X * 1 / 4, UIProcessor.ScreenBounds.Y * 1 / 7),
                                        new Rectangle(0, 0, UIProcessor.ScreenBounds.X / 2, UIProcessor.ScreenBounds.Y * 5 / 7),
                                            UIProcessor.UICanvas, content.Load<Texture2D>("Sprites/RectangleBox"));

            currentPanel = mainMenuPanel; // main menu panel is default panel

            mainMenuText = new UIText(new Point(mainMenuPanel.rectangle.Width / 2 - (int)Math.Round(titleFont.MeasureString("Main Menu").X / 2), -(int)Math.Round(titleFont.MeasureString("Main Menu").Y / 2)),
                                        new Rectangle(Point.Zero, titleFont.MeasureString("Main Menu").ToPoint()),
                                        mainMenuPanel, titleFont, "Main Menu");

            newGameButton = new UIButton(new Point(mainMenuPanel.rectangle.Width / 8, mainMenuPanel.rectangle.Height / 7),
                                      mainMenuPanel,
                                      new Rectangle(0, 0, mainMenuPanel.rectangle.Width * 3 / 4, mainMenuPanel.rectangle.Height / 6),
                                      content.Load<Texture2D>("C_NewGame1"), new Rectangle(0, 0, 96, 32),
                                      content.Load<SpriteFont>("Fonts/UIFontBig"), "newGame", "New Game");

            continueButton = new UIButton(new Point(mainMenuPanel.rectangle.Width / 8, mainMenuPanel.rectangle.Height * 2 / 7),
                                     mainMenuPanel,
                                     new Rectangle(0, 0, mainMenuPanel.rectangle.Width * 3 / 4, mainMenuPanel.rectangle.Height / 6),
                                     content.Load<Texture2D>("C_Continue1"), new Rectangle(0, 0, 96, 32),
                                     content.Load<SpriteFont>("Fonts/UIFontBig"), "continue", "Continue");

            settingsButton = new UIButton(new Point(mainMenuPanel.rectangle.Width / 8, (int)Math.Round(mainMenuPanel.rectangle.Height * 3f / 7)),
                                      mainMenuPanel,
                                      new Rectangle(0, 0, mainMenuPanel.rectangle.Width * 3 / 8, mainMenuPanel.rectangle.Height / 6),
                                      content.Load<Texture2D>("A_Settings1"), new Rectangle(0, 0, 96, 32),
                                      content.Load<SpriteFont>("Fonts/UIFontBig"), "settings", "Settings");

            //creatorsbutton = new uibutton(new point(mainmenupanel.rectangle.width * 4 / 8, (int)math.round(mainmenupanel.rectangle.height * 3f / 7)),
            //                          mainmenupanel,
            //                          new rectangle(0, 0, mainmenupanel.rectangle.width * 3 / 8, mainmenupanel.rectangle.height / 6),
            //                          content.load<texture2d>("sprites/part 3 a"), new rectangle(0, 0, 68, 36),
            //                          content.load<spritefont>("fonts/uifontbig"), "creators", "creators");

            //instructionButton = new UIButton(new Point(mainMenuPanel.rectangle.Width / 8, (int)Math.Round(mainMenuPanel.rectangle.Height * 4f / 7)),
            //                          mainMenuPanel,
            //                          new Rectangle(0, 0, mainMenuPanel.rectangle.Width * 3 / 8, mainMenuPanel.rectangle.Height / 6),
            //                          content.Load<Texture2D>("Sprites/Part 3 A"), new Rectangle(0, 0, 68, 36),
            //                          content.Load<SpriteFont>("Fonts/UIFontBig"), "instructions", "Instruction");

            recordsButton = new UIButton(new Point(mainMenuPanel.rectangle.Width * 4 / 8, (int)Math.Round(mainMenuPanel.rectangle.Height * 4f / 7)),
                                      mainMenuPanel,
                                      new Rectangle(0, 0, mainMenuPanel.rectangle.Width * 3 / 8, mainMenuPanel.rectangle.Height / 6),
                                      content.Load<Texture2D>("C_info1"), new Rectangle(0, 0, 96, 32),
                                      content.Load<SpriteFont>("Fonts/UIFontBig"), "records", "Records");


            exitButton = new UIButton(new Point(mainMenuPanel.rectangle.Width / 4, (int)Math.Round(mainMenuPanel.rectangle.Height * 5f / 7)),
                                      mainMenuPanel,
                                      new Rectangle(0, 0, mainMenuPanel.rectangle.Width / 2, mainMenuPanel.rectangle.Height / 6),
                                      content.Load<Texture2D>("A_Exit1"), new Rectangle(0, 0, 96, 32),
                                      content.Load<SpriteFont>("Fonts/UIFontBig"), "exit", "Exit");

            newGameButton.Clicked += (s, e) => {
               
            };
            continueButton.Clicked += (s, e) => {
               
            };
            settingsButton.Clicked += (s, e) => { };
            recordsButton.Clicked += (s, e) => { };
            instructionButton.Clicked += (s, e) => {};

            exitButton.Clicked += (s, e) => {  };
            creatorsButton.Clicked += (s, e) =>
            {
            };

            //block for not ready buttons

            //continueButton.IsActive = IsSaveGameExist();


            #endregion

            #region settings
            settingsPanel = new UIPanel(
            new Point(UIProcessor.ScreenBounds.X * 1 / 4, UIProcessor.ScreenBounds.Y * 1 / 7),
            new Rectangle(0, 0, UIProcessor.ScreenBounds.X / 2, UIProcessor.ScreenBounds.Y * 5 / 7),
            UIProcessor.UICanvas,
            content.Load<Texture2D>("Sprites/RectangleBox"));

            settingsText = new UIText(
            new Point(settingsPanel.rectangle.Width / 2 - (int)Math.Round(titleFont.MeasureString("Settings").X / 2), -(int)Math.Round(titleFont.MeasureString("Settings").Y / 2)),
            new Rectangle(Point.Zero, titleFont.MeasureString("Settings").ToPoint()),
            settingsPanel,
            titleFont,
            "Settings");

            musicVolumeText = new UIText(
            new Point(settingsPanel.rectangle.Width / 2 - (int)Math.Round(bigFont.MeasureString("Music Volume").X / 2), settingsPanel.rectangle.Height * 3 / 16),
            new Rectangle(Point.Zero,
            bigFont.MeasureString("Music Volume").ToPoint()),
            settingsPanel,
            bigFont,
            "Music Volume");

            soundVolumeText = new UIText(
            new Point(settingsPanel.rectangle.Width / 2 - (int)Math.Round(bigFont.MeasureString("Sound Volume").X / 2), settingsPanel.rectangle.Height * 8 / 16),
            new Rectangle(Point.Zero,
            bigFont.MeasureString("Sound Volume").ToPoint()),
            settingsPanel,
            bigFont,
            "Sound Volume");

            downSound = new UIButton(new Point(settingsPanel.rectangle.Width * 6 / 16, settingsPanel.rectangle.Height * 9 / 16),
                                     settingsPanel,
                                     new Rectangle(0, 0, 50, 50),
                                     content.Load<Texture2D>("Sprites/Part 3 A"), new Rectangle(0, 0, 68, 36),
                                     content.Load<SpriteFont>("Fonts/UIFontBig"), "downSound", "-");

            upSound = new UIButton(new Point(settingsPanel.rectangle.Width * 9 / 16, settingsPanel.rectangle.Height * 9 / 16),
                         settingsPanel,
                         new Rectangle(0, 0, 50, 50),
                         content.Load<Texture2D>("Sprites/Part 3 A"), new Rectangle(0, 0, 68, 36),
                         content.Load<SpriteFont>("Fonts/UIFontBig"), "upSound", "+");


            downMusic = new UIButton(new Point(settingsPanel.rectangle.Width * 6 / 16, settingsPanel.rectangle.Height * 4 / 16),
                         settingsPanel,
                         new Rectangle(0, 0, 50, 50),
                         content.Load<Texture2D>("Sprites/Part 3 A"), new Rectangle(0, 0, 68, 36),
                         content.Load<SpriteFont>("Fonts/UIFontBig"), "downMusic", "-");

            upMusic = new UIButton(new Point(settingsPanel.rectangle.Width * 9 / 16, settingsPanel.rectangle.Height * 4 / 16),
                        settingsPanel,
                        new Rectangle(0, 0, 50, 50),
                        content.Load<Texture2D>("Sprites/Part 3 A"), new Rectangle(0, 0, 68, 36),
                        content.Load<SpriteFont>("Fonts/UIFontBig"), "upMusic", "+");

            backSettingsButton = new UIButton(new Point(settingsPanel.rectangle.Width / 4, (int)Math.Round(settingsPanel.rectangle.Height * 5f / 7)),
                                      settingsPanel,
                                      new Rectangle(0, 0, settingsPanel.rectangle.Width / 2, settingsPanel.rectangle.Height / 6),
                                      content.Load<Texture2D>("Sprites/Part 3 A"), new Rectangle(0, 0, 68, 36),
                                      content.Load<SpriteFont>("Fonts/UIFontBig"), "back", "Back");

            backSettingsButton.Clicked += (s, e) =>
            {

            };

            downMusic.Clicked += (s, e) => { };
            upMusic.Clicked += (s, e) => { };
            downSound.Clicked += (s, e) => { };
            upSound.Clicked += (s, e) => { };
            settingsPanel.IsVisible = false;

            #endregion

            #region records

            scorePanel = new UIPanel(
            new Point(UIProcessor.ScreenBounds.X * 1 / 4, UIProcessor.ScreenBounds.Y * 1 / 7),
            new Rectangle(0, 0, UIProcessor.ScreenBounds.X / 2, UIProcessor.ScreenBounds.Y * 5 / 7),
            UIProcessor.UICanvas,
            content.Load<Texture2D>("Sprites/RectangleBox"));
            ScoreArrange();
            backRecordsButton = new UIButton(new Point(scorePanel.rectangle.Width / 4, (int)Math.Round(scorePanel.rectangle.Height * 5f / 7)),
                                      scorePanel,
                                      new Rectangle(0, 0, scorePanel.rectangle.Width / 2, scorePanel.rectangle.Height / 6),
                                      content.Load<Texture2D>("Sprites/Part 3 A"), new Rectangle(0, 0, 68, 36),
                                      content.Load<SpriteFont>("Fonts/UIFontBig"), "back", "Back");

            backRecordsButton.Clicked += (s, e) =>
            {
            };

            scoreTitle = new UIText(
            new Point(scorePanel.rectangle.Width / 2 - (int)Math.Round(titleFont.MeasureString("Score").X / 2), -(int)Math.Round(titleFont.MeasureString("Score").Y / 2)),
            new Rectangle(Point.Zero, titleFont.MeasureString("Score").ToPoint()),
            scorePanel,
            titleFont,
            "Score");

            scorePanel.IsVisible = false;

            //    public List<UIText> records;
            //public Button backRecordsButton;





            //records

            #endregion

            #region creators
            //creators


            creatorsPanel = new UIPanel(new Point(UIProcessor.ScreenBounds.X * 1 / 4, UIProcessor.ScreenBounds.Y * 1 / 7),
                                        new Rectangle(0, 0, UIProcessor.ScreenBounds.X / 2, UIProcessor.ScreenBounds.Y * 5 / 7),
                                            UIProcessor.UICanvas, content.Load<Texture2D>("Sprites/RectangleBox"));

            creatorsTitleText = new UIText(new Point(creatorsPanel.rectangle.Width / 2 - (int)Math.Round(titleFont.MeasureString("Creators").X / 2), -(int)Math.Round(titleFont.MeasureString("Creators").Y / 2)),
                                        new Rectangle(Point.Zero, titleFont.MeasureString("Creators").ToPoint()),
                                        creatorsPanel, titleFont, "Creators");



            //creatorsText = new UIText();

            BackCreatorsButton = new UIButton(new Point(creatorsPanel.rectangle.Width / 4, (int)Math.Round(creatorsPanel.rectangle.Height * 5f / 7)),
                                      creatorsPanel,
                                      new Rectangle(0, 0, creatorsPanel.rectangle.Width / 2, creatorsPanel.rectangle.Height / 6),
                                      content.Load<Texture2D>("Sprites/Part 3 A"), new Rectangle(0, 0, 68, 36),
                                      content.Load<SpriteFont>("Fonts/UIFontBig"), "back", "Back");

            BackCreatorsButton.Clicked += (s, e) =>
            {
            };
            creatorsPanel.IsVisible = false;


            #endregion

            #region instruction

            //creators


            instructionPanel = new UIPanel(new Point(UIProcessor.ScreenBounds.X * 1 / 4, UIProcessor.ScreenBounds.Y * 1 / 7),
                                        new Rectangle(0, 0, UIProcessor.ScreenBounds.X / 2, UIProcessor.ScreenBounds.Y * 5 / 7),
                                            UIProcessor.UICanvas, content.Load<Texture2D>("Sprites/RectangleBox"));

            instructionsTitle = new UIText(new Point(instructionPanel.rectangle.Width / 2 - (int)Math.Round(titleFont.MeasureString("Instruction").X / 2), -(int)Math.Round(titleFont.MeasureString("Instruction").Y / 2)),
                                        new Rectangle(Point.Zero, titleFont.MeasureString("Instruction").ToPoint()),
                                        instructionPanel, titleFont, "Instruction");


            backInstructionButton = new UIButton(new Point(instructionPanel.rectangle.Width / 4, (int)Math.Round(instructionPanel.rectangle.Height * 5f / 7)),
                                      instructionPanel,
                                      new Rectangle(0, 0, instructionPanel.rectangle.Width / 2, instructionPanel.rectangle.Height / 6),
                                      content.Load<Texture2D>("Sprites/Part 3 A"), new Rectangle(0, 0, 68, 36),
                                      content.Load<SpriteFont>("Fonts/UIFontBig"), "back", "Back");

            backInstructionButton.Clicked += (s, e) =>
            {
            };
            instructionPanel.IsVisible = false;

            #endregion

            #region Pause

            pausePanel = new UIPanel(new Point(UIProcessor.ScreenBounds.X * 1 / 4, UIProcessor.ScreenBounds.Y * 1 / 7),
                                        new Rectangle(0, 0, UIProcessor.ScreenBounds.X / 2, UIProcessor.ScreenBounds.Y * 4 / 7),
                                            UIProcessor.UICanvas, content.Load<Texture2D>("Sprites/RectangleBox"));
            pauseTitleText = new UIText(new Point(pausePanel.rectangle.Width / 2 - (int)Math.Round(titleFont.MeasureString("Pause").X / 2), -(int)Math.Round(titleFont.MeasureString("Pause").Y / 2)),
                                        new Rectangle(Point.Zero, titleFont.MeasureString("Pause").ToPoint()),
                                        pausePanel, titleFont, "Pause");
            pauseBackToGameButton = new UIButton(new Point(pausePanel.rectangle.Width / 8, (int)Math.Round(pausePanel.rectangle.Height * 3f / 7)),
                                      pausePanel,
                                      new Rectangle(0, 0, pausePanel.rectangle.Width * 3 / 8, pausePanel.rectangle.Height / 6),
                                      content.Load<Texture2D>("Sprites/Part 3 A"), new Rectangle(0, 0, 68, 36),
                                      content.Load<SpriteFont>("Fonts/UIFontBig"), "backToGame", "Resume");
            pauseBackToMainMenuButton = new UIButton(new Point(pausePanel.rectangle.Width * 4 / 8, (int)Math.Round(pausePanel.rectangle.Height * 3f / 7)),
                                      pausePanel,
                                      new Rectangle(0, 0, pausePanel.rectangle.Width * 3 / 8, pausePanel.rectangle.Height / 6),
                                      content.Load<Texture2D>("Sprites/Part 3 A"), new Rectangle(0, 0, 68, 36),
                                      content.Load<SpriteFont>("Fonts/UIFontBig"), "backToMainMenu", "Exit");

            pausePanel.IsVisible = false;

            pauseBackToMainMenuButton.Clicked += (s, e) =>
            {
            };
            pauseBackToGameButton.Clicked += (s, e) => { };

            #endregion

            #region Gameplay UI
            gameplayLayoutPanel = new UIPanel(Point.Zero, new Rectangle(Point.Zero, screenBorders), UIProcessor.UICanvas);
            heartTexture = Game1.CommonContent.Load<Texture2D>("Sprites/ui_heart_full");
            #endregion

            #region Win
            //Won
            winPanel = new UIPanel(new Point(UIProcessor.ScreenBounds.X * 1 / 4, UIProcessor.ScreenBounds.Y * 1 / 7),
                                            new Rectangle(0, 0, UIProcessor.ScreenBounds.X / 2, UIProcessor.ScreenBounds.Y * 4 / 7),
                                                UIProcessor.UICanvas, content.Load<Texture2D>("Sprites/RectangleBox"));
            winTitleText = new UIText(new Point(winPanel.rectangle.Width / 2 - (int)Math.Round(titleFont.MeasureString("Win").X / 2), -(int)Math.Round(titleFont.MeasureString("Win").Y / 2)),
                                            new Rectangle(Point.Zero, titleFont.MeasureString("Win").ToPoint()),
                                            winPanel, titleFont, "Win");
            winDescriptionText = new UIText(new Point(winPanel.rectangle.Width / 2 - (int)Math.Round(titleFont.MeasureString("You WON!").X / 2), winPanel.rectangle.Height / 2 - (int)Math.Round(titleFont.MeasureString("You WON!").Y / 2)),
                                            new Rectangle(Point.Zero, titleFont.MeasureString("You WON!").ToPoint()),
                                            winPanel, titleFont, "You WON!");
            winBackToMainMenuButton = new UIButton(new Point(winPanel.rectangle.Width * 3 / 8, (int)Math.Round(winPanel.rectangle.Height * 5f / 7)),
                                          winPanel,
                                           new Rectangle(0, 0, winPanel.rectangle.Width * 3 / 8, winPanel.rectangle.Height / 6),
                                          content.Load<Texture2D>("Sprites/Part 3 A"), new Rectangle(0, 0, 68, 36),
                                          content.Load<SpriteFont>("Fonts/UIFontBig"), "backToMainMenu", "Exit");
            winBackToMainMenuButton.Clicked += (s, e) =>
            {             
            };
            winPanel.IsVisible = false;
            #endregion

            #region Lose
            //lose
            losePanel = new UIPanel(new Point(UIProcessor.ScreenBounds.X * 1 / 4, UIProcessor.ScreenBounds.Y * 1 / 7),
                                        new Rectangle(0, 0, UIProcessor.ScreenBounds.X / 2, UIProcessor.ScreenBounds.Y * 4 / 7),
                                            UIProcessor.UICanvas, content.Load<Texture2D>("Sprites/RectangleBox"));
            loseTitleText = new UIText(new Point(losePanel.rectangle.Width / 2 - (int)Math.Round(titleFont.MeasureString("Lose").X / 2), -(int)Math.Round(titleFont.MeasureString("Lose").Y / 2)),
                                            new Rectangle(Point.Zero, titleFont.MeasureString("Lose").ToPoint()),
                                            losePanel, titleFont, "Lose");
            //loseDescriptionText = new UIText(new Point(losePanel.rectangle.Width / 2 - (int)Math.Round(titleFont.MeasureString("Oh no, you lose!").X / 2), losePanel.rectangle.Height / 2 - (int)Math.Round(titleFont.MeasureString("Oh no, you lose!").Y / 2)),
            //                                new Rectangle(Point.Zero, titleFont.MeasureString("Oh no, you lose!").ToPoint()),
            //                                losePanel, this.titleFont, "Oh no, you lose!");
            loseBackToMainMenuButton = new UIButton(new Point(losePanel.rectangle.Width * 3 / 8, (int)Math.Round(losePanel.rectangle.Height * 5f / 7)),
                                          losePanel,
                                          new Rectangle(0, 0, losePanel.rectangle.Width * 3 / 8, losePanel.rectangle.Height / 6),
                                          content.Load<Texture2D>("Sprites/Part 3 A"), new Rectangle(0, 0, 68, 36),
                                          content.Load<SpriteFont>("Fonts/UIFontBig"), "backToMainMenu", "Exit");

            loseBackToMainMenuButton.Clicked += (s, e) =>
            {
                


            };
            losePanel.IsVisible = false;

            #endregion


            Game1.pauseForUI += (s, e) => { CurrentPanel = pausePanel; };

        }


        //Chack if there is a saved game
        public bool IsSaveGameExist()
        {
          return false;

        }

        /// <summary>
        /// Totally a work around thing. For dynamically changing elements
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void DrawMe(SpriteBatch spriteBatch)
        {
            if (currentPanel == settingsPanel)
            {
               
            }

            if (currentPanel == losePanel)
            {
                
            }

            if (currentPanel == gameplayLayoutPanel)
            {
               

            }

            //absolutely terrible wqay to provide text =(. Didn't have enough time at all!
            if (currentPanel == creatorsPanel)
            {
               

            }

            if (currentPanel == instructionPanel)
            {
               

            }


        }

        public void ScoreArrange()
        {

        }



    }

}
