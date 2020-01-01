using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePattern
{
    public delegate void OnClick(Button sender);

    public class Button : Sprite
    {
        public bool isHover { get; private set; }
        private MouseState oldMouseSate;
        public OnClick OnClick { get; set; }

        public Button(Texture2D texture) : base(texture)
        {

        }

        public override void Update(GameTime gameTime)
        {
            MouseState newMouseState = Mouse.GetState();
            Point MousePosition = Mouse.GetState().Position;

            if (BoundingBox.Contains(MousePosition))
            {
                if (!isHover)
                {
                    isHover = true;
                }
            }
            else
            {
                if (isHover == true)
                {

                }
                isHover = false;
            }

            if (isHover)
            {
                if (newMouseState.LeftButton == ButtonState.Pressed && oldMouseSate.LeftButton == ButtonState.Released)
                {
                    if (OnClick != null)
                    {
                        OnClick(this);
                    }
                }
            }

            oldMouseSate = newMouseState;
            base.Update(gameTime);
        }
    }
}
