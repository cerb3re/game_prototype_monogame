﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace _3D_ISO
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D img1;
        List<Texture2D> lstTexture2D;
        List<Texture2D> lstTexture3D;
        TileMap myMap;
        Vector2 map2DOrigin;
        Vector2 map3DOrigin;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 600;
            graphics.ApplyChanges();

            myMap = new TileMap();
            myMap.set2DSize(32, 32);
            myMap.set3DSize(32 * 2, 32 * 2);

            int[,] mapData = new int[,]
            {
                {2, 2, 1, 2, 2, 2, 1, 2, 2, 2},
                {2, 2, 1, 2, 2, 2, 2, 2, 2, 2},
                {2, 2, 2, 2, 2, 2, 2, 2, 2, 2},
                {2, 2, 2, 2, 2, 2, 2, 2, 2, 2},
                {2, 2, 2, 2, 2, 2, 2, 2, 2, 2},
                {2, 2, 2, 2, 2, 2, 2, 2, 2, 2},
                {2, 2, 2, 2, 2, 2, 2, 2, 2, 2},
                {2, 2, 2, 2, 2, 2, 2, 2, 2, 2},
                {2, 2, 2, 2, 2, 2, 2, 2, 2, 2},
                {2, 2, 2, 2, 2, 2, 2, 2, 2, 2},
            };

            myMap.setData(mapData);
            map2DOrigin = new Vector2(10, (graphics.PreferredBackBufferHeight / 2) - ((myMap.tileHeigth2D * myMap.mapHeight) / 2));
            map3DOrigin = new Vector2(10 + (myMap.tileWidth2D * myMap.mapWidth) + (myMap.tileWidth3D * (myMap.mapWidth / 2)), ((myMap.tileHeigth2D * myMap.mapHeight) / 2));
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
 

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

            lstTexture2D = new List<Texture2D>();
            lstTexture3D = new List<Texture2D>();

            Texture2D tx;
            tx = Content.Load<Texture2D>("stone2D");
            lstTexture2D.Add(tx);
            tx = Content.Load<Texture2D>("dirt2D");
            lstTexture2D.Add(tx);

            tx = Content.Load<Texture2D>("stone3D");
            lstTexture3D.Add(tx);
            tx = Content.Load<Texture2D>("dirt3D");
            lstTexture3D.Add(tx);


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

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

          
            spriteBatch.Begin();
            // 2D TileMap
            for (int line = 0; line < myMap.mapWidth; line++)
            {
                for (int column = 0; column < myMap.mapHeight; column++)
                {
                    int id = myMap.getId(line, column);
                    if (id >= 0)
                    {
                        int x = column * myMap.tileWidth2D;
                        int y = line * myMap.tileHeigth2D;
                        Vector2 position = new Vector2(x, y);
                        Texture2D tx = lstTexture2D[id - 1];
                        if (tx != null)
                        {
                            position = position + map2DOrigin;
                            spriteBatch.Draw(tx, position, Color.White);
                        }
                    }
                }
            }

            // 3D TileMap
            for (int line = 0; line < myMap.mapWidth; line++)
            {
                for (int column = 0; column < myMap.mapHeight; column++)
                {
                    int id = myMap.getId(line, column);
                    if (id >= 0)
                    {
                        int x = column * myMap.tileWidth2D;
                        int y = line * myMap.tileHeigth2D;
                        Vector2 position = new Vector2(x, y);
                        position = myMap.to3D(position);
                        Texture2D tx = lstTexture3D[id - 1];
                        if (tx != null)
                        {
                            position = position + map3DOrigin;
                            spriteBatch.Draw(tx, position, Color.White);
                        }
                    }
                }
            }
            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
