using DungeonCrawler;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography.Xml;

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

        #region UI

        UIManager uiManager;
        public static event EventHandler pauseForUI;

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
            _graphics.PreferredBackBufferHeight = 1280;



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
            UIProcessor.MouseCoursorTexture = Content.Load<Texture2D>("Sprites/Cursor Default");

            //camera
            _camera = new Camera(Vector2.Zero, new Vector2(-10000, -10000), new Vector2(10000, 10000), new Vector2(720, 1280));
            //_camera.SetCameraTarget();

            //File loader
            FileLoader.RootFolder = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Content"));

            //music 

            Dictionary<string, SoundEffect> songs = new Dictionary<string, SoundEffect>();
            songs.Add("mainMenu", Content.Load<SoundEffect>("Sounds/MainMenuMusic"));
            songs.Add("game", Content.Load<SoundEffect>("Sounds/GameMusic"));

            musicManager = new MusicManager(songs);
            musicManager.Play("mainMenu");

            //try
            //{
            //    if (File.Exists(FileLoader.RootFolder + "\\Preferences\\PlayerPreferences.json"))
            //        currentPreferences = FileLoader.LoadFromJson<Preferences>(FileLoader.RootFolder + "\\Preferences\\PlayerPreferences.json");
            //    else
            //        Debug.WriteLine("First load. File PlayerPreferences hasn't been found.");
            //}
            //catch
            //{
            //    Debug.WriteLine("First load. File PlayerPreferences hasn't been found.");
            //}


            //musicManager.SetMusicVolume(currentPreferences.MusicVolume);
            //SoundManager.SetMasterVolume(currentPreferences.SoundVolume);

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
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();

            CollisionProcessor.UpdateMe();
            UIProcessor.UpdateMe(Mouse.GetState(), Keyboard.GetState(), GamePad.GetState(PlayerIndex.One));
           
            //Update elapsed second time for the whole game
            Extentions.TotalSeconds = gameTime.ElapsedGameTime.TotalSeconds;

            //music volume setting
            //musicManager.SetMusicVolume(currentPreferences.MusicVolume);

            switch (StateOfGame)
            {
                case StateGame.MainMenu:
                    break;

                case StateGame.Game:
                    //game update
                   
                    //pause state transition
                    if (UIProcessor.Back)
                    {
                        //StateOfGame = StateGame.Pause;
                        //musicManager.Pause();
                        //musicManager.Play("mainMenu");
                        //SoundManager.SetMasterVolume(0f);
                        //pauseForUI?.Invoke(this, EventArgs.Empty);
                    }

                    break;
                case StateGame.Pause:
                    break;

                case StateGame.Win:
                    break;

                case StateGame.GameOver:
                    break;
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp, sortMode: SpriteSortMode.FrontToBack);

            switch (StateOfGame)
            {
                case StateGame.MainMenu:
                    break;
                case StateGame.Game:
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
            UIProcessor.DrawMe(_spriteBatch);
            uiManager.DrawMe(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
