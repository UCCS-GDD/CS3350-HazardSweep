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
using Hazard_Sweep.Classes;
using System.Threading;

namespace Hazard_Sweep
{
    public enum Facing { Left, Right };
    public enum WeaponType { Melee, Pistol, AssaultRifle, Shotgun };

    public enum DropType { Health, PistolAmmo, AssaultAmmo, ShotgunAmmo}

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        PlayerSprite player;
        GameElements elements;
        Camera camera;
        Room street0, street1, street2, street3, street4, street5, street6, street7, street8,
            room0, room1, room2, room3, room4, room5, room6, room7, room8;
        List<int> gridNumbers;

        //Splash screen
        public enum GameState { START, PLAY, PAUSE, MENU, WIN, LOSE };
        SplashScreen splashScreen;
        MenuScreen menuScreen;
        GameState currentGameState = GameState.START;

        //pause press
        bool pressed = false;
        bool released = false;

        //sound effects
        Song music;
        SoundEffect reload, pistolFire, machineFire, shotgunFire, shells, stab, damagedPlayer, dryFire, zombieDamage;
        SoundEffect zombie1, zombie2, zombie3, zombie4, zombie5, zombie6, zombie7;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 576;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // Adds screen size
            GlobalClass.ScreenWidth = graphics.PreferredBackBufferWidth;
            GlobalClass.ScreenHeight = graphics.PreferredBackBufferHeight;

            player = new PlayerSprite(this, "Images//playerWalk", new Vector2(GlobalClass.ScreenWidth / 2,
                GlobalClass.ScreenHeight / 2), 2, 6, this);

            gridNumbers = new List<int>(Enumerable.Range(0,9));
            Shuffle(gridNumbers);

            street0 = new Room(this, "Images//Maps//External//test01", new Vector2(100, 100), 1, 1, false, false, player, Color.White, 0);
            street1 = new Room(this, "Images//Maps//External//test01", new Vector2(100, 100), 1, 1, false, false, player, Color.LightBlue,1);
            street2 = new Room(this, "Images//Maps//External//test01", new Vector2(100, 100), 1, 1, false, false, player, Color.LightCoral,2);
            street3 = new Room(this, "Images//Maps//External//test01", new Vector2(100, 100), 1, 1, false, false, player, Color.LightGoldenrodYellow, 3);
            street4 = new Room(this, "Images//Maps//External//test01", new Vector2(100, 100), 1, 1, false, false, player, Color.LightGreen,4);
            street5 = new Room(this, "Images//Maps//External//test01", new Vector2(100, 100), 1, 1, false, false, player, Color.LightGray, 5);
            street6 = new Room(this, "Images//Maps//External//test01", new Vector2(100, 100), 1, 1, false, false, player, Color.LightPink, 6);
            street7 = new Room(this, "Images//Maps//External//test01", new Vector2(100, 100), 1, 1, false, false, player, Color.LightSteelBlue, 7);
            street8 = new Room(this, "Images//Maps//External//test01", new Vector2(100, 100), 1, 1, false, false, player, Color.LightSeaGreen, 8);

            room0 = new Room(this, "Images//Maps//Internal//test01", new Vector2(100, 100), 1, 1, false, false, player, Color.White, 9);
            room1 = new Room(this, "Images//Maps//Internal//test01", new Vector2(100, 100), 1, 1, false, false, player, Color.LightBlue, 10);
            room2 = new Room(this, "Images//Maps//Internal//test01", new Vector2(100, 100), 1, 1, false, false, player, Color.LightCoral, 11);
            room3 = new Room(this, "Images//Maps//Internal//test01", new Vector2(100, 100), 1, 1, false, false, player, Color.LightGoldenrodYellow, 12);
            room4 = new Room(this, "Images//Maps//Internal//test01", new Vector2(100, 100), 1, 1, false, false, player, Color.LightGreen, 13);
            room5 = new Room(this, "Images//Maps//Internal//test01", new Vector2(100, 100), 1, 1, false, false, player, Color.LightGray, 14);
            room6 = new Room(this, "Images//Maps//Internal//test01", new Vector2(100, 100), 1, 1, false, false, player, Color.LightPink, 15);
            room7 = new Room(this, "Images//Maps//Internal//test01", new Vector2(100, 100), 1, 1, false, false, player, Color.LightSteelBlue, 16);
            room8 = new Room(this, "Images//Maps//Internal//test01", new Vector2(100, 100), 1, 1, false, false, player, Color.LightSeaGreen, 17);

