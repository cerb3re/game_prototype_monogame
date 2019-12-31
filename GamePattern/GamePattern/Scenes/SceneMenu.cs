using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GamePattern
{
    class SceneMenu : AScene
    {
        KeyboardState oldKeyboardState;
        GamePadState oldGamePadState;

        public SceneMenu(Main main) : base(main)
        {

        }

        public override void Load()
        {
            oldKeyboardState = Keyboard.GetState();
            oldGamePadState = GamePad.GetState(PlayerIndex.One, GamePadDeadZone.IndependentAxes);

            base.Load();
        }

        public override void Unload()
        {
            base.Unload();
        }

        public override void Update(GameTime gameTime)
        {
            // TODO: check if exit is enter and "ask if sure ?"
            KeyboardState newkeyboardState = Keyboard.GetState();
            GamePadCapabilities capabilitiesPlayerOne = GamePad.GetCapabilities(PlayerIndex.One);
            GamePadState newGamePadState;
            bool buttonA = false;

            // touch Button A is pressed
            if (capabilitiesPlayerOne.IsConnected)
            {
                newGamePadState = GamePad.GetState(PlayerIndex.One, GamePadDeadZone.IndependentAxes);
                if (newGamePadState.IsButtonDown(Buttons.A) == true && oldGamePadState.IsButtonDown(Buttons.A) == false)
                {
                    buttonA = true;
                }
            }

            // touch space pressed
            if (newkeyboardState.IsKeyDown(Keys.Space) && !oldKeyboardState.IsKeyDown(Keys.Space))
            {
                main.GameState.ChangeScene(GameState.SceneType.Gameplay);
            }

            // touch Enter is pressed
            if (newkeyboardState.IsKeyDown(Keys.Enter) && oldKeyboardState.IsKeyDown(Keys.Enter) || buttonA)
            {
                main.GameState.ChangeScene(GameState.SceneType.Gameplay);
            }
            oldKeyboardState = newkeyboardState;

            if (capabilitiesPlayerOne.IsConnected)
            {
                newGamePadState = GamePad.GetState(PlayerIndex.One, GamePadDeadZone.IndependentAxes);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            main.spriteBatch.DrawString(AssetManager.MainFont, "MENU", Vector2.Zero , Color.White);

            base.Draw(gameTime);
        }
    }
}
