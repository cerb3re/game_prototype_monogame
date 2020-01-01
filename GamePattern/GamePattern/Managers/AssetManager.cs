using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePattern
{
    class AssetManager
    {
        public static ContentManager ContentManager;
        public static SpriteFont MainFont { get; private set; }
        public static Song Music { get; private set; }

        public static void Load(ContentManager contentManager)
        {
            ContentManager = contentManager;
        }

        public static void FontManager(String font)
        {
            MainFont = ContentManager.Load<SpriteFont>(font);
        }

        public static void SongManager(String song)
        {
            Music = ContentManager.Load<Song>(song);
        }
    }
}
