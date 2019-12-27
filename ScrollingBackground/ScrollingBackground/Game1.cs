using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace ScrollingBackground
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager   graphics;
        SpriteBatch             spriteBatch;
        SpriteFont font;
        float size;

        Texture2D urbanScrolling0;
        Texture2D urbanScrolling1;
        Texture2D urbanScrolling2;
        Texture2D urbanScrolling3;

        Background background0;
        Background background1;
        Background background2;
        Background background3;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            size = GraphicsDevice.Viewport.Width;

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

            urbanScrolling0 = Content.Load<Texture2D>("urban_scrolling0");
            urbanScrolling1 = Content.Load<Texture2D>("urban_scrolling1");
            urbanScrolling2 = Content.Load<Texture2D>("urban_scrolling2");
            urbanScrolling3 = Content.Load<Texture2D>("urban_scrolling3");

            background0 = new Background(urbanScrolling0, -1, size);
            background1 = new Background(urbanScrolling1, -3, size);
            background2 = new Background(urbanScrolling2, -4, size);
            background3 = new Background(urbanScrolling3, -5, size);

            font = Content.Load<SpriteFont>("fonts");

            // TODO: use this.Content to load your game content here
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

            background0.Update();
            background1.Update();
            background2.Update();
            background3.Update();

            base.Update(gameTime);
        }

        private void DrawBackground(Background background)
        {
            spriteBatch.Draw(background.Picture, background.Position, Color.White);
            if (background.Position.X < 0)
            {
                spriteBatch.Draw(background.Picture, new Vector2(background.Position.X + background.Picture.Width, 0), Color.White);
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            DrawBackground(background0);
            DrawBackground(background3);
            DrawBackground(background2);
            DrawBackground(background1);
            spriteBatch.DrawString(font, "TANGUY CHENIER - SCROLLING PARALLAX", new Vector2((size / 2) - font.Texture.Width, GraphicsDevice.Viewport.Height / 3), Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
