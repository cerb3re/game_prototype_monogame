using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace LunarLander
{
    
    public class Lander
    {
        public Texture2D engineImg { get; set; }
        public Texture2D shipImg { get; set; }
        public Vector2 position { get; set; } = Vector2.Zero;
        public Vector2 velocity { get; set; } = Vector2.Zero;
        public float speed { get; set; } = 0.02f;
        public float speedMax { get; set; } = 2f;
        public bool engineOn { get; set; } = false;
        public float angle { get; set; } = 270; // 90 + 90 + 90 (bas, gauche, haut) -- initialement à droite

        internal void Update()
        {
            Vector2 exponentialVelocity = new Vector2(0, 0.005f);

            velocity += exponentialVelocity;
            position += velocity;
        }
    }

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Lander lander;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            lander = new Lander
            {
                position = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2),
                shipImg = Content.Load<Texture2D>("ship"),
                engineImg = Content.Load<Texture2D>("engine"),
            };

        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                lander.angle -= 2;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                lander.angle += 2;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                lander.engineOn = true;

                float angle_radian = MathHelper.ToRadians(lander.angle);
                float angle_x = (float)Math.Cos(angle_radian) * lander.speed;
                float angle_y = (float)Math.Sin(angle_radian) * lander.speed;

                lander.velocity += new Vector2(angle_x, angle_y);
            }
            else
            {
                lander.engineOn = false;
            }


            lander.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            Vector2 origin = new Vector2(lander.shipImg.Width / 2, lander.shipImg.Height / 2);

            spriteBatch.Begin();
            spriteBatch.Draw(lander.shipImg, lander.position, null, Color.White, MathHelper.ToRadians(lander.angle), origin, new Vector2(1, 1), SpriteEffects.None, 0);
            if (lander.engineOn)
            {
                Vector2 originEngin = new Vector2(lander.engineImg.Width / 2, lander.engineImg.Height / 2);

                spriteBatch.Draw(lander.engineImg, lander.position, null, Color.White, MathHelper.ToRadians(lander.angle), originEngin, new Vector2(1, 1), SpriteEffects.None, 0);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