            //add rooms to game
            Components.Add(street0);
            Components.Add(player);

            //add test drops to game
            //Components.Add(new itemDrop(this, null, new Vector2(300, 300)));

            //Add game components
            //Components.Add(player = new PlayerSprite(this, "Images//playerWalk", new Vector2(GlobalClass.ScreenWidth / 2,
            //    GlobalClass.ScreenHeight / 2), 2, 6));
            Components.Add(new Enemy(this, "Images//enemyWalk", new Vector2(200, 200), 2, 5));
            elements = new GameElements(this, player);
            elements.Initialize();
            //elements.LoadContent();
            camera = new Camera(this);

            //Splashscreen component
            splashScreen = new SplashScreen(this);
            menuScreen = new MenuScreen(this);
            Components.Add(splashScreen);
            Components.Add(menuScreen);

            // what is this for?
            Random rand = new Random();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Creates a service for the spritebatch so it can be used in other classes
            Services.AddService(typeof(SpriteBatch), spriteBatch);

            //load sound effects
            music = Content.Load<Song>("Sounds/Unseen Horrors");
            reload = Content.Load<SoundEffect>("Sounds/reload");
            pistolFire = Content.Load<SoundEffect>("Sounds/pistolLicenseAttribution3");
            machineFire = Content.Load<SoundEffect>("Sounds/machine gun");
            shotgunFire = Content.Load<SoundEffect>("Sounds/Shotgunsound");
            shells = Content.Load<SoundEffect>("Sounds/shell");
            stab = Content.Load<SoundEffect>("Sounds/Stab");
            damagedPlayer = Content.Load<SoundEffect>("Sounds/damagedplayer");
            dryFire = Content.Load<SoundEffect>("Sounds/Dry Fire Gun-SoundBible.com-2053652037");
            zombieDamage = Content.Load<SoundEffect>("Sounds/zombiedamage");
            zombie1 = Content.Load<SoundEffect>("Sounds/Zombie 1");
            zombie2 = Content.Load<SoundEffect>("Sounds/Zombie 2");
            zombie3 = Content.Load<SoundEffect>("Sounds/Zombie 3");
            zombie4 = Content.Load<SoundEffect>("Sounds/Zombie 4");
            zombie5 = Content.Load<SoundEffect>("Sounds/Zombie 5");
            zombie6 = Content.Load<SoundEffect>("Sounds/Zombie 6");
            zombie7 = Content.Load<SoundEffect>("Sounds/Zombie 7");


            // load game elements
            elements.LoadContent();

            //testExMap.LoadContent();

