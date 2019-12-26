using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cube3D
{
    class MapEditor
    {
        private MainGame mainGame;
        public bool IsActive { get; set; }
        private SpriteFont font;

        public MapEditor(MainGame game)
        {
            this.mainGame = game;
            this.font = this.mainGame.Content.Load<SpriteFont>("pixelfont");
            this.IsActive = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (this.IsActive)
            {
                spriteBatch.DrawString(font, "INTEGRATED MAP EDITOR", new Vector2(0, 0), Color.White);
            }
        }

        public void Active()
        {
            this.IsActive = !this.IsActive;
        }
    }
}
