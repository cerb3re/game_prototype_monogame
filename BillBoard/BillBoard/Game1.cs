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

        VertexPositionNormalTexture[] quadVertices; // vector
        AlphaTestEffect mySpriteEffect;

        //private Texture2D monsterTexture;
        private FrameAnimation monster;
        private Vector3 cameraPosition;

        private Matrix modelMatrix; // world
        private Matrix view;
        private Matrix projection;

        private float camDirection = 0;

        int[,] mapData = new int[,]
        {
            { 1,1,1,1,1,1,1,1,1,1 },
            { 1,0,2,0,0,0,0,1,1,1 },
            { 1,0,0,0,0,2,0,0,0,1 },
            { 1,1,0,1,1,1,0,1,0,1 },
            { 1,1,0,1,1,1,2,1,2,1 },
            { 1,1,0,1,1,1,0,1,1,1 },
            { 1,0,0,2,0,0,0,0,0,1 },
            { 1,0,1,1,1,1,0,1,0,1 },
            { 1,2,0,0,0,1,0,0,2,1 },
            { 1,0,1,1,1,1,1,1,1,1 }
        };

        MouseState originMouseState;

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
            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            graphics.GraphicsDevice.RasterizerState = rasterizerState;

            quadVertices = new VertexPositionNormalTexture[4];
            quadVertices[0].Position = new Vector3(-1, -1, 0); // left
            quadVertices[1].Position = new Vector3(-1, 1, 0); // up left
            quadVertices[2].Position = new Vector3(1, -1, 0); // down right
            quadVertices[3].Position = new Vector3(1, 1, 0); // up right

            // the texture coordinate are in (u,v) format
            quadVertices[0].TextureCoordinate = new Vector2(0, 1);
            quadVertices[1].TextureCoordinate = new Vector2(0, 0);
            quadVertices[2].TextureCoordinate = new Vector2(1, 1);
            quadVertices[3].TextureCoordinate = new Vector2(1, 0);

            modelMatrix = Matrix.Identity;

            cameraPosition = new Vector3(0, 0, 10);

            projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(45f),
                16f / 9f,
                1f,
                100f
                );

            CenterMouse();
            originMouseState = Mouse.GetState();

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

            //monsterTexture = Content.Load<Texture2D>("Anim_char7_idle_01");
            monster = new FrameAnimation(100);

            for (int i = 1; i <= 6; i++)
            {
                monster.AddTexture(Content.Load<Texture2D>("Anim_char7_idle_0" + i));
            }

            cubeModel = Content.Load<Model>("cube");
            mySpriteEffect = new AlphaTestEffect(GraphicsDevice)
            {
                Texture = monster.GetTexture(),
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

            MouseState currentMouseState = Mouse.GetState();

            if (currentMouseState != originMouseState)
            {
                float differenceX = currentMouseState.X - originMouseState.X;
                camDirection -= 0.001f * differenceX;
                CenterMouse();
            }

            // Rotate Camera with keyboard
            if (Keyboard.GetState().IsKeyDown(Keys.Q) && shift)  // left
            {
                camDirection += .01f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D) && shift)  // right
            {
                camDirection -= .01f;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Z) && !shift)
            {
                Matrix forwardMovement = Matrix.CreateRotationY(camDirection);
                Vector3 v = new Vector3(0, 0, -.1f);
                v = Vector3.Transform(v, forwardMovement);

                // right-hand rules, supposed that the +Z value is going in front on the screen
         
                cameraPosition.Z += v.Z; 
                cameraPosition.X += v.X;

            }

            if (Keyboard.GetState().IsKeyDown(Keys.S) && !shift)
            {
                Matrix forwardMovement = Matrix.CreateRotationY(camDirection);
                Vector3 v = new Vector3(0, 0, .1f);
                v = Vector3.Transform(v, forwardMovement);

                // right-hand rules, supposed that the +Z value is going in front on the screen

                cameraPosition.Z += v.Z;
                cameraPosition.X += v.X;

            }


            if (Keyboard.GetState().IsKeyDown(Keys.Q) && !shift)
            {
                Matrix forwardMovement = Matrix.CreateRotationY(camDirection);
                Vector3 v = new Vector3(-.1f, 0, 0);
                v = Vector3.Transform(v, forwardMovement);

                // right-hand rules, supposed that the +Z value is going in front on the screen
                cameraPosition.Z += v.Z;
                cameraPosition.X += v.X;

            }


            if (Keyboard.GetState().IsKeyDown(Keys.D) && !shift)
            {
                Matrix forwardMovement = Matrix.CreateRotationY(camDirection);
                Vector3 v = new Vector3(.1f, 0, 0);
                v = Vector3.Transform(v, forwardMovement);

                // right-hand rules, supposed that the +Z value is going in front on the screen
                cameraPosition.Z += v.Z;
                cameraPosition.X += v.X;

            }

            // Move camera up & down
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

            monster.Update(gameTime);

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

        private void Draw3DSprite(VertexPositionNormalTexture[] quad,Vector3 quadPosition,Texture2D texture, Matrix vue, Matrix projection)
        {
            mySpriteEffect.View = view;
            mySpriteEffect.Projection = projection;

            Vector3 directionVector = Vector3.Normalize(quadPosition - cameraPosition);
            Matrix lookAt = Matrix.CreateWorld(quadPosition, directionVector, Vector3.Up);

            mySpriteEffect.World = lookAt;

            mySpriteEffect.Texture = texture;

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

        private void CenterMouse()
        {
            Mouse.SetPosition(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            this.GraphicsDevice.SamplerStates[0] = SamplerState.PointClamp;

            int mapPosX = -10;
            int mapPosZ = -20;

            for (int line = 0; line < 10; line++)
            {
                for (int column = 0; column < 10; column++)
                {
                    int id = mapData[line, column];
                    int x = (column * 2) + mapPosX;
                    int z = (line * 2) + mapPosZ;

                    if (id == 1)
                    {
                        Matrix wallMatrix = Matrix.CreateWorld(new Vector3(x, 0, z), Vector3.Forward, Vector3.Up);
                        DrawModel(cubeModel, wallMatrix, view, projection);
                    }
                    else if (id == 2)
                    {
                        Draw3DSprite(quadVertices, new Vector3(x, 0, z), monster.GetTexture(), view, projection);
                    }
                }
            }

            base.Draw(gameTime);
        }
    }
}
