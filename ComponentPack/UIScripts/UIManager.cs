using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace ComponentPack.UIScripts
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

            newGameButton = new Button(new Point(mainMenuPanel.rectangle.Width / 8, mainMenuPanel.rectangle.Height / 7),
                                      mainMenuPanel,
                                      new Rectangle(0, 0, mainMenuPanel.rectangle.Width * 3 / 4, mainMenuPanel.rectangle.Height / 6),
                                      content.Load<Texture2D>("Sprites/Part 3 A"), new Rectangle(0, 0, 68, 36),
                                      content.Load<SpriteFont>("Fonts/UIFontBig"), "newGame", "New Game");

            continueButton = new Button(new Point(mainMenuPanel.rectangle.Width / 8, mainMenuPanel.rectangle.Height * 2 / 7),
                                     mainMenuPanel,
                                     new Rectangle(0, 0, mainMenuPanel.rectangle.Width * 3 / 4, mainMenuPanel.rectangle.Height / 6),
                                     content.Load<Texture2D>("Sprites/Part 3 A"), new Rectangle(0, 0, 68, 36),
                                     content.Load<SpriteFont>("Fonts/UIFontBig"), "continue", "Continue");

            settingsButton = new Button(new Point(mainMenuPanel.rectangle.Width / 8, (int)Math.Round(mainMenuPanel.rectangle.Height * 3f / 7)),
                                      mainMenuPanel,
                                      new Rectangle(0, 0, mainMenuPanel.rectangle.Width * 3 / 8, mainMenuPanel.rectangle.Height / 6),
                                      content.Load<Texture2D>("Sprites/Part 3 A"), new Rectangle(0, 0, 68, 36),
                                      content.Load<SpriteFont>("Fonts/UIFontBig"), "settings", "Settings");

            creatorsButton = new Button(new Point(mainMenuPanel.rectangle.Width * 4 / 8, (int)Math.Round(mainMenuPanel.rectangle.Height * 3f / 7)),
                                      mainMenuPanel,
                                      new Rectangle(0, 0, mainMenuPanel.rectangle.Width * 3 / 8, mainMenuPanel.rectangle.Height / 6),
                                      content.Load<Texture2D>("Sprites/Part 3 A"), new Rectangle(0, 0, 68, 36),
                                      content.Load<SpriteFont>("Fonts/UIFontBig"), "creators", "Creators");

            instructionButton = new Button(new Point(mainMenuPanel.rectangle.Width / 8, (int)Math.Round(mainMenuPanel.rectangle.Height * 4f / 7)),
                                      mainMenuPanel,
                                      new Rectangle(0, 0, mainMenuPanel.rectangle.Width * 3 / 8, mainMenuPanel.rectangle.Height / 6),
                                      content.Load<Texture2D>("Sprites/Part 3 A"), new Rectangle(0, 0, 68, 36),
                                      content.Load<SpriteFont>("Fonts/UIFontBig"), "instructions", "Instruction");

            recordsButton = new Button(new Point(mainMenuPanel.rectangle.Width * 4 / 8, (int)Math.Round(mainMenuPanel.rectangle.Height * 4f / 7)),
                                      mainMenuPanel,
                                      new Rectangle(0, 0, mainMenuPanel.rectangle.Width * 3 / 8, mainMenuPanel.rectangle.Height / 6),
                                      content.Load<Texture2D>("Sprites/Part 3 A"), new Rectangle(0, 0, 68, 36),
                                      content.Load<SpriteFont>("Fonts/UIFontBig"), "records", "Records");


            exitButton = new Button(new Point(mainMenuPanel.rectangle.Width / 4, (int)Math.Round(mainMenuPanel.rectangle.Height * 5f / 7)),
                                      mainMenuPanel,
                                      new Rectangle(0, 0, mainMenuPanel.rectangle.Width / 2, mainMenuPanel.rectangle.Height / 6),
                                      content.Load<Texture2D>("Sprites/Part 3 A"), new Rectangle(0, 0, 68, 36),
                                      content.Load<SpriteFont>("Fonts/UIFontBig"), "exit", "Exit");

            newGameButton.Clicked += (s, e) => {
                CurrentPanel = gameplayLayoutPanel;
                Game1.game1.GameReload();
                Game1.game1.NewGameLoad();
            };
            continueButton.Clicked += (s, e) => {
                CurrentPanel = gameplayLayoutPanel;
                Game1.game1.GameReload();
                Game1.game1.GameLoad();
            };
            settingsButton.Clicked += (s, e) => { CurrentPanel = settingsPanel; };
            recordsButton.Clicked += (s, e) => { CurrentPanel = scorePanel; };
            instructionButton.Clicked += (s, e) => { CurrentPanel = instructionPanel; };

            exitButton.Clicked += (s, e) => { game.Exit(); };
            creatorsButton.Clicked += (s, e) =>
            {
                CurrentPanel = creatorsPanel;
                BackCreatorsButton.IsActive = true;
            };

            //block for not ready buttons

            continueButton.IsActive = IsSaveGameExist();


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

            downSound = new Button(new Point(settingsPanel.rectangle.Width * 6 / 16, settingsPanel.rectangle.Height * 9 / 16),
                                     settingsPanel,
                                     new Rectangle(0, 0, 50, 50),
                                     content.Load<Texture2D>("Sprites/Part 3 A"), new Rectangle(0, 0, 68, 36),
                                     content.Load<SpriteFont>("Fonts/UIFontBig"), "downSound", "-");

            upSound = new Button(new Point(settingsPanel.rectangle.Width * 9 / 16, settingsPanel.rectangle.Height * 9 / 16),
                         settingsPanel,
                         new Rectangle(0, 0, 50, 50),
                         content.Load<Texture2D>("Sprites/Part 3 A"), new Rectangle(0, 0, 68, 36),
                         content.Load<SpriteFont>("Fonts/UIFontBig"), "upSound", "+");


            downMusic = new Button(new Point(settingsPanel.rectangle.Width * 6 / 16, settingsPanel.rectangle.Height * 4 / 16),
                         settingsPanel,
                         new Rectangle(0, 0, 50, 50),
                         content.Load<Texture2D>("Sprites/Part 3 A"), new Rectangle(0, 0, 68, 36),
                         content.Load<SpriteFont>("Fonts/UIFontBig"), "downMusic", "-");

            upMusic = new Button(new Point(settingsPanel.rectangle.Width * 9 / 16, settingsPanel.rectangle.Height * 4 / 16),
                        settingsPanel,
                        new Rectangle(0, 0, 50, 50),
                        content.Load<Texture2D>("Sprites/Part 3 A"), new Rectangle(0, 0, 68, 36),
                        content.Load<SpriteFont>("Fonts/UIFontBig"), "upMusic", "+");

            backSettingsButton = new Button(new Point(settingsPanel.rectangle.Width / 4, (int)Math.Round(settingsPanel.rectangle.Height * 5f / 7)),
                                      settingsPanel,
                                      new Rectangle(0, 0, settingsPanel.rectangle.Width / 2, settingsPanel.rectangle.Height / 6),
                                      content.Load<Texture2D>("Sprites/Part 3 A"), new Rectangle(0, 0, 68, 36),
                                      content.Load<SpriteFont>("Fonts/UIFontBig"), "back", "Back");

            backSettingsButton.Clicked += (s, e) =>
            {

                try
                {
                    if (File.Exists(FileLoader.RootFolder + "\\Preferences\\PlayerPreferences.json"))
                    {
                        FileLoader.DeleteFile(FileLoader.RootFolder + "\\Preferences\\PlayerPreferences.json");
                    }

                    FileLoader.SaveToJson<Preferences>(Game1.currentPreferences, FileLoader.RootFolder + "\\Preferences\\PlayerPreferences.json");
                }
                catch
                {
                }
                CurrentPanel = mainMenuPanel;
                continueButton.IsActive = IsSaveGameExist();

            };

            downMusic.Clicked += (s, e) => { Game1.currentPreferences.MusicVolume -= 0.1f; };
            upMusic.Clicked += (s, e) => { Game1.currentPreferences.MusicVolume += 0.1f; };
            downSound.Clicked += (s, e) => { Game1.currentPreferences.SoundVolume -= 0.1f; };
            upSound.Clicked += (s, e) => { Game1.currentPreferences.SoundVolume += 0.1f; };
            settingsPanel.IsVisible = false;

            #endregion

            #region records

            scorePanel = new UIPanel(
            new Point(UIProcessor.ScreenBounds.X * 1 / 4, UIProcessor.ScreenBounds.Y * 1 / 7),
            new Rectangle(0, 0, UIProcessor.ScreenBounds.X / 2, UIProcessor.ScreenBounds.Y * 5 / 7),
            UIProcessor.UICanvas,
            content.Load<Texture2D>("Sprites/RectangleBox"));
            ScoreArrange();
            backRecordsButton = new Button(new Point(scorePanel.rectangle.Width / 4, (int)Math.Round(scorePanel.rectangle.Height * 5f / 7)),
                                      scorePanel,
                                      new Rectangle(0, 0, scorePanel.rectangle.Width / 2, scorePanel.rectangle.Height / 6),
                                      content.Load<Texture2D>("Sprites/Part 3 A"), new Rectangle(0, 0, 68, 36),
                                      content.Load<SpriteFont>("Fonts/UIFontBig"), "back", "Back");

            backRecordsButton.Clicked += (s, e) =>
            {
                CurrentPanel = mainMenuPanel;
                continueButton.IsActive = IsSaveGameExist();
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

            BackCreatorsButton = new Button(new Point(creatorsPanel.rectangle.Width / 4, (int)Math.Round(creatorsPanel.rectangle.Height * 5f / 7)),
                                      creatorsPanel,
                                      new Rectangle(0, 0, creatorsPanel.rectangle.Width / 2, creatorsPanel.rectangle.Height / 6),
                                      content.Load<Texture2D>("Sprites/Part 3 A"), new Rectangle(0, 0, 68, 36),
                                      content.Load<SpriteFont>("Fonts/UIFontBig"), "back", "Back");

            BackCreatorsButton.Clicked += (s, e) =>
            {
                CurrentPanel = mainMenuPanel;
                continueButton.IsActive = IsSaveGameExist();
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


            backInstructionButton = new Button(new Point(instructionPanel.rectangle.Width / 4, (int)Math.Round(instructionPanel.rectangle.Height * 5f / 7)),
                                      instructionPanel,
                                      new Rectangle(0, 0, instructionPanel.rectangle.Width / 2, instructionPanel.rectangle.Height / 6),
                                      content.Load<Texture2D>("Sprites/Part 3 A"), new Rectangle(0, 0, 68, 36),
                                      content.Load<SpriteFont>("Fonts/UIFontBig"), "back", "Back");

            backInstructionButton.Clicked += (s, e) =>
            {
                CurrentPanel = mainMenuPanel;
                continueButton.IsActive = IsSaveGameExist();
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
            pauseBackToGameButton = new Button(new Point(pausePanel.rectangle.Width / 8, (int)Math.Round(pausePanel.rectangle.Height * 3f / 7)),
                                      pausePanel,
                                      new Rectangle(0, 0, pausePanel.rectangle.Width * 3 / 8, pausePanel.rectangle.Height / 6),
                                      content.Load<Texture2D>("Sprites/Part 3 A"), new Rectangle(0, 0, 68, 36),
                                      content.Load<SpriteFont>("Fonts/UIFontBig"), "backToGame", "Resume");
            pauseBackToMainMenuButton = new Button(new Point(pausePanel.rectangle.Width * 4 / 8, (int)Math.Round(pausePanel.rectangle.Height * 3f / 7)),
                                      pausePanel,
                                      new Rectangle(0, 0, pausePanel.rectangle.Width * 3 / 8, pausePanel.rectangle.Height / 6),
                                      content.Load<Texture2D>("Sprites/Part 3 A"), new Rectangle(0, 0, 68, 36),
                                      content.Load<SpriteFont>("Fonts/UIFontBig"), "backToMainMenu", "Exit");

            pausePanel.IsVisible = false;

            pauseBackToMainMenuButton.Clicked += (s, e) =>
            {
                CurrentPanel = mainMenuPanel;
                Game1.game1.GameSave();
                Game1.game1.GameReload();
                continueButton.IsActive = IsSaveGameExist();
            };
            pauseBackToGameButton.Clicked += (s, e) => { CurrentPanel = gameplayLayoutPanel; };

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
            winBackToMainMenuButton = new Button(new Point(winPanel.rectangle.Width * 3 / 8, (int)Math.Round(winPanel.rectangle.Height * 5f / 7)),
                                          winPanel,
                                           new Rectangle(0, 0, winPanel.rectangle.Width * 3 / 8, winPanel.rectangle.Height / 6),
                                          content.Load<Texture2D>("Sprites/Part 3 A"), new Rectangle(0, 0, 68, 36),
                                          content.Load<SpriteFont>("Fonts/UIFontBig"), "backToMainMenu", "Exit");
            winBackToMainMenuButton.Clicked += (s, e) =>
            {
                CurrentPanel = mainMenuPanel;
                recordsButton.IsActive = false;
                settingsButton.IsActive = false;
                continueButton.IsActive = IsSaveGameExist();
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
            loseBackToMainMenuButton = new Button(new Point(losePanel.rectangle.Width * 3 / 8, (int)Math.Round(losePanel.rectangle.Height * 5f / 7)),
                                          losePanel,
                                          new Rectangle(0, 0, losePanel.rectangle.Width * 3 / 8, losePanel.rectangle.Height / 6),
                                          content.Load<Texture2D>("Sprites/Part 3 A"), new Rectangle(0, 0, 68, 36),
                                          content.Load<SpriteFont>("Fonts/UIFontBig"), "backToMainMenu", "Exit");

            loseBackToMainMenuButton.Clicked += (s, e) =>
            {
                Game1.game1.SaveRecords();
                Game1.game1.GameReload();
                CurrentPanel = mainMenuPanel;
                try
                {
                    FileLoader.DeleteFile(FileLoader.RootFolder + "\\SaveFiles\\SaveFile.json");
                }
                catch
                {

                }

                continueButton.IsActive = IsSaveGameExist();


            };
            losePanel.IsVisible = false;

            #endregion


            Game1.pauseForUI += (s, e) => { CurrentPanel = pausePanel; };

        }


        //Chack if there is a saved game
        public bool IsSaveGameExist()
        {
            try
            {
                //FileLoader.LoadFromJson<SaveFile>(FileLoader.RootFolder + "\\SaveFiles\\SaveFile.json");
                return File.Exists(FileLoader.RootFolder + "\\SaveFiles\\SaveFile.json");
            }
            catch
            {
                return false;
            }

        }

        /// <summary>
        /// Totally a work around thing. For dynamically changing elements
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void DrawMe(SpriteBatch spriteBatch)
        {
            if (currentPanel == settingsPanel)
            {
                //volume of sounds
                spriteBatch.DrawString(bigFont, Math.Round(Game1.currentPreferences.SoundVolume * 10).ToString(),
                    new Vector2(settingsPanel.Position.X + settingsPanel.rectangle.Width / 2 - 5,
                    settingsPanel.rectangle.Y + settingsPanel.rectangle.Height * 9 / 16 + 5),
                    Color.Gold);


                //volume of music
                spriteBatch.DrawString(bigFont, Math.Round(Game1.currentPreferences.MusicVolume * 10).ToString(),
                    new Vector2(settingsPanel.Position.X + settingsPanel.rectangle.Width / 2 - 5,
                    settingsPanel.rectangle.Y + (settingsPanel.rectangle.Height * 4 / 16) + 5),
                    Color.Gold);

            }

            if (currentPanel == losePanel)
            {
                spriteBatch.DrawString(bigFont, "Oh no, you lose!",
                    new Vector2(losePanel.Position.X + losePanel.rectangle.Width / 2 - 100,
                    losePanel.rectangle.Y + losePanel.rectangle.Height * 4 / 16 + 5),
                    Color.White);



                //volume of music
                spriteBatch.DrawString(bigFont, Game1.CurrentScore.ToString(),
                    new Vector2(losePanel.Position.X + losePanel.rectangle.Width / 2 - 5,
                    losePanel.rectangle.Y + (losePanel.rectangle.Height * 5 / 16) + 5),
                    Color.White);
            }

            if (currentPanel == gameplayLayoutPanel)
            {
                spriteBatch.DrawString(bigFont, "Score: " + Game1.CurrentScore,
                   new Vector2(Game1.screenBounds.X - bigFont.MeasureString("Score: " + Game1.CurrentScore).X, 0),
                   Color.White);

                for (int i = 0; i < Game1.mainCharacter.Health; i++)
                {
                    spriteBatch.Draw(heartTexture,
                        new Rectangle(Game1.screenBounds.X - heartTexture.Width * 6,
                        heartTexture.Height * 3 + (i * heartTexture.Height * 3),
                        heartTexture.Width * 3, heartTexture.Height * 3),
                        Color.White);
                }

            }

            //absolutely terrible wqay to provide text =(. Didn't have enough time at all!
            if (currentPanel == creatorsPanel)
            {
                spriteBatch.DrawString(smallFont, "Game assets were made by next venerable artists:",
                    new Vector2(creatorsPanel.rectangle.X + creatorsPanel.rectangle.Width / 2 - smallFont.MeasureString("Game assets were made by next venerable artists:").X / 2, creatorsPanel.rectangle.Y + creatorsPanel.rectangle.Height * 5 / 20),
                    Color.White);

                spriteBatch.DrawString(smallFont, "anokolisa.itch.io",
                    new Vector2(creatorsPanel.rectangle.X + creatorsPanel.rectangle.Width / 2 - smallFont.MeasureString("anokolisa.itch.io").X / 2, creatorsPanel.rectangle.Y + creatorsPanel.rectangle.Height * 6 / 20),
                    Color.White);

                spriteBatch.DrawString(smallFont, "bdragon1727.itch.io",
                    new Vector2(creatorsPanel.rectangle.X + creatorsPanel.rectangle.Width / 2 - smallFont.MeasureString("bdragon1727.itch.io").X / 2, creatorsPanel.rectangle.Y + creatorsPanel.rectangle.Height * 7 / 20),
                    Color.White);

                spriteBatch.DrawString(smallFont, "wenrexa.itch.io",
                    new Vector2(creatorsPanel.rectangle.X + creatorsPanel.rectangle.Width / 2 - smallFont.MeasureString("wenrexa.itch.io").X / 2, creatorsPanel.rectangle.Y + creatorsPanel.rectangle.Height * 8 / 20),
                    Color.White);

                spriteBatch.DrawString(smallFont, "etahoshi.itch.io",
                    new Vector2(creatorsPanel.rectangle.X + creatorsPanel.rectangle.Width / 2 - smallFont.MeasureString("etahoshi.itch.io").X / 2, creatorsPanel.rectangle.Y + creatorsPanel.rectangle.Height * 9 / 20),
                    Color.White);

                spriteBatch.DrawString(smallFont, "jdwasabi.itch.io",
                    new Vector2(creatorsPanel.rectangle.X + creatorsPanel.rectangle.Width / 2 - smallFont.MeasureString("jdwasabi.itch.io").X / 2, creatorsPanel.rectangle.Y + creatorsPanel.rectangle.Height * 10 / 20),
                    Color.White);

                spriteBatch.DrawString(smallFont, "leohpaz.itch.io",
                    new Vector2(creatorsPanel.rectangle.X + creatorsPanel.rectangle.Width / 2 - smallFont.MeasureString("leohpaz.itch.io").X / 2, creatorsPanel.rectangle.Y + creatorsPanel.rectangle.Height * 11 / 20),
                    Color.White);

                spriteBatch.DrawString(smallFont, "And programming - Iurii Kupreev",
                    new Vector2(creatorsPanel.rectangle.X + creatorsPanel.rectangle.Width / 2 - smallFont.MeasureString("And programming - Iurii Kupreev").X / 2, creatorsPanel.rectangle.Y + creatorsPanel.rectangle.Height * 12 / 20),
                    Color.White);

            }

            if (currentPanel == instructionPanel)
            {
                spriteBatch.DrawString(smallFont, "Description",
    new Vector2(creatorsPanel.rectangle.X + creatorsPanel.rectangle.Width / 2 - smallFont.MeasureString("Description").X / 2, creatorsPanel.rectangle.Y + creatorsPanel.rectangle.Height * 2 / 20),
    Color.White);

                spriteBatch.DrawString(smallFont, "     In this game you kill enemies which appear ",
                    new Vector2(creatorsPanel.rectangle.X + creatorsPanel.rectangle.Width / 11, creatorsPanel.rectangle.Y + creatorsPanel.rectangle.Height * 3 / 20),
                    Color.White);
                spriteBatch.DrawString(smallFont, "appear in a random plase at arena.",
                    new Vector2(creatorsPanel.rectangle.X + creatorsPanel.rectangle.Width / 11, creatorsPanel.rectangle.Y + creatorsPanel.rectangle.Height * 4 / 20),
                    Color.White);

                spriteBatch.DrawString(smallFont, "Enemies are becoming slightly harder to kill, ",
                    new Vector2(creatorsPanel.rectangle.X + creatorsPanel.rectangle.Width / 11, creatorsPanel.rectangle.Y + creatorsPanel.rectangle.Height * 5 / 20),
                    Color.White);

                spriteBatch.DrawString(smallFont, "bigger and faster with every dead foe.",
                    new Vector2(creatorsPanel.rectangle.X + creatorsPanel.rectangle.Width / 11, creatorsPanel.rectangle.Y + creatorsPanel.rectangle.Height * 6 / 20),
                    Color.White);

                spriteBatch.DrawString(smallFont, "Control",
                    new Vector2(creatorsPanel.rectangle.X + creatorsPanel.rectangle.Width / 2 - smallFont.MeasureString("Control").X / 2, creatorsPanel.rectangle.Y + creatorsPanel.rectangle.Height * 7 / 20),
                    Color.White);

                spriteBatch.DrawString(smallFont, "WASD - moving Up, Down, Left, Right",
                    new Vector2(creatorsPanel.rectangle.X + creatorsPanel.rectangle.Width / 11, creatorsPanel.rectangle.Y + creatorsPanel.rectangle.Height * 8 / 20),
                    Color.White);

                spriteBatch.DrawString(smallFont, "Left mouse click - attack",
                    new Vector2(creatorsPanel.rectangle.X + creatorsPanel.rectangle.Width / 11, creatorsPanel.rectangle.Y + creatorsPanel.rectangle.Height * 9 / 20),
                    Color.White);

                spriteBatch.DrawString(smallFont, "Score counting",
                    new Vector2(creatorsPanel.rectangle.X + creatorsPanel.rectangle.Width / 2 - smallFont.MeasureString("Score counting").X / 2, creatorsPanel.rectangle.Y + creatorsPanel.rectangle.Height * 10 / 20),
                    Color.White);

                spriteBatch.DrawString(smallFont, "     Score records after player's character die.",
                    new Vector2(creatorsPanel.rectangle.X + creatorsPanel.rectangle.Width / 11, creatorsPanel.rectangle.Y + creatorsPanel.rectangle.Height * 11 / 20),
                    Color.White);

                spriteBatch.DrawString(smallFont, "There is only one save, New Game rewrite it!",
                    new Vector2(creatorsPanel.rectangle.X + creatorsPanel.rectangle.Width / 11, creatorsPanel.rectangle.Y + creatorsPanel.rectangle.Height * 12 / 20),
                    Color.White);

                spriteBatch.DrawString(smallFont, "Hope you will have a good time with that!",
                    new Vector2(creatorsPanel.rectangle.X + creatorsPanel.rectangle.Width / 11, creatorsPanel.rectangle.Y + creatorsPanel.rectangle.Height * 13 / 20),
                    Color.White);

            }


        }

        public void ScoreArrange()
        {

            if (records == null)
            {

                records = new List<UIText>();
            }

            for (int i = 0; i < records.Count; i++)
            {
                UIProcessor.RemoveElement(records[i]);
            }

            records.Clear();


            for (int i = 0; i < 5; i++)
            {
                int j = i + 1;
                records.Add(new UIText(
            new Point(scorePanel.rectangle.Width / 3, scorePanel.rectangle.Height * (3 + j) / 16),
            new Rectangle(Point.Zero,
            bigFont.MeasureString(j + ". Score: " + Game1.currentScoreRecords.ScoreRecords[i].Score).ToPoint()),
            scorePanel,
            bigFont,
            j + ". Score: " + Game1.currentScoreRecords.ScoreRecords[i].Score));
                records[i].IsVisible = false;
                records[i].IsActive = false;
            }
        }



    }

}
