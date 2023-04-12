//-----------------------------------------------------------------------------------------------------------------Neccessary Libraries
//-------------------------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace STICK_HERO
{
   
    //------------------------------------------------------------------------------------------------------------------------------------------------
    //-----------------------------------------------------------------------------------------------------------------Declaring Classes and Functions
    //------------------------------------------------------------------------------------------------------------------------------------------------

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public MouseState GameBridgeMouse;
        public MouseState GameMelonMouse;
        public MouseState MenuMouse;
        public KeyboardState ShortcutKeys;

        //-------------------------------------------------Collision
        public bool IfCollision(int XofDest, int WidthOfDest, int XofStart, int HeightOfStart)
        {
            if (XofStart + HeightOfStart >= XofDest && XofStart + HeightOfStart <= XofDest + WidthOfDest)
                return true;
            else
                return false;
        }
    
        class Sprite : Microsoft.Xna.Framework.Game
        {
            Rectangle rectangle;
            Texture2D texture;


            public Sprite(Texture2D newTexture, Rectangle newRectangle)
            {
                texture = newTexture;
                rectangle = newRectangle;
            }

            public void Update()
            {
            }
            public void Draw(SpriteBatch spiriteBatch)
            {
                spiriteBatch.Draw(texture, rectangle, Color.White);
            }

        }



        //-------------------------------------------------
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //-------------------------------------------------Sprite ( Class )



        //-------------------------------------------------Rectangles
        //---------------Game Rects
        Rectangle PlayerRect;
        Rectangle BlackPlatformRect1;
        Rectangle RedPlatformRect1;
        Rectangle BlackPlatformRect2;
        Rectangle RedPlatformRect2;
        Rectangle InGameScoreRect;
        Rectangle PlayerMenuRect;
        Rectangle BlackPlatformMenuRect;
        Rectangle WallpaperRect1;
        Rectangle BridgeRect;
        //---------------Menu Rects
        Rectangle PlayButtonRect;
        Rectangle OptionsButtonRect;
        Rectangle CharacterSelectButtonRect;
        Rectangle MelonModeButtonRect;
        Rectangle HelpButtonRect;       
        Rectangle LogoRect;
        //---------------Character Select Menu Textures
        Rectangle CharacterSelectSheetRect;
        Rectangle SonicPlayerRect;
        Rectangle JafarPlayerRect;
        Rectangle MegaManPlayerRect;
        //------------------Options Rects
        Rectangle OptionsLogoRect;
        Rectangle MusicLogoRect;
        Rectangle SoundLogoRect;
        Rectangle ToggleFullscreenLogoRect;
        //-------for Music
        Rectangle OnGreenRectM;
        Rectangle OnGreyRectM;
        Rectangle OffGreenRectM;
        Rectangle OffGreyRectM;
        //-------for Sound
        Rectangle OnGreenRectS;
        Rectangle OnGreyRectS;
        Rectangle OffGreenRectS;
        Rectangle OffGreyRectS;
        //-------for Fullscreen
        Rectangle OnGreenRectT;
        Rectangle OnGreyRectT;
        Rectangle OffGreenRectT;
        Rectangle OffGreyRectT;
        Rectangle MenuButtonOPRect;
        //--------------- GameOverRects
        Rectangle MenuButtonGORect;
        Rectangle PlayAgainButtonRect;
        Rectangle LeaderboardsButtonRect;
        Rectangle ScoreBoardRect;
        //----------------HelpRects
        Rectangle MenuButtonHeRect;
        Rectangle Help11Rect;
        Rectangle Help12Rect;
        Rectangle Help21Rect;
        Rectangle Help22Rect;


        //-------------------------------------------------Textures
        //---------------Game Textures
        Texture2D Player;
        Texture2D BlackPlatform;
        Texture2D RedPlatform;
        Texture2D InGameScore;
        Texture2D Wallpaper1;
        //---------------Menu Textures
        Texture2D PlayButton;
        Texture2D OptionsButton;
        Texture2D CharacterSelectButton;
        Texture2D MelonModeButton;        
        Texture2D HelpButton;        
        Texture2D Logo;
        //---------------Character Select Menu Textures
        Texture2D CharacterSelectSheet;
        Texture2D SonicPlayer;
        Texture2D JafarPlayer;
        Texture2D MegaManPlayer;
        //---------------Options Textures
        Texture2D OptionsLogo;
        Texture2D MusicLogo;
        Texture2D SoundLogo;
        Texture2D ToggleFullscreenLogo;
        Texture2D OnGreen;
        Texture2D OnGrey;
        Texture2D OffGreen;
        Texture2D OffGrey;
        //---------------GameOver Textures
        Texture2D MenuButton;
        Texture2D PlayAgainButton;
        Texture2D LeaderboardsButton;
        Texture2D ScoreBoard;
        //---------------Help Textures
        Texture2D Help11;
        Texture2D Help12;
        Texture2D Help21;
        Texture2D Help22;

        //-------------------------------------------------Vectors

        Vector2 BridgeOrigin;

        //-------------------------------------------------SoundEffects & Song

        SoundEffect DeathFX;
        SoundEffect ScoreFX;
        SoundEffect BridgeFallingFX;
        Song WindSong;

        //-------------------------------------------------Other

        Random Rand = new Random();
        SpriteFont Font;
        SpriteFont FontScoreB;
        SpriteFont FontHighScoreB;

        //-------------------------------------------------Variables 

        int RandPlatformX;
        int RandPlatformWidth;
        int Score = 0;
        int ScoreController = 0;
        //-------Bridge
        int BridgeX = 95 ;
        int BridgeY = 600;
        int BridgeWidth = 5 ;
        int BridgeHeight = 0;
        int BridgeV = 10;
        //-------Player
        int PlayerX = 44;
        int PlayerY = 550;
        int PlayerXV = 5;
        int PlayerYV = 10;


        //-------------------------------------------------States and Booleans

        bool ShowingBridge = false;
        bool StateSound = true;
        bool StateScreen = false;
        bool ScoreStop = true;
        bool BridgeCreationController = true;
        bool GameOverState = false;

        int PlayerSpriteState = 0;

        int PlayFXrepetitive1 = 0;
        int PlayFXrepetitive2 = 0;
       

        //-------------------------------------------------Enums 
        enum GameBridge
        { Standing , BridgeCreation, CheckCollision, PushPlatforms }
        GameBridge GameState = GameBridge.Standing;

        enum Menu
        { MainMenu, GameBridge, GameMelon ,Options, Credits, Help, Leaderboards, CharacterSelect, GameOver, Exit }
        Menu MenuState = Menu.MainMenu;

        //-----------------------------------------------------------------------------------------------------------------Class for the Entire Game 
        //------------------------------------------------------------------------------------------------------------------------------------------

        public Game1()
        {
            //-------------------------------------------------Screen

            IsMouseVisible = true;
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 980;
            graphics.PreferredBackBufferWidth = 650;
            Content.RootDirectory = "Content";
        }

        //-----------------------------------------------------------------------------------------------------------------Initialize
        //---------------------------------------------------------------------------------------------------------------------------
        protected override void Initialize()
        {
            //-----------------------------------------Rectangle Initialization(Objects)
            if (PlayerSpriteState == 0)
                PlayerRect = new Rectangle(PlayerX, PlayerY, 50, 50);
            if (PlayerSpriteState == 1)
                MegaManPlayerRect = new Rectangle(PlayerX, PlayerY, 50, 50);
            if (PlayerSpriteState == 2)
                SonicPlayerRect = new Rectangle(PlayerX, PlayerY, 50, 50);
            BlackPlatformRect1 = new Rectangle(0, 600, 100, 400);
            RedPlatformRect1 = new Rectangle(45, 600, 10, 7);
            InGameScoreRect = new Rectangle(225, 75, 200, 75);
            //-----------------------------------------Rectangle Initialization(Wallpapers)
            WallpaperRect1 = new Rectangle(0, 0, 690, 1000);
            //-----------------------------------------Vector Initialization
            BridgeOrigin = new Vector2(0, 0);
            //-----------------------------------------Random Initilization
            RandPlatformX = Rand.Next(150, 500);
            RandPlatformWidth = Rand.Next(50, 150);


            base.Initialize();
        }

        //-----------------------------------------------------------------------------------------------------------------Load
        //---------------------------------------------------------------------------------------------------------------------
        protected override void LoadContent()
        {
          
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //-----------------------------------------Load Objects
            Player = Content.Load<Texture2D>("Player");
            BlackPlatform = Content.Load<Texture2D>("BlackBlock");
            RedPlatform = Content.Load<Texture2D>("RedBlock");
            InGameScore = Content.Load<Texture2D>("INGameScore");
            //-----------------------------------------Load Wallpapers
            Wallpaper1 = Content.Load<Texture2D>("Wallpaper1");
            //-----------------------------------------Load SoundEffects & Songs
            ScoreFX = Content.Load<SoundEffect>("Score");
            DeathFX = Content.Load<SoundEffect>("Death");
            BridgeFallingFX = Content.Load<SoundEffect>("BridgeFalling");
            WindSong = Content.Load<Song>("WIND");
            //-----------------------------------------Load Fonts
            Font = Content.Load<SpriteFont>("MyFont");
            FontScoreB = Content.Load<SpriteFont>("MyFont");
            FontHighScoreB = Content.Load<SpriteFont>("MyFont");
            //-----------------------------------------Load Menu Buttons
            //---------------Menu Buttons
            PlayButton = Content.Load<Texture2D>("Button Play");
            OptionsButton = Content.Load<Texture2D>("Options Button");
            CharacterSelectButton = Content.Load<Texture2D>("Character Select");
            MelonModeButton = Content.Load<Texture2D>("Melon Mode");
            HelpButton = Content.Load<Texture2D>("Help");
            Logo = Content.Load<Texture2D>("LOGO");
            //---------------Character Select Buttons
            CharacterSelectSheet = Content.Load<Texture2D>("CS");
            SonicPlayer = Content.Load<Texture2D>("Sonic");
            JafarPlayer = Content.Load<Texture2D>("Jafar");
            MegaManPlayer = Content.Load<Texture2D>("MegaMan");
            //---------------Options Buttons
            OptionsLogo = Content.Load<Texture2D>("Options");
            MusicLogo = Content.Load<Texture2D>("Music");
            SoundLogo = Content.Load<Texture2D>("Sound");
            ToggleFullscreenLogo = Content.Load<Texture2D>("ToggleFullscreen");
            OnGreen = Content.Load<Texture2D>("ONON");
            OnGrey = Content.Load<Texture2D>("ONOFF");
            OffGreen = Content.Load<Texture2D>("OFFON");
            OffGrey = Content.Load<Texture2D>("OFFOFF");    
            //---------------GameOver Buttons
            MenuButton = Content.Load<Texture2D>("Button Menu");
            PlayAgainButton = Content.Load<Texture2D>("Button PlayAgain");
            LeaderboardsButton = Content.Load<Texture2D>("Button Stats");
            ScoreBoard = Content.Load<Texture2D>("ScoreBoard -Gameover");
            //---------------Help Object
            Help11 = Content.Load<Texture2D>("11");
            Help12 = Content.Load<Texture2D>("12");
            Help21 = Content.Load<Texture2D>("21");
            Help22 = Content.Load<Texture2D>("22");


            MediaPlayer.Play(WindSong);

        }

        //-----------------------------------------------------------------------------------------------------------------Unload
        //-----------------------------------------------------------------------------------------------------------------------
        protected override void UnloadContent()
        {
   
        }

        //-----------------------------------------------------------------------------------------------------------------Update
        //-----------------------------------------------------------------------------------------------------------------------
        protected override void Update(GameTime gameTime)
        {


            GameBridgeMouse = Mouse.GetState();
            BlackPlatformRect1 = new Rectangle(0, 600, 100, 400);
            RedPlatformRect1 = new Rectangle(45, 600, 10, 7);
            BlackPlatformRect2 = new Rectangle(RandPlatformX, 600, RandPlatformWidth, 400);
            RedPlatformRect2 = new Rectangle(RandPlatformX + ((RandPlatformWidth / 2) - 5), 600, 10, 7);



            

            //----------------------------------------------------Main Menu
            //-------------------------------------------------------------

            if (MenuState == Menu.MainMenu)
            {
                MenuMouse = Mouse.GetState();
                //---------------Menu Objects
                PlayButtonRect = new Rectangle(200, 350, 250, 250);
                LogoRect = new Rectangle(100, 50, 460, 246);
                OptionsButtonRect = new Rectangle(50, 700, 75, 75);
                CharacterSelectButtonRect = new Rectangle(50, 800, 75, 75);
                MelonModeButtonRect = new Rectangle(525, 700, 75, 75);
                HelpButtonRect = new Rectangle(525, 800, 75, 75);
                PlayerMenuRect = new Rectangle(300, 700, 50, 50);
                BlackPlatformMenuRect = new Rectangle(280, 750, 100, 400);

                if (PlayerSpriteState == 0)
                    PlayerRect = new Rectangle(PlayerX, PlayerY, 50, 50);
                if (PlayerSpriteState == 1)
                    MegaManPlayerRect = new Rectangle(PlayerX, PlayerY, 50, 50);
                if (PlayerSpriteState == 2)
                    SonicPlayerRect = new Rectangle(PlayerX, PlayerY, 50, 50);




                if (MenuMouse.X >= PlayButtonRect.X && MenuMouse.X <= PlayButtonRect.X + PlayButtonRect.Width && MenuMouse.Y >= PlayButtonRect.Y && MenuMouse.Y <= PlayButtonRect.Y + PlayButtonRect.Height && MenuMouse.RightButton == ButtonState.Pressed)
                    MenuState = Menu.GameBridge;

                if (MenuMouse.X >= OptionsButtonRect.X && MenuMouse.X <= OptionsButtonRect.X + OptionsButtonRect.Width && MenuMouse.Y >= OptionsButtonRect.Y && MenuMouse.Y <= OptionsButtonRect.Y + OptionsButtonRect.Height && MenuMouse.RightButton == ButtonState.Pressed)
                    MenuState = Menu.Options;

                if (MenuMouse.X >= CharacterSelectButtonRect.X && MenuMouse.X <= CharacterSelectButtonRect.X + CharacterSelectButtonRect.Width && MenuMouse.Y >= CharacterSelectButtonRect.Y && MenuMouse.Y <= CharacterSelectButtonRect.Y + CharacterSelectButtonRect.Height && MenuMouse.RightButton == ButtonState.Pressed)
                    MenuState = Menu.CharacterSelect;

                if (MenuMouse.X >= MelonModeButtonRect.X && MenuMouse.X <= MelonModeButtonRect.X + MelonModeButtonRect.Width && MenuMouse.Y >= MelonModeButtonRect.Y && MenuMouse.Y <= MelonModeButtonRect.Y + MelonModeButtonRect.Height && MenuMouse.RightButton == ButtonState.Pressed)
                    MenuState = Menu.GameMelon;

                if (MenuMouse.X >= HelpButtonRect.X && MenuMouse.X <= HelpButtonRect.X + HelpButtonRect.Width && MenuMouse.Y >= HelpButtonRect.Y && MenuMouse.Y <= HelpButtonRect.Y + HelpButtonRect.Height && MenuMouse.RightButton == ButtonState.Pressed)
                    MenuState = Menu.Help;
            }

            //----------------------------------------------------Game Menu
            //-------------------------------------------------------------
            else if (MenuState == Menu.GameBridge)
            {



                if (GameBridgeMouse.LeftButton == ButtonState.Pressed)
                {
                    GameState = GameBridge.BridgeCreation;
                    ShowingBridge = true;
                }
                //--------------------------------------Bridge Creation Section
                if (GameState == GameBridge.BridgeCreation /*&& BridgeCreationController*/)
                {
                    BridgeRect = new Rectangle(BridgeX, BridgeY, BridgeWidth, BridgeHeight);
                    BridgeHeight += BridgeV;
                    BridgeY -= BridgeV;

                    if (BridgeY == 0)
                    {
                        BridgeV = 0;
                    }
                    if (GameBridgeMouse.LeftButton == ButtonState.Released)
                    {
                        ShowingBridge = false;
                        GameState = GameBridge.CheckCollision;
                    }
                }



                //--------------------------------------Check Collision Section
                if (GameState == GameBridge.CheckCollision)
                {
                   // BridgeCreationController = false;
                    if (IfCollision(BlackPlatformRect2.X, BlackPlatformRect2.Width, BridgeRect.X, BridgeRect.Height))
                    {

                        PlayFXrepetitive1++;
                        if (PlayFXrepetitive1 == 1 && ScoreStop == true)
                        {
                            if (StateSound == true)
                                BridgeFallingFX.Play();

                            ScoreStop = false;
                            Score++;
                        }
                        if (IfCollision(RedPlatformRect2.X, RedPlatformRect2.Width, BridgeRect.X, BridgeRect.Height) && ScoreStop == true)
                        {
                            ScoreStop = false;
                            Score += 2;//RED PLAT Score.
                        }
                        PlayerRect.X += PlayerXV;
                        if (PlayerRect.X >= BlackPlatformRect2.X + BlackPlatformRect2.Width - 55)
                        {
                            PlayerXV = 0;

                            PlayFXrepetitive2++;
                            if (PlayFXrepetitive2 == 1 && StateSound == true)
                                ScoreFX.Play();
                        }
                    }
                    else
                    {
                        PlayerRect.X += PlayerXV;
                       
                        if (PlayerRect.X >= BridgeRect.X + BridgeRect.Height)
                        {
                            PlayerXV = 0;
                            PlayerRect.Y += PlayerYV;

                            if (PlayerRect.Y == 980 && StateSound == true)
                            {
                                DeathFX.Play();

                                if (PlayerRect.Y >= 980)
                                   MenuState = Menu.GameOver;
                            }

                        }
                    }
                }
            
            //---------------------------------------Push Platforms Section
            if (GameState == GameBridge.PushPlatforms)
            {

            }
        }
            //----------------------------------------------------Options Menu
            //----------------------------------------------------------------
            if (MenuState == Menu.Options)
            {
                MenuMouse = Mouse.GetState();

                OptionsLogoRect = new Rectangle(125, 50, 400, 70);
                //---------- Music Objects
                MusicLogoRect = new Rectangle(55, 250, 150, 30);

                OnGreenRectM = new Rectangle(50, 300, 60, 30);
                if (MenuMouse.X >= OnGreenRectM.X && MenuMouse.X <= OnGreenRectM.X + OnGreenRectM.Width && MenuMouse.Y >= OnGreenRectM.Y && MenuMouse.Y <= OnGreenRectM.Y + OnGreenRectM.Height && MenuMouse.RightButton == ButtonState.Pressed)
                    MediaPlayer.Play(WindSong);

                OffGreyRectM = new Rectangle(130, 301, 60, 28);
                if (MenuMouse.X >= OffGreyRectM.X && MenuMouse.X <= OffGreyRectM.X + OffGreyRectM.Width && MenuMouse.Y >= OffGreyRectM.Y && MenuMouse.Y <= OffGreyRectM.Y + OffGreyRectM.Height && MenuMouse.RightButton == ButtonState.Pressed)
                    MediaPlayer.Stop();

                //---------- Sound Objects
                SoundLogoRect = new Rectangle(420, 250, 150, 30);

                OnGreenRectS = new Rectangle(415, 300, 60, 30);
                if (MenuMouse.X >= OnGreenRectS.X && MenuMouse.X <= OnGreenRectS.X + OnGreenRectS.Width && MenuMouse.Y >= OnGreenRectS.Y && MenuMouse.Y <= OnGreenRectS.Y + OnGreenRectS.Height && MenuMouse.RightButton == ButtonState.Pressed)
                    StateSound = true;

                OffGreyRectS = new Rectangle(495, 301, 60, 28);
                if (MenuMouse.X >= OffGreyRectS.X && MenuMouse.X <= OffGreyRectS.X + OffGreyRectS.Width && MenuMouse.Y >= OffGreyRectS.Y && MenuMouse.Y <= OffGreyRectS.Y + OffGreyRectS.Height && MenuMouse.RightButton == ButtonState.Pressed)
                    StateSound = false;

                //---------- FullScreen Objects Objects
                ToggleFullscreenLogoRect = new Rectangle(175, 500, 300, 80);

                OnGreenRectT = new Rectangle(250, 600, 60, 30);
                if (MenuMouse.X >= OnGreenRectT.X && MenuMouse.X <= OnGreenRectT.X + OnGreenRectT.Width && MenuMouse.Y >= OnGreenRectT.Y && MenuMouse.Y <= OnGreenRectT.Y + OnGreenRectT.Height && MenuMouse.RightButton == ButtonState.Pressed)

                    graphics.IsFullScreen = true;


                OffGreyRectT = new Rectangle(340, 601, 60, 28);
                if (MenuMouse.X >= OffGreyRectT.X && MenuMouse.X <= OffGreyRectT.X + OffGreyRectT.Width && MenuMouse.Y >= OffGreyRectT.Y && MenuMouse.Y <= OffGreyRectT.Y + OffGreyRectT.Height && MenuMouse.RightButton == ButtonState.Pressed)
                    graphics.IsFullScreen = false;

                MenuButtonOPRect = new Rectangle(255, 800, 100, 100);
                if (MenuMouse.X >= MenuButtonOPRect.X && MenuMouse.X <= MenuButtonOPRect.X + MenuButtonOPRect.Width && MenuMouse.Y >= MenuButtonOPRect.Y && MenuMouse.Y <= MenuButtonOPRect.Y + MenuButtonOPRect.Height && MenuMouse.RightButton == ButtonState.Pressed)
                    MenuState = Menu.MainMenu;
            }
            //----------------------------------------------------Character Select Menu
            //----------------------------------------------------------------
            if (MenuState == Menu.CharacterSelect)
            {

                MenuMouse = Mouse.GetState();
                MenuButtonHeRect = new Rectangle(280, 860, 100, 100);
                if (MenuMouse.X >= MenuButtonHeRect.X && MenuMouse.X <= MenuButtonHeRect.X + MenuButtonHeRect.Width && MenuMouse.Y >= MenuButtonHeRect.Y && MenuMouse.Y <= MenuButtonHeRect.Y + MenuButtonHeRect.Height && MenuMouse.RightButton == ButtonState.Pressed)
                    MenuState = Menu.MainMenu;

                CharacterSelectSheetRect = new Rectangle(50, 50, 552, 804);
                MegaManPlayerRect = new Rectangle(380, 180, 150, 200);
                if (MenuMouse.X >= MenuButtonHeRect.X && MenuMouse.X <= MenuButtonHeRect.X + MenuButtonHeRect.Width && MenuMouse.Y >= MenuButtonHeRect.Y && MenuMouse.Y <= MenuButtonHeRect.Y + MenuButtonHeRect.Height && MenuMouse.RightButton == ButtonState.Pressed)
                    PlayerSpriteState = 2;

                SonicPlayerRect = new Rectangle(130, 460, 160, 250);
                if (MenuMouse.X >= MenuButtonHeRect.X && MenuMouse.X <= MenuButtonHeRect.X + MenuButtonHeRect.Width && MenuMouse.Y >= MenuButtonHeRect.Y && MenuMouse.Y <= MenuButtonHeRect.Y + MenuButtonHeRect.Height && MenuMouse.RightButton == ButtonState.Pressed)
                    PlayerSpriteState = 3;


            }

                //----------------------------------------------------GameOver Menu
                //----------------------------------------------------------------

                if (MenuState == Menu.GameOver)
                {
                PlayerRect = new Rectangle(PlayerX, PlayerY, 50, 50);
                BridgeRect = new Rectangle(BridgeX, BridgeY, 5 ,0);
                
                PlayerXV = 5;
                
                MenuMouse = Mouse.GetState();
                MenuButtonGORect = new Rectangle(265, 800, 100, 100);
                if (MenuMouse.X >= MenuButtonGORect.X && MenuMouse.X <= MenuButtonGORect.X + MenuButtonGORect.Width && MenuMouse.Y >= MenuButtonGORect.Y && MenuMouse.Y <= MenuButtonGORect.Y + MenuButtonGORect.Height && MenuMouse.RightButton == ButtonState.Pressed)
                    MenuState = Menu.MainMenu;
                PlayAgainButtonRect = new Rectangle(160, 800, 100, 100);
                if (MenuMouse.X >= PlayAgainButtonRect.X && MenuMouse.X <= PlayAgainButtonRect.X + PlayAgainButtonRect.Width && MenuMouse.Y >= PlayAgainButtonRect.Y && MenuMouse.Y <= PlayAgainButtonRect.Y + PlayAgainButtonRect.Height && MenuMouse.RightButton == ButtonState.Pressed)
                    MenuState = Menu.GameBridge;
                LeaderboardsButtonRect = new Rectangle(370, 800, 100, 100);
                if (MenuMouse.X >= LeaderboardsButtonRect.X && MenuMouse.X <= LeaderboardsButtonRect.X + LeaderboardsButtonRect.Width && MenuMouse.Y >= LeaderboardsButtonRect.Y && MenuMouse.Y <= LeaderboardsButtonRect.Y + LeaderboardsButtonRect.Height && MenuMouse.RightButton == ButtonState.Pressed)
                    MenuState = Menu.Leaderboards;
                ScoreBoardRect = new Rectangle(110, 100, 450, 300);
               
            }
            //---------------------------------------------------Help Menu
            //----------------------------------------------------------------
            if (MenuState == Menu.Help)
            {
                MenuMouse = Mouse.GetState();
                MenuButtonHeRect = new Rectangle(280, 805, 100, 100);
                if (MenuMouse.X >= MenuButtonHeRect.X && MenuMouse.X <= MenuButtonHeRect.X + MenuButtonHeRect.Width && MenuMouse.Y >= MenuButtonHeRect.Y && MenuMouse.Y <= MenuButtonHeRect.Y + MenuButtonHeRect.Height && MenuMouse.RightButton == ButtonState.Pressed)
                    MenuState = Menu.MainMenu;

                Help11Rect = new Rectangle(50, 50, 250, 350);
                Help12Rect = new Rectangle(350, 50, 250, 350);
                Help21Rect = new Rectangle(50, 450, 250, 350);
                Help22Rect = new Rectangle(350, 450, 250, 350);

            }
                //----------------------------------------------------Character Select Menu
                //----------------------------------------------------------------


                base.Update(gameTime);
        }

        //-----------------------------------------------------------------------------------------------------------------Draw
        //---------------------------------------------------------------------------------------------------------------------
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();

            //----------------------------------------------------Main Menu
            //-------------------------------------------------------------

            if (MenuState == Menu.MainMenu)
            {
                spriteBatch.Draw(Wallpaper1, WallpaperRect1, Color.White);

                spriteBatch.Draw(Player, PlayerMenuRect, Color.White);

                spriteBatch.Draw(BlackPlatform, BlackPlatformMenuRect, Color.White);

                spriteBatch.Draw(PlayButton, PlayButtonRect, Color.White);

                spriteBatch.Draw(OptionsButton, OptionsButtonRect, Color.White);

                spriteBatch.Draw(CharacterSelectButton, CharacterSelectButtonRect, Color.White);

                spriteBatch.Draw(MelonModeButton, MelonModeButtonRect, Color.White);

                spriteBatch.Draw(HelpButton, HelpButtonRect, Color.White);

                spriteBatch.Draw(Logo, LogoRect, Color.White);
   

            }

            //----------------------------------------------------Options Menu
            //-------------------------------------------------------------

            if (MenuState == Menu.Options)
            {

                spriteBatch.Draw(Wallpaper1, WallpaperRect1, Color.White);

                spriteBatch.Draw(OptionsLogo, OptionsLogoRect, Color.White);

                spriteBatch.Draw(MusicLogo, MusicLogoRect, Color.White);

                spriteBatch.Draw(OnGreen, OnGreenRectM, Color.White);
                spriteBatch.Draw(OnGrey, OnGreyRectM, Color.White);
                spriteBatch.Draw(OffGreen, OffGreenRectM, Color.White);
                spriteBatch.Draw(OffGrey, OffGreyRectM, Color.White);

                spriteBatch.Draw(SoundLogo, SoundLogoRect, Color.White);

                spriteBatch.Draw(OnGreen, OnGreenRectS, Color.White);
                spriteBatch.Draw(OnGrey, OnGreyRectS, Color.White);
                spriteBatch.Draw(OffGreen, OffGreenRectS, Color.White);
                spriteBatch.Draw(OffGrey, OffGreyRectS, Color.White);

                spriteBatch.Draw(ToggleFullscreenLogo, ToggleFullscreenLogoRect, Color.White);

                spriteBatch.Draw(OnGreen, OnGreenRectT, Color.White);
                spriteBatch.Draw(OnGrey, OnGreyRectT, Color.White);
                spriteBatch.Draw(OffGreen, OffGreenRectT, Color.White);
                spriteBatch.Draw(OffGrey, OffGreyRectT, Color.White);

               spriteBatch.Draw(MenuButton, MenuButtonOPRect, Color.White);
            }

            //----------------------------------------------------Game Menu
            //-------------------------------------------------------------

            if (MenuState == Menu.GameBridge)
            {

                spriteBatch.Draw(Wallpaper1, WallpaperRect1, Color.White);

                if (PlayerSpriteState == 0)
                    spriteBatch.Draw(Player, PlayerRect, Color.White);
                if (PlayerSpriteState == 1)
                    spriteBatch.Draw(MegaManPlayer, MegaManPlayerRect, Color.White);
                if (PlayerSpriteState == 2)
                    spriteBatch.Draw(SonicPlayer, SonicPlayerRect, Color.White);

                spriteBatch.Draw(InGameScore, InGameScoreRect, Color.White);

                spriteBatch.Draw(BlackPlatform, BlackPlatformRect1, Color.White);

                spriteBatch.Draw(BlackPlatform, BlackPlatformRect2, Color.White);

                spriteBatch.Draw(RedPlatform, RedPlatformRect1, Color.White);

                spriteBatch.DrawString(Font, "" + Score, new Vector2(300, 75), Color.Black);

                spriteBatch.Draw(RedPlatform, RedPlatformRect2, Color.White);

                if (ShowingBridge == true)
                {
                    spriteBatch.Draw(BlackPlatform, BridgeRect, Color.White);
                }
                if (ShowingBridge == false)
                {
                    BridgeRect.Y = 600;
                    spriteBatch.Draw(BlackPlatform, BridgeRect, null, Color.White, 4.712389f, BridgeOrigin, SpriteEffects.None, 0);
                }
            }

            //----------------------------------------------------GameOver Menu
            //-----------------------------------------------------------------
            if (MenuState == Menu.GameOver)
            {
                
                spriteBatch.Draw(Wallpaper1, WallpaperRect1, Color.White);

                spriteBatch.Draw(MenuButton, MenuButtonGORect, Color.White);

                spriteBatch.Draw(PlayAgainButton, PlayAgainButtonRect, Color.White);

                spriteBatch.Draw(LeaderboardsButton, LeaderboardsButtonRect, Color.White);

                spriteBatch.Draw(ScoreBoard, ScoreBoardRect, Color.White);

                spriteBatch.DrawString(FontScoreB, "" + Score, new Vector2(320, 180), Color.Black);
                spriteBatch.DrawString(FontHighScoreB, "" + 1, new Vector2(320, 300), Color.Black);

            }

            //----------------------------------------------------GameOver Menu
            //-----------------------------------------------------------------

            if (MenuState == Menu.Help)
            {

                spriteBatch.Draw(Wallpaper1, WallpaperRect1, Color.White);

                spriteBatch.Draw(MenuButton, MenuButtonHeRect, Color.White);

                spriteBatch.Draw(Help11, Help11Rect, Color.White);

                spriteBatch.Draw(Help12, Help12Rect, Color.White);

                spriteBatch.Draw(Help21, Help21Rect, Color.White);

                spriteBatch.Draw(Help22, Help22Rect, Color.White);

            }

            //---------------------------------------------Character Select Menu
            //------------------------------------------------------------------
            if (MenuState == Menu.CharacterSelect)
            {

                spriteBatch.Draw(Wallpaper1, WallpaperRect1, Color.White);

                spriteBatch.Draw(MenuButton, MenuButtonHeRect, Color.White);

                spriteBatch.Draw(CharacterSelectSheet, CharacterSelectSheetRect, Color.White);

                spriteBatch.Draw(SonicPlayer, SonicPlayerRect, Color.White);

                spriteBatch.Draw(MegaManPlayer, MegaManPlayerRect, Color.White);

                spriteBatch.Draw(JafarPlayer, JafarPlayerRect, Color.White);

            }

            //------------------------------------------------------------------

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
//-----------------------------------------------------------------------------------------------------------------END
//--------------------------------------------------------------------------------------------------------------------