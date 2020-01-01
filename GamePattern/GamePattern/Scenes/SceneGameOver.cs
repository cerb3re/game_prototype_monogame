using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GamePattern
{
    class SceneGameOver : AScene
    {
        KeyboardState oldKeyboardState;
        GamePadState oldGamePadState;
        private Button button;

        public SceneGameOver(Main main) : base(main)
        {
        }

        public override void Draw(GameTime gameTime)
        {
            main.spriteBatch.DrawString(AssetManager.MainFont, "GAMEOVER", Vector2.Zero, Color.White);

            base.Draw(gameTime);
        }

        public override void Load()
        {
            Rectangle screen = main.Window.ClientBounds;
            button = new Button(main.Content.Load<Texture2D>("button"));
            oldKeyboardState = Keyboard.GetState();
            oldGamePadState = GamePad.GetState(PlayerIndex.One, GamePadDeadZone.IndependentAxes);

            button.Position = new Vector2((screen.Width / 2) - button.Texture.Width / 2, (screen.Height / 2) - button.Texture.Height / 2);
            button.OnClick = onClickPlay;

            listActors.Add(button);

            base.Load();
        }

        public override void Unload()
        {
            base.Unload();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public void onClickPlay(Button sender)
        {
            main.GameState.ChangeScene(GameState.SceneType.Gameplay);
        }
    }
}
