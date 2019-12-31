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

            base.Load();
        }

        public override void Unload()
        {
            base.Unload();
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState newkeyboardState = Keyboard.GetState();

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