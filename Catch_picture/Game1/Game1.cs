using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D img;
        List<Jelly> lstJelly = new List<Jelly>();
        Jelly jelly;
        SpriteEffects effect = SpriteEffects.None;
        MouseState oldMouseState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsFixedTimeStep = true;
            IsMouseVisible = true;
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

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
            Random random = new Random();

            for (int i = 0; i < 10; i++)
            {
                img = Content.Load<Texture2D>("metalPanel");
                int x = random.Next(0, GraphicsDevice.Viewport.Width - img.Width);
                int y = random.Next(0, GraphicsDevice.Viewport.Height - img.Height);
                int speed = random.Next(1, 5);
                int alphaBlending = random.Next(0, 1);

                this.jelly = new Jelly(x, y, img, speed, alphaBlending, Color.White);
                this.lstJelly.Add(jelly);
            }

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            MouseState mouseState = Mouse.GetState();
            bool bClic = false;

            if (mouseState.LeftButton == ButtonState.Pressed && ButtonState.Released == oldMouseState.LeftButton)
            {
                bClic = true;   
            }
            oldMouseState = mouseState;


            for (int i = lstJelly.Count - 1; i >= 0; i--)
            {
                Jelly item = lstJelly[i];

                item.position.X += item.speed;

                if (item.position.X > GraphicsDevice.Viewport.Width - item.img.Width)
                {
                    item.position.X = GraphicsDevice.Viewport.Width - item.img.Width;
                    item.speed = 0 - item.speed;
                }
                if (item.position.X < 0)
                {
                    item.position.X = 0;
                    item.speed = 0 - item.speed;

                }

                if (item.speed > 0)
                {
                    this.effect = SpriteEffects.FlipHorizontally;
                }
                else
                {
                    this.effect = SpriteEffects.FlipHorizontally;
                }
                
                if (bClic == true && item.thisPictureIsClicked == false)
                {
                    if (mouseState.X >= item.position.X && mouseState.Y >= item.position.Y
                        && mouseState.X <= item.position.X + item.img.Width && mouseState.Y <= item.position.Y + item.img.Height)
                    {
                        bClic = false;

                        if (item.thisPictureIsAllreadyClicked == false)
                        {
                            item.thisPictureIsAllreadyClicked = true;
                            item.color = Color.Red;
                            item.oldSpeed = item.speed;
                            item.speed = 0;
                        }
                        else
                        {
                            Console.WriteLine(item.thisPictureIsClicked);
                            item.thisPictureIsAllreadyClicked = false;
                            item.color = Color.White;
                            item.speed = item.oldSpeed;
                        }
                    }
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
            GraphicsDevice.Clear(Color.CornflowerBlue);
            

            spriteBatch.Begin();

            for (int i = 0; i < 10; i++)
            {
                spriteBatch.Draw(lstJelly[i].img, lstJelly[i].position, null, lstJelly[i].color * 0.5f, 0, Vector2.Zero, 1.0f, effect, 0);
            }


            spriteBatch.End();
          

            base.Draw(gameTime);
        }
    }
}
