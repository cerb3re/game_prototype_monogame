using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BillBoard
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Model cubeModel;

        VertexPosition[] quadVertices; // vector
        BasicEffect mySpriteEffect;

        private Vector3 cameraPosition;

        private Matrix modelMatrix; // world
        private Matrix view;
        private Matrix projection;

        private float camDirection = 0;

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
            quadVertices = new VertexPosition[4];
            quadVertices[0].Position = new Vector3(-1, -1, 0); // left
            quadVertices[1].Position = new Vector3(-1, 1, 0); // up left
            quadVertices[2].Position = new Vector3(1, -1, 0); // down right
            quadVertices[3].Position = new Vector3(1, 1, 0); // up right

            modelMatrix = Matrix.Identity;

            cameraPosition = new Vector3(0, 0, 10);

            projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(45f),
                16f / 9f,
                1f,
                100f
                );

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

            cubeModel = Content.Load<Model>("cube");
            mySpriteEffect = new BasicEffect(GraphicsDevice)
            {
                FogEnabled = true,
                FogStart = 2,
                FogEnd = 20
            };
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

            bool shift = Keyboard.GetState().IsKeyDown(Keys.LeftShift) || Keyboard.GetState().IsKeyDown(Keys.RightShift);

            // Rotate Camera with keyboard
            if (Keyboard.GetState().IsKeyDown(Keys.Q) && shift)  // left
            {
                camDirection += .01f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D) && shift)  // right
            {
                camDirection -= .01f;
            }

            // Move camera up & down
            if (Keyboard.GetState().IsKeyDown(Keys.Z) && !shift)
            {
                Matrix forwardMovement = Matrix.CreateRotationY(camDirection);
                Vector3 v = new Vector3(0, 0, .1f);
                v = Vector3.Transform(v, forwardMovement);

                // right-hand rule, supposed that the +Z value is going in front on the screen
         
                cameraPosition.Z -= v.Z; 
                cameraPosition.X -= v.X;

            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                cameraPosition += new Vector3(0, .05f, 0);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                cameraPosition += new Vector3(0, -.05f, 0);
            }

            Matrix rotationMatrix = Matrix.CreateRotationY(camDirection);
            Vector3 forwardNormal = rotationMatrix.Forward;

            view = Matrix.CreateLookAt(
                cameraPosition,
                cameraPosition + (forwardNormal * 10f),
                Vector3.Up
                );

            base.Update(gameTime);
        }

        private void DrawModel(Model model, Matrix world, Matrix view, Matrix projection)
        {

            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = world;
                    effect.View = view;
                    effect.Projection = projection;
                    effect.FogEnabled = true;
                    effect.FogStart = 2;
                    effect.FogEnd = 20;
                }
                mesh.Draw();
            }

        }

        private void Draw3DSprite(VertexPosition[] quad, Matrix world, Matrix vue, Matrix projection)
        {
            mySpriteEffect.View = view;
            mySpriteEffect.Projection = projection;
            mySpriteEffect.World = world;

            foreach(var pass in mySpriteEffect.CurrentTechnique.Passes)
            {
                pass.Apply();

                graphics.GraphicsDevice.DrawUserPrimitives(
                    PrimitiveType.TriangleStrip,
                    quad,
                    0,
                    2
                    );
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            Draw3DSprite(quadVertices, Matrix.Identity, view, projection);
            //DrawModel(cubeModel, modelMatrix, view, projection);

            base.Draw(gameTime);
        }
    }
}
