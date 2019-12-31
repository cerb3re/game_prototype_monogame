using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace GamePattern
{
    class Hero : Sprite
    {
        public int Energy;

        public Hero(Texture2D texture) : base(texture)
        {
            this.Energy = 100;
        }
    }

    class Meteor : Sprite
    {
        public Meteor(Texture2D texture) : base(texture)
        {
            do
            {
                vy = (float)Utils.GetInt(-3, 3) / 5;
            } while (vy == 0);
            do
            {
                vx = (float)Utils.GetInt(-3, 3) / 5;
            } while (vx == 0);

        }
    }

    class SceneGamePlay : AScene
    {
        private KeyboardState oldKeyboardState;
        private Hero hero;


        public SceneGamePlay(Main main) : base(main)
        {

        }

        public override void Load()
        {
            oldKeyboardState = Keyboard.GetState();

            Rectangle screen = main.Window.ClientBounds;
            hero = new Hero(main.Content.Load<Texture2D>("ship"));
            hero.Position = new Vector2((screen.Width / 2) - hero.Texture.Width / 2, (screen.Height / 2) - hero.Texture.Height / 2);
            listActors.Add(hero);
            
            for (int i = 0; i < 20; i++)
            {
                Meteor m = new Meteor(main.Content.Load<Texture2D>("meteor"));
                m.Position = new Vector2(
                    Utils.GetInt(1, screen.Width - m.Texture.Width),
                    Utils.GetInt(1, screen.Height - m.Texture.Height)
                    );
                listActors.Add(m);
            }

            base.Load();
        }

        public override void Unload()
        {
            base.Unload();
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState newkeyboardState = Keyboard.GetState();
            Rectangle Screen = main.Window.ClientBounds;

            foreach (IActor actor in listActors)
            {
                if (actor is Meteor)
                {
                    Meteor m = (Meteor)actor;

                    if (m.Position.X < 0)
                    {
                        m.vx = 0 - m.vx;
                        m.Position = new Vector2(0, m.Position.Y);
                    }
                    if (m.Position.X + m.Texture.Width > Screen.Width)
                    {
                        m.vx = 0 - m.vx;
                        m.Position = new Vector2(Screen.Width - m.BoundingBox.Width, m.Position.Y);
                    }
                    if (m.Position.Y < 0)
                    {
                        m.vy = 0 - m.vy;
                        m.Position = new Vector2(m.Position.X, 0);
                    }
                    if (m.Position.Y + m.Texture.Height > Screen.Height)
                    {
                        m.vy = 0 - m.vy;
                        m.Position = new Vector2(m.Position.X, Screen.Height - m.BoundingBox.Height);
                    }

                }
            }

            if (newkeyboardState.IsKeyDown(Keys.G) && !oldKeyboardState.IsKeyDown(Keys.G))
            {
                Console.WriteLine("ok");
            }

            if (newkeyboardState.IsKeyDown(Keys.Right))
            {
                hero.Move(1, 0);
            }

            oldKeyboardState = newkeyboardState;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            main.spriteBatch.DrawString(AssetManager.MainFont, "GAMEPLAY", Vector2.Zero, Color.White);

            base.Draw(gameTime);
        }
    }
}