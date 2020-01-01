using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePattern
{
    public interface IActor
    {
        Vector2 Position { get; set; }
        Rectangle BoundingBox { get; set; }

        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
        void TouchedBy(IActor by);
        bool ToRemove { get; set; }
    }
}
