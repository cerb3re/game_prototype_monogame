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
    public class Sprite : IActor
    {
        public Vector2 Position { get; set; }
        public Rectangle BoundingBox { get; set; }
        public Texture2D Texture { get; set; }
        public bool ToRemove { get; set; }

        public float vx;
        public float vy;

        public Sprite(Texture2D texture)
        {
            this.Texture = texture;
            this.ToRemove = false;
        }

        public void Move(float x, float y)
        {
            Position = new Vector2(Position.X + x, Position.Y + y);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, BoundingBox, Color.White);
        }

        public virtual void Update(GameTime gameTime)
        {
            Move(vx, vy);

            BoundingBox = new Rectangle(
                (int)Position.X,
                (int)Position.Y,
                Texture.Width,
                Texture.Height
                );
        }

        public virtual void TouchedBy(IActor by)
        {
        }
    }
}
