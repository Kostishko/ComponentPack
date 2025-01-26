using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using DungeonCrawler;



namespace ComponentPack
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;


        #region Load data
        //current loaded files

        internal static PlayerPreferences currentPreferences;
        internal static RecordsData currentScoreRecords;

        #endregion

        #region metadata
        public enum StateGame
        {
            MainMenu,
            Game,
            Pause,
            Win,
            GameOver
        }

        public static StateGame StateOfGame;

        public static Point screenBounds;

        //at som emoment I've decided to make a common link for ContentManager
        public static ContentManager CommonContent;
        public static Game1 game1;

        private MusicManager musicManager;

        //Camers
        private Camera _camera;

        #endregion

        #region Gameplay variables

        public int CurrentScore;
        public MainCharacter MainChar;
        public BubbleManager bubbleManager;
        public Background background;


        //controll
        KeyboardState prevKeyboardState;
        KeyboardState currKeyboardState;

        #endregion

        #region UI

        //UIManager uiManager;
        public static event EventHandler pauseForUI;
        public SpriteFont UIFont;
        public SpriteFont UIFontSmall;

        #endregion

        public Game1()
        {


            _graphics = new GraphicsDeviceManager(this);
            _graphics.SynchronizeWithVerticalRetrace = true;
            this.IsFixedTimeStep = true;
            Content.RootDirectory = "Content";
            CommonContent = Content;
            IsMouseVisible = true;
            game1 = this;

            //Scren bounds
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = 720;
            _graphics.PreferredBackBufferHeight = 960;



        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);


            #region load data

            //UI processor
            UIProcessor.ScreenBounds = new Point(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            UIProcessor.BigUIFont = Content.Load<SpriteFont>("Fonts/UIFontBig");
            UIProcessor.MouseCoursorTexture = Content.Load<Texture2D>("Sprites/Cursor");

            //camera
            _camera = new Camera(Vector2.Zero, new Vector2(-10000, -10000), new Vector2(10000, 10000), new Vector2(720, 960));
            //_camera.SetCameraTarget();

            //File loader
            FileLoader.RootFolder = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Content"));

            //music 
            //music 

            Dictionary<string, SoundEffect> songs = new Dictionary<string, SoundEffect>();
            songs.Add("mainMenu", Content.Load<SoundEffect>("Sounds/FunkedUp"));
            

            musicManager = new MusicManager(songs);
            musicManager.Play("mainMenu");

            try
            {
                if (File.Exists(FileLoader.RootFolder + "\\SavedData\\PlayerPreferences.json"))
                    currentPreferences = FileLoader.LoadFromJson<PlayerPreferences>(FileLoader.RootFolder + "\\SavedData\\PlayerPreferences.json");
                else
                    Debug.WriteLine("First load. File PlayerPreferences hasn't been found.");
            }
            catch
            {
                Debug.WriteLine("First load. File PlayerPreferences hasn't been found.");
            }

            //try
            //{
            //    if (File.Exists(FileLoader.RootFolder + "\\SavedData\\RecordsData.json"))
            //    {
            //        currentScoreRecords = FileLoader.LoadFromJson<RecordsData>(FileLoader.RootFolder + "\\SavedData\\RecordsData.json");
            //        currentScoreRecords.ScoreRecords.Sort((x, y) => y.Score.CompareTo(x.Score)); // do the sorting
            //    }
            //}
            //catch
            //{
            //    Debug.WriteLine("Something wrong with the Scores file loading!");
            //}



            #endregion

            #region Gameplay objects loading
            CurrentScore = 0;
            MainChar = new MainCharacter(new Vector2 (720/2,960/2), 0f, CommonContent);
            _camera.SetCameraTarget(MainChar);
            bubbleManager = new BubbleManager(MainChar);
            StateOfGame = StateGame.MainMenu;
            background = new Background(new Vector2(1000,500), 0f, CommonContent);

            UIFont = CommonContent.Load<SpriteFont>("Fonts/UIFontBig");
            UIFontSmall = CommonContent.Load<SpriteFont>("Fonts/UIFontSmall");

            musicManager.SetMusicVolume(currentPreferences.MusicVolume);
            SoundManager.SetMasterVolume(currentPreferences.SoundVolume);
            #endregion

            #region Debug
            DebugManager.DebugTexture = Content.Load<Texture2D>("Debug/DebugBounds");
            DebugManager.isWorking = true;
            DebugManager.SpriteBatch = _spriteBatch;
            DebugManager.DebugFont = Content.Load<SpriteFont>("Debug/DebugFont");
            #endregion


        }

        protected override void Update(GameTime gameTime)
        {
            background.UpdateMe();
            _camera.UpdateMe();
            currKeyboardState = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            CollisionProcessor.UpdateMe();
            //UIProcessor.UpdateMe(Mouse.GetState(), Keyboard.GetState(), GamePad.GetState(PlayerIndex.One));
           
            //Update elapsed second time for the whole game
            Extentions.TotalSeconds = gameTime.ElapsedGameTime.TotalSeconds;

            //music volume setting
            //musicManager.SetMusicVolume(currentPreferences.MusicVolume);

            switch (StateOfGame)
            {
                case StateGame.MainMenu:
                    if(Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {
                        StateOfGame = StateGame.Game;
                    }
                    break;

                case StateGame.Game:
                    //game update
                   MainChar.UpdateMe(prevKeyboardState,currKeyboardState);
                    bubbleManager.Update();

                    //pause state transition
                    //if (UIProcessor.Back)
                    //{
                    //    //StateOfGame = StateGame.Pause;
                    //    //musicManager.Pause();
                    //    //musicManager.Play("mainMenu");
                    //    //SoundManager.SetMasterVolume(0f);
                    //    //pauseForUI?.Invoke(this, EventArgs.Empty);
                    //}

                    break;
                case StateGame.Pause:
                    break;

                case StateGame.Win:
                    break;

                case StateGame.GameOver:
                    break;
            }

            prevKeyboardState = currKeyboardState;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            background.DrawMe(_spriteBatch);
            _spriteBatch.End();


            _spriteBatch.Begin(samplerState: SamplerState.PointClamp, sortMode: SpriteSortMode.FrontToBack,  transformMatrix: _camera.GetCam());

            switch (StateOfGame)
            {
                case StateGame.MainMenu:
                    break;
                case StateGame.Game:
                    MainChar.DrawMe(_spriteBatch);
                    bubbleManager.DrawMe(_spriteBatch);

                    break;
                case StateGame.Pause:
                    break;

                case StateGame.Win:
                    break;

                case StateGame.GameOver:
                   

                    break;
            }

        
            _spriteBatch.End();

            //UI draw
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            //UIProcessor.DrawMe(_spriteBatch);
            //DebugManager.DebugLine("Camera position: " + _camera.position, Vector2.Zero);
            //DebugManager.DebugLine("Character position: " + MainChar.Transform.Position, new Vector2 (0,20));
            //uiManager.DrawMe(_spriteBatch);
            if(StateOfGame == StateGame.GameOver)
            {
                _spriteBatch.DrawString(UIFont, "Thanks for playing", new Vector2(200, 300), Color.Yellow);
                _spriteBatch.DrawString(UIFont, "Team :", new Vector2(300, 350), Color.Yellow);
                _spriteBatch.DrawString(UIFontSmall, "Iurii (Ludenus), Jakub (Xilled)", new Vector2(200, 400), Color.Yellow);
                _spriteBatch.DrawString(UIFontSmall, "Zampfies 'Rain'", new Vector2(250, 420), Color.Yellow);
                _spriteBatch.DrawString(UIFont, "Used free assets :", new Vector2(200, 500), Color.Yellow);
                _spriteBatch.DrawString(UIFontSmall, "https://opengameart.org/users/joth", new Vector2(200, 550), Color.Yellow);
                _spriteBatch.DrawString(UIFontSmall, "https://opengameart.org/users/jalastram", new Vector2(200, 570), Color.Yellow);
                _spriteBatch.DrawString(UIFontSmall, "https://opengameart.org/users/kobatogames", new Vector2(200, 590), Color.Yellow);
                _spriteBatch.DrawString(UIFontSmall, "https://craftpix.net/", new Vector2(250, 610), Color.Yellow);
                _spriteBatch.DrawString(UIFont, "Pres ESC to Exit", new Vector2(200, 700), Color.Yellow);
            }

            if(StateOfGame == StateGame.MainMenu)
            {
                _spriteBatch.DrawString(UIFont, "Press ENTER to Play!", new Vector2(200, 300), Color.Yellow);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        //Loading for new game methods
        public void NewGameLoad()
        {
            //player data loading
            CurrentScore = 0;            
            StateOfGame = StateGame.Game;

        }


        /// <summary>
        /// Save new record if this is a record
        /// </summary>
        public void SaveRecords()
        {
            ScoreRecord newRecord = new ScoreRecord();
            newRecord.Score = CurrentScore;
            currentScoreRecords.ScoreRecords.Add(newRecord);
            currentScoreRecords.ScoreRecords.Sort((x, y) => y.Score.CompareTo(x.Score)); // sort records
            currentScoreRecords.ScoreRecords.RemoveAt(5);
            //uiManager.ScoreArrange();

            //try
            //{
            //    if (File.Exists(FileLoader.RootFolder + "\\SavedData\\RecordsData.json"))
            //    {
            //        FileLoader.DeleteFile(FileLoader.RootFolder + "\\SavedData\\RecordsData.json");
            //        FileLoader.SaveToJson<RecordsData>(currentScoreRecords, FileLoader.RootFolder + "\\SavedData\\RecordsData.json");
            //    }
            //}
            //catch
            //{
            //    Debug.WriteLine("Something wrong with ScoreRecord file saving!");
            //}
        }

    }


}
