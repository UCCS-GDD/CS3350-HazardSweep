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
    public class PlayerSprite : Sprite
    {
        Game1 gameRef;

        //variables
        protected Vector2 movement = Vector2.Zero;
        protected MouseState ms;
        protected Vector2 mousePosition;
        protected Facing direction;
        protected int health;
        protected Rectangle drawRectangle;
        protected Rectangle collisionRec;
        protected int spriteRows, spriteCols;
        private bool stabPressed = false;

        //variables for hit animations
        Color color = Color.White;
        int colorTimer;
        bool shouldColor;

        // weapon variables
        Weapon weapon;
        protected WeaponType weaponSelect;
        public bool hasMelee = false;
        public bool hasPistol = false;
        public bool hasAssaultRifle = false;
        public bool hasShotgun = false;
        MeleeWeapon melee;
        Weapon pistol;
        Weapon assaultRifle;
        Weapon shotgun;
        protected Vector2 bulletOrigin;
        protected Vector2 bulletDirection;

        //reticle
        public Reticle reticle;

        // animation variables
        Texture2D playerWalkPistol, playerWalkRifle, playerWalkShotgun;
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
        bool displayNPCMessage = false;
        bool teleportReleased = true;
        int newRoom;
        int currentRoom;

        //bug fix variables
        private int afterMenuTimer = 30;

        Rectangle boundary;

        SpriteFont instructFont;
        int displayTimer = 0;

        //Constructor
        public PlayerSprite(Game game, string textureFile, Vector2 position, int spriteRows, int spriteCols, Game1 gameRef)
            : base(game, textureFile, position, 10)
        {
            this.gameRef = gameRef;

            health = 100;
            color = Color.White;
            colorTimer = 2;
            shouldColor = false;

            //randomize weapons spawn later
            hasMelee = true;
            //if (randomNumGen(0, 3) == 1)
            //{
            hasPistol = true;
            //}
            if (randomNumGen(0, 4) == 1)
            {
                hasAssaultRifle = true;
            }
            if (randomNumGen(0, 4) == 1)
            {
                hasShotgun = true;
            }

            //sets initial direction
            direction = Facing.Left;

            //set rows and columns
            this.spriteRows = spriteRows;
            this.spriteCols = spriteCols;

            // create weapons
            if (hasPistol)
            {
                pistol = new Weapon(WeaponType.Pistol, game, 48, 12, 12);
            }
            if (hasAssaultRifle)
            {
                assaultRifle = new Weapon(WeaponType.AssaultRifle, game, 60, 30, 5);
            }
            if (hasShotgun)
            {
                shotgun = new Weapon(WeaponType.Shotgun, game, 24, 6, 20);
            }

            // set starting weapon
            weapon = pistol;

            // set up animation
            frameCount = 0;
            prevX = position.X;
            prevY = position.Y;
            animate = false;

            //set previous position
            previousPosition = position;

            this.DrawOrder = 10;

        }

        //load content
        protected override void LoadContent()
        {
            base.LoadContent();

            // sprites for weapon types
            playerWalkPistol = game.Content.Load<Texture2D>("Images//playerWalkPistol");
            playerWalkRifle = game.Content.Load<Texture2D>("Images//playerWalkRifle");
            playerWalkShotgun = game.Content.Load<Texture2D>("Images//playerWalk");

            // font for entering doors
            instructFont = Game.Content.Load<SpriteFont>(@"Fonts\VtksMoney_30");

            //reticle
            reticle = new Reticle(game, "Images//reticle", new Vector2(0, 0), 20, gameRef);
            game.Components.Add(reticle);

            // sets up bullet origin vector has to be here so texture is loaded when looking at width and height
            bulletOrigin = new Vector2(texture.Width / spriteCols / 2, texture.Height / spriteRows / 2);
            game.Components.Add(weapon);
            drawRectangle = new Rectangle(0, 0, texture.Width / spriteCols, texture.Height / spriteRows);

            //create melee weapon
            melee = new MeleeWeapon(game, bulletOrigin, 100);

            //set the size and initial position of the bounding box
            boundingBox.Height = texture.Height / spriteRows;
            boundingBox.Width = texture.Width / spriteCols;
            boundingBox.X = (int)position.X;
            boundingBox.Y = (int)position.Y;

            //collision detection
            collisionRec.Height = texture.Height;
            collisionRec.Width = texture.Width / 3;
            collisionRec.X = boundingBox.X;
            collisionRec.Y = boundingBox.Y;
        }

        // update method
        public override void Update(GameTime gameTime)
        {
            if (((Game1)Game).GetGameState() == Game1.GameState.PLAY)
            {
                if (afterMenuTimer > 0)
                {
                    afterMenuTimer--;
                }

                KeyboardState keyboardState = Keyboard.GetState();
                ms = (game as Game1).GetMouseState();                

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
                        if (b.Intersects(this.collisionRec) && isActive)
                        {
                            inArea = true;
                            displayActivationMessage = true;
                            newPosition = d.getExitLocation();
                            newRoom = d.getDestination();
                            displayTimer = 0;
                        }
                    }
                    else if (g is NPC)
                    {
                        NPC n = (NPC)g;

                        //collision logic
                        Rectangle b = n.getActivationArea();
                        //bool isActive = d.getActive();
                        if (b.Intersects(this.collisionRec))
                        {
                            nearNPC = true;
                            displayNPCMessage = true;
                            displayTimer = 0;
                        }
                    }
                    else if (g is itemDrop)
                    {
                        itemDrop i = (itemDrop)g;

                        //collision logic
                        Rectangle b = i.getRectangle();
                        if (b.Intersects(this.collisionRec))
                        {
                            DropType d = i.getDropType();
                            int v = i.getDropValue();
                            switch (d)
                            {
                                case (DropType.Health):
                                    if ((health + v) <= 100)
                                    {
                                        health += v;
                                    }
                                    else
                                    {
                                        health = 100;
                                    }
                                    break;
                                case (DropType.AssaultAmmo):
                                    if (!hasAssaultRifle)
                                    {
                                        hasAssaultRifle = true;
                                        assaultRifle = new Weapon(WeaponType.AssaultRifle, game, 60, 30, 5);
                                    }
                                    else
                                    {
                                        assaultRifle.addBullets(v);
                                    }
                                    break;
                                case (DropType.PistolAmmo):
                                    if (!hasPistol)
                                    {
                                        hasPistol = true;
                                        pistol = new Weapon(WeaponType.Pistol, game, v, 12, 12);
                                    }
                                    else
                                    {
                                        pistol.addBullets(v);
                                    }
                                    break;
                                case (DropType.ShotgunAmmo):
                                    if (!hasShotgun)
                                    {
                                        hasShotgun = true;
                                        shotgun = new Weapon(WeaponType.Shotgun, game, v, 6, 20);
                                    }
                                    else
                                    {
                                        shotgun.addBullets(v);
                                    }
                                    break;
                            }
                            //removal logic
                            i.removeItem();
                        }
                    }
                }

                //activation if player is in activation area
                if (keyboardState.IsKeyDown(Keys.E) && inArea == true && teleportReleased)
                {
                    displayActivationMessage = false;
                    inArea = false;
                    ((Game1)Game).ChangeLevel(currentRoom, newRoom);
                    this.DrawOrder = 20;
                    this.position = newPosition;
                    teleportReleased = false;
                }

                if (keyboardState.IsKeyDown(Keys.E) && nearNPC == true)
                {
                    // in the future, we'll have this display dialogue to give quests
                    nearNPC = false;
                    displayNPCMessage = false;
                    ((Game1)Game).ChangeGameState(Game1.GameState.WIN);
                }

                if (keyboardState.IsKeyUp(Keys.E))
                {
                    teleportReleased = true;
                    inArea = false;
                    nearNPC = false;
                }

                if(displayTimer < 2 && !inArea)
                {
                    displayTimer++;
                }
                if(displayTimer == 2)
                {
                    displayActivationMessage = false;
                    displayNPCMessage = false;
                    displayTimer = 0;
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
                    //if (position.X < boundary.Right)
                    if (position.X < 2300)
                    {
                        movement.X += 5;
                    }

                }
                if (keyboardState.IsKeyDown(Keys.A))
                {
                    //if (position.X > boundary.Left)
                    if (position.X > 10)
                        movement.X -= 5;
                }
                if (keyboardState.IsKeyDown(Keys.W))
                {
                    //if (position.Y > boundary.Top)
                    if (position.Y > 184)
                        movement.Y -= 5;
                }
                if (keyboardState.IsKeyDown(Keys.S))
                {
                    //if (position.Y < boundary.Bottom)
                    if (position.Y < 416)
                        movement.Y += 5;
                }

                movement.Normalize();
                if (movement.Length() > 0)
                {
                    movement.X *= 5;
                    movement.Y *= 5;
                    position.X = movement.X + position.X;
                    position.Y = movement.Y + position.Y;
                }
                movement = Vector2.Zero;

                contained = true;

                //update mouse position and bullet direction
                mousePosition.X = reticle.readPosition.X;
                mousePosition.Y = reticle.readPosition.Y;
                bulletDirection = mousePosition - position;
                bulletDirection.Y -= 30;
                if(bulletDirection != Vector2.Zero)
                {
                    bulletDirection.Normalize();
                }

                //update direction
                if(bulletDirection.X < 0)
                {
                    direction = Facing.Left;
                }
                else
                {
                    direction = Facing.Right;
                }

                // update bulletOrigin
                // pistol
                if (weapon.GetWeaponType() == WeaponType.Pistol && direction == Facing.Left)
                    bulletOrigin = new Vector2(texture.Width / spriteCols / 2 - 32, texture.Height / spriteRows / 2 + 18);
                if (weapon.GetWeaponType() == WeaponType.Pistol && direction == Facing.Right)
                    bulletOrigin = new Vector2(texture.Width / spriteCols / 2 + 64, texture.Height / spriteRows / 2 + 17);
                // assault rifle
                if (weapon.GetWeaponType() == WeaponType.AssaultRifle && direction == Facing.Left)
                    bulletOrigin = new Vector2(texture.Width / spriteCols / 2 - 33, texture.Height / spriteRows / 2 + 11);
                if (weapon.GetWeaponType() == WeaponType.AssaultRifle && direction == Facing.Right)
                    bulletOrigin = new Vector2(texture.Width / spriteCols / 2 + 75, texture.Height / spriteRows / 2 + 12);
                // shotgun
                if (weapon.GetWeaponType() == WeaponType.Shotgun && direction == Facing.Left)
                    bulletOrigin = new Vector2(texture.Width / spriteCols / 2 - 33, texture.Height / spriteRows / 2 + 14);
                if (weapon.GetWeaponType() == WeaponType.Shotgun && direction == Facing.Right)
                    bulletOrigin = new Vector2(texture.Width / spriteCols / 2 + 75, texture.Height / spriteRows / 2 + 13);

                // weapon handling
                if (ms.LeftButton == ButtonState.Pressed && afterMenuTimer == 0)
                {
                    weapon.shoot(position + bulletOrigin, bulletDirection);
                }
                if (keyboardState.IsKeyDown(Keys.C))
                {
                    stabPressed = true;
                }
                if (stabPressed == true && keyboardState.IsKeyUp(Keys.C))
                {
                    melee.attack(direction, position);
                    (game as Game1).playStab();
                    stabPressed = false;
                }
                if (keyboardState.IsKeyDown(Keys.R))
                {
                    weapon.reload();
                }

                // changing weapons
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

                // change sprite for weapon
                if (weapon.GetWeaponType() == WeaponType.Pistol)
                    texture = playerWalkPistol;
                if (weapon.GetWeaponType() == WeaponType.AssaultRifle)
                    texture = playerWalkRifle;
                if (weapon.GetWeaponType() == WeaponType.Shotgun)
                    texture = playerWalkShotgun;

                //ends the game if player's health is 0, will later go to menus
                if (health <= 0)
                {
                    ((Game1)Game).ChangeGameState(Game1.GameState.LOSE);
                }

                // animate

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
                base.Update(gameTime);

                //player damage representation
                if (shouldColor)
                {
                    color = Color.Red;
                    colorTimer = 5;
                    shouldColor = false;
                }

                //reduce color timer
                colorTimer--;

                //reset the color
                if (colorTimer <= 0)
                {
                    color = Color.White;
                }
                collisionRec.X = boundingBox.X;
                collisionRec.Y = boundingBox.Y;
            }
        }

        //draw method
        public override void Draw(GameTime gameTime)
        {
            if (((Game1)Game).GetGameState() == Game1.GameState.PLAY)
            {
                sb = Game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;
                //    sb.Begin();
                // sb.Draw(texture, position, drawRectangle, Color.White);
                sb.Draw(texture, position, drawRectangle, color, 0f, new Vector2(0f, 0f), new Vector2(2f, 2f), SpriteEffects.None, 0.5f);
                //  sb.End();

                if (displayActivationMessage) 
                    sb.DrawString(instructFont, "press E to proceed", new Vector2(position.X - 32, position.Y - 64), Color.Black);
                if (displayNPCMessage)
                    sb.DrawString(instructFont, "press E to talk to scientist", new Vector2(position.X - 32, position.Y - 64), Color.Black);
            }
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
            shouldColor = true;
            health -= damage;
            (game as Game1).playPlayerDamaged();
        }

        //returns bounding box
        public Rectangle getRectangle()
        {
            return collisionRec;
        }

        public void SetBoundary(Rectangle b)
        {
            boundary = b;
        }
    }
}
