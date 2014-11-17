using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Hazard_Sweep.Classes
{
    public class Player
    {
        #region fields
        protected String textureFile;
        protected Texture2D texture;
        protected Vector2 position;
        protected Vector2 center;
        protected Color color;
        protected Random random;
        //protected SpriteBatch sb;
        protected Game game;
        protected Rectangle boundingBox;

        //variables
        protected Vector2 movement;
        protected MouseState ms;
        protected Vector2 mousePosition;
        protected Facing direction;
        protected int health;
        protected Rectangle drawRectangle;
        protected int spriteRows, spriteCols;

        // weapon variables
        Weapon weapon;
        protected WeaponType weaponSelect;
        public bool hasMelee;
        public bool hasPistol;
        public bool hasAssaultRifle;
        public bool hasShotgun;
        Weapon melee;
        Weapon pistol;
        Weapon assaultRifle;
        Weapon shotgun;
        protected Vector2 bulletOrigin;

        // animation variables
        int frameCount;
        int animationSpeed = 10;
        float prevX;
        float prevY;
        bool animate;

        //contain player in room
        bool contained = true;
        Vector2 previousPosition;

        //activation area
        bool inArea = false;
        bool nearNPC = false;
        Vector2 newPosition;
        bool displayActivationMessage = false;
        bool teleportReleased = true;
        int newRoom;
        int currentRoom;

        Rectangle boundary;
        #endregion

        //Constructor
        public Player(Game game, string textureFile, Vector2 position, int spriteRows, int spriteCols)
        {
            this.textureFile = textureFile;
            this.position = position;
            color = Color.White;
            random = new Random();
            this.game = game;

            health = 100;

            //randomize weapons spawn later
            hasMelee = true;
            hasPistol = true;
            hasAssaultRifle = true;
            hasShotgun = true;

            //sets initial direction
            direction = Facing.Left;

            //set rows and columns
            this.spriteRows = spriteRows;
            this.spriteCols = spriteCols;

            // create weapons
            melee = new Weapon(WeaponType.Melee, game, 0, 0, 15);
            pistol = new Weapon(WeaponType.Pistol, game, 48, 12, 12);
            assaultRifle = new Weapon(WeaponType.AssaultRifle, game, 60, 30, 5);
            shotgun = new Weapon(WeaponType.Shotgun, game, 24, 6, 20);

            // set starting weapon
            weapon = pistol;

            // set up animation
            frameCount = 0;
            prevX = position.X;
            prevY = position.Y;
            animate = false;

            //set previous position
            previousPosition = position;

            // this.DrawOrder = 10;
        }

        //load content
        public void LoadContent()
        {
            texture = game.Content.Load<Texture2D>(textureFile);
            center = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);
            boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            // sets up bullet origin vector has to be here so texture is loaded when looking at width and height
            bulletOrigin = new Vector2(texture.Width / spriteCols / 2, texture.Height / spriteRows / 2);
            game.Components.Add(weapon);
            drawRectangle = new Rectangle(0, 0, texture.Width / spriteCols, texture.Height / spriteRows);
        }

        // update method
        public void Update(GameTime gameTime, Game1.GameState gameState)
        {
            if (gameState == Game1.GameState.PLAY)
            {
                #region collision
                //checks for collisions with room's bounding box
                foreach (GameComponent g in game.Components)
                {
                    if (g is Room)
                    {
                        Room r = (Room)g;
                        currentRoom = r.GetID();

                        //collision logic
                        Rectangle b = r.Boundary;
                        SetBoundary(b);
                        if (!b.Contains(this.boundingBox))
                        {
                            contained = false;
                        }
                    }
                    else if (g is Door)
                    {
                        Door d = (Door)g;

                        //collision logic
                        Rectangle b = d.getActivationArea();
                        bool isActive = d.getActive();
                        if (b.Intersects(this.boundingBox) && isActive)
                        {
                            inArea = true;
                            newPosition = d.getExitLocation();
                            newRoom = d.getDestination();
                        }
                    }
                    else if (g is NPC)
                    {
                        NPC n = (NPC)g;

                        //collision logic
                        Rectangle b = n.getActivationArea();
                        //bool isActive = d.getActive();
                        if (b.Intersects(this.boundingBox))
                        {
                            nearNPC = true;
                        }
                    }
                }

                #endregion

                #region input
                KeyboardState keyboardState = Keyboard.GetState();
                ms = Mouse.GetState();

                //activation if player is in activation area
                if (keyboardState.IsKeyDown(Keys.E) && inArea == true && teleportReleased)
                {
                    displayActivationMessage = true;
                    inArea = false;
                    // Maybe but the changeLevel method somewhere else? can't access from here
                    // ((Game1)Game).ChangeLevel(currentRoom, newRoom);
                    
                    //this.DrawOrder = 20;
                    this.position = newPosition;
                    teleportReleased = false;
                }

                if (keyboardState.IsKeyDown(Keys.E) && nearNPC == true)
                {
                    // in the future, we'll have this display dialogue to give guests
                    nearNPC = false;
                    // ((Game1)Game).ChangeGameState(Game1.GameState.WIN);
                }

                if (keyboardState.IsKeyUp(Keys.E))
                {
                    teleportReleased = true;
                    inArea = false;
                    nearNPC = false;
                }

                //logic for animation
                if (direction == Facing.Left)
                {
                    drawRectangle.Y = texture.Height / spriteRows;
                }
                else if (direction == Facing.Right)
                {
                    drawRectangle.Y = 0;
                }

                //move the sprite with WASD
                if (keyboardState.IsKeyDown(Keys.D))
                {
                    direction = Facing.Right;
                    if (position.X < boundary.Right)
                    {
                        position.X += 5;
                    }
                }
                else if (keyboardState.IsKeyDown(Keys.A))
                {
                    direction = Facing.Left;
                    if (position.X > boundary.Left)
                    {
                        position.X -= 5;
                    }
                }
                else if (keyboardState.IsKeyDown(Keys.W))
                {
                    if (position.Y > boundary.Top)
                    {
                        position.Y -= 5;
                    }
                }
                else if (keyboardState.IsKeyDown(Keys.S))
                {
                    if (position.Y < boundary.Bottom)
                    {
                        position.Y += 5;
                    }
                }

                contained = true;

                // weapon handling
                if (keyboardState.IsKeyDown(Keys.Space))
                {
                    weapon.shoot(position + bulletOrigin, direction);
                }
                if (keyboardState.IsKeyDown(Keys.R))
                {
                    weapon.reload();
                }

                // changing weapons
                if (keyboardState.IsKeyDown(Keys.D1))
                {
                    if (hasMelee == true)
                    {
                        game.Components.Remove(weapon);
                        weapon = melee;
                        game.Components.Add(weapon);
                    }
                }
                if (keyboardState.IsKeyDown(Keys.D2))
                {
                    if (hasPistol == true)
                    {
                        game.Components.Remove(weapon);
                        weapon = pistol;
                        game.Components.Add(weapon);
                    }
                }
                if (keyboardState.IsKeyDown(Keys.D3))
                {
                    if (hasAssaultRifle == true)
                    {
                        game.Components.Remove(weapon);
                        weapon = assaultRifle;
                        game.Components.Add(weapon);
                    }
                }
                if (keyboardState.IsKeyDown(Keys.D4))
                {
                    if (hasShotgun == true)
                    {
                        game.Components.Remove(weapon);
                        weapon = shotgun;
                        game.Components.Add(weapon);
                    }
                }
                #endregion

                #region animate

                // check for movement
                if (position.X == prevX && position.Y == prevY)
                {
                    animate = false;
                    frameCount = 0;
                }
                else
                {
                    animate = true;
                }

                // if player is moving, animate
                if (animate)
                {
                    if (frameCount == animationSpeed)
                    {
                        // check for rollover on sprite strip
                        if ((drawRectangle.X + drawRectangle.Width) > (texture.Width - drawRectangle.Width))
                        {
                            drawRectangle.X = 0;
                        }
                        else
                        {
                            drawRectangle.X += drawRectangle.Width;
                        }

                        // restart timer
                        frameCount = 0;
                    }
                }

                // increment animation assets as appropriate
                frameCount += 1;
                prevX = position.X;
                prevY = position.Y;

                //set previous position
                previousPosition = position;
                #endregion

                //ends the game if player's health is 0, will later go to menus
                if (health <= 0)
                {
                    //((Game1)Game).ChangeGameState(Game1.GameState.LOSE);
                }

                boundingBox.X = (int)position.X;
                boundingBox.Y = (int)position.Y;
            }
        }

        //draw method
        public void Draw(GameTime gameTime, SpriteBatch sb)
        {
            //if (((Game1)Game).GetGameState() == Game1.GameState.PLAY)
            //{
                //sb = Game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;
                //sb.Begin();
                // sb.Draw(texture, position, drawRectangle, Color.White);
                sb.Draw(texture, position, drawRectangle, Color.White, 0f, new Vector2(0f, 0f), new Vector2(2f, 2f), SpriteEffects.None, 0.5f);
                //sb.End();
            //}
        }

        //returns the health
        public int GetHealth()
        {
            return health;
        }

        // returns player's weapon
        public Weapon GetWeapon()
        {
            return weapon;
        }

        //returns player's position
        public Vector2 getPlayerPosition()
        {
            return position;
        }

        //returns center
        public Vector2 GetCenter()
        {
            return center;
        }

        //sets the player's health
        public void reducePlayerHealth(int damage)
        {
            health -= damage;
        }

        public void SetBoundary(Rectangle b)
        {
            boundary = b;
        }
    }
}