            // start music and loop
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(music);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            splashScreen.Update(gameTime);
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                pressed = true;
            }
            if (keyboardState.IsKeyUp(Keys.Escape) && pressed == true)
            {
                released = true;
            }

            if (currentGameState == GameState.PLAY)
            {
                if (pressed == true && released == true)
                {
                    currentGameState = GameState.PAUSE;
                    pressed = false;
                    released = false;
                }
                // update game elements
                elements.Update(gameTime);

                //update camera
                camera.Update(gameTime, player);
            }
            else if (currentGameState == GameState.PAUSE)
            {
                if (pressed == true && released == true)
                {
                    currentGameState = GameState.PLAY;
                    pressed = false;
                    released = false;
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            if (currentGameState == GameState.PLAY)
            {
                // draw objects
                //spriteBatch.Begin();
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null,
                    null, null, camera.transform);
                //testExMap.Draw(spriteBatch);
                base.Draw(gameTime);

                spriteBatch.End();       

            }
            
            if (currentGameState == GameState.START)
            {
                //spriteBatch.Begin();
                splashScreen.Draw(gameTime);
                //spriteBatch.End();
            }
            
            if (currentGameState == GameState.MENU)
            {
                menuScreen.Draw(gameTime);
            }

            //draw hud
            if (currentGameState == GameState.PLAY)
            {
                //foreach (IGameComponent g in Components)
                //{
                //    if (g is Reticle)
                //    {
                //        Reticle r = (Reticle)g;
                //        r.Draw(spriteBatch);
                //    }
                //}
                elements.Draw(spriteBatch);
            }

        }

        public void ChangeGameState(GameState state)
        {
            currentGameState = state;

            splashScreen.Enabled = false;
            splashScreen.Visible = false;
            menuScreen.Enabled = false;
            menuScreen.Visible = false;

            switch (currentGameState)
            {
                case GameState.START:
                    splashScreen.Enabled = true;
                    splashScreen.Visible = true;
                    break;
                case GameState.PLAY:
                    break;
                case GameState.PAUSE:
                    break;
                case GameState.MENU:
                    menuScreen.Enabled = true;
                    menuScreen.Visible = true;
                    break;
                case GameState.LOSE:
                    break;
                case GameState.WIN:
                    break;
            }
        }

        public GameState GetGameState()
        {
            return currentGameState;
        }

        // I'm sure there's a better way to do this, but it's late and I have no idea what I'm doing
        public Room GetRoom(int id)
        {
            switch (id)
            {
                case 0:
                    return street0;
                    break;
                case 1:
                    return street1;
                    break;
                case 2:
                    return street2;
                    break;
                case 3:
                    return street3;
                    break;
                case 4:
                    return street4;
                    break;
                case 5:
                    return street5;
                    break;
                case 6:
                    return street6;
                    break;
                case 7:
                    return street7;
                    break;
                case 8:
                    return street8;
                    break;
                case 9:
                    return room0;
                    break;
                case 10:
                    return room1;
                    break;
                case 11:
                    return room2;
                    break;
                case 12:
                    return room3;
                    break;
                case 13:
                    return room4;
                    break;
                case 14:
                    return room5;
                    break;
                case 15:
                    return room6;
                    break;
                case 16:
                    return room7;
                    break;
                case 17:
                    return room8;
                    break;
                default:
                    return street0;
                    break;
            }
        }

        public int GetRoomID()
        {
            for (int i = Components.Count() - 1; i > -1; i--)
            //foreach (IGameComponent g in Components)
            {
                IGameComponent g = Components[i];
                if (g is Room)
                {
                    Room r = (Room)g;
                    return r.GetID();
                }
            }
            return -1;
        }

        public void ChangeLevel(int oldLevel, int newLevel)
        {
            for (int i = Components.Count() - 1; i > -1; i--)
            //foreach (IGameComponent g in Components)
            {
                IGameComponent g = Components[i];
                if (g is Enemy)
                {
                    Enemy e = (Enemy)g;
                    Components.Remove(e);
                }
                if (g is Bullet)
                {
                    Bullet bu = (Bullet)g;
                    Components.Remove(bu);
                }
                if (g is Barricade)
                {
                    Barricade b = (Barricade)g;
                    Components.Remove(b);
                }
                if (g is Door)
                {
                    Door d = (Door)g;
                    Components.Remove(d);
                }
                if (g is NPC)
                {
                    NPC n = (NPC)g;
                    Components.Remove(n);
                }
                if (g is itemDrop)
                {
                    itemDrop item = (itemDrop)g;
                    Components.Remove(item);
                }
            }
            Components.Remove(GetRoom(oldLevel));
            Components.Add(GetRoom(newLevel));
        }

        public Camera GetCamera()
        {
            return camera;
        }

        #region Shuffling
        // found at http://stackoverflow.com/questions/273313/randomize-a-listt-in-c-sharp

        public static class ThreadSafeRandom
        {
            [ThreadStatic]
            private static Random Local;

            public static Random ThisThreadsRandom
            {
                get { return Local ?? (Local = new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId))); }
            }
        }

        public static void Shuffle(List<int> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
                int value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        #endregion

        #region sound effect methods
        //method to play reloading sound effect
        public void playReload()
        {
            SoundEffectInstance reloadInst = reload.CreateInstance();
            reloadInst.Volume = .9f;
            reloadInst.Play();
        }

        //method to play pistol shot and shell drop
        public void playPistol()
        {
            SoundEffectInstance pistolInst = pistolFire.CreateInstance();
            pistolInst.Volume = .2f;
            pistolInst.Play();
            SoundEffectInstance shellInst = shells.CreateInstance();
            shellInst.Volume = .9f;
            shellInst.Play();
        }

        //method to play machinegun shot and shell drop
        public void playMachinegun()
        {
            SoundEffectInstance machineInst = machineFire.CreateInstance();
            machineInst.Volume = .2f;
            machineInst.Play();
            SoundEffectInstance shellInst = shells.CreateInstance();
            shellInst.Volume = .9f;
            shellInst.Play();
        }

        //method to play shotgun shot and shell drop
        public void playShotgun()
        {
            SoundEffectInstance shotgunInst = shotgunFire.CreateInstance();
            shotgunInst.Volume = .2f;
            shotgunInst.Play();
            SoundEffectInstance shellInst = shells.CreateInstance();
            shellInst.Volume = .9f;
            shellInst.Play();
        }

        //method that plays the stabbing sound
        public void playStab()
        {
            SoundEffectInstance stabInst = stab.CreateInstance();
            stabInst.Volume = .5f;
            stabInst.Play();
        }

        //method to play damaged player sound
        public void playPlayerDamaged()
        {
            SoundEffectInstance playerDamInst = damagedPlayer.CreateInstance();
            playerDamInst.Volume = .7f;
            playerDamInst.Play();
        }

        //method to play dry fire sound effect
        public void playDryFire()
        {
            SoundEffectInstance dryFireInst = dryFire.CreateInstance();
            dryFireInst.Volume = .5f;
            dryFireInst.Play();
        }

        //method to play zombie damage sound effect
        public void playZombieDamage()
        {
            SoundEffectInstance zombieDamageInst = zombieDamage.CreateInstance();
            zombieDamageInst.Volume = .5f;
            zombieDamageInst.Play();
        }

        //method that plays a random zombie death noise when called
        public void playZombieDeath()
        {
            Random rand = new Random();
            int noise = rand.Next(1, 7);
            switch (noise)
            {
                case 1:
                    SoundEffectInstance temp1 = zombie1.CreateInstance();
                    temp1.Volume = .5f;
                    temp1.Play();
                    break;
                case 2:
                    SoundEffectInstance temp2 = zombie2.CreateInstance();
                    temp2.Volume = .5f;
                    temp2.Play();
                    break;
                case 3:
                    SoundEffectInstance temp3 = zombie3.CreateInstance();
                    temp3.Volume = .5f;
                    temp3.Play();
                    break;
                case 4:
                    SoundEffectInstance temp4 = zombie4.CreateInstance();
                    temp4.Volume = .5f;
                    temp4.Play();
                    break;
                case 5:
                    SoundEffectInstance temp5 = zombie5.CreateInstance();
                    temp5.Volume = .5f;
                    temp5.Play();
                    break;
                case 6:
                    SoundEffectInstance temp6 = zombie6.CreateInstance();
                    temp6.Volume = .5f;
                    temp6.Play();
                    break;
                case 7:
                    SoundEffectInstance temp7 = zombie7.CreateInstance();
                    temp7.Volume = .5f;
                    temp7.Play();
                    break;
                default:
                    SoundEffectInstance tempdef = zombie1.CreateInstance();
                    tempdef.Volume = .5f;
                    tempdef.Play();
                    break;
            }
        }
        #endregion
    }
}
//sound effect credits
//Pistol firing sound effect Recorded by GoodSoundForYou
//Shotgun sound effect Recorded by Soundeffects
//machinegunfire Recorded by WEL
//shell sound effect Recorded by Marcel
//reload effect by Mike Koenig
//game music public domain
//zombie noises made by group members