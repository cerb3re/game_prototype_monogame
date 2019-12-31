using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePattern
{
    class AssetManager
    {
        public static SpriteFont MainFont { get; private set; }

        public static void Load(String font, ContentManager contentManager)
        {
            MainFont = contentManager.Load<SpriteFont>(font);
        }
    }
}
