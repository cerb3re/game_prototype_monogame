using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrollingBackground
{
    class Background
    {
        private Vector2     position;
        private Texture2D   picture;
        private float       velocity;
        private float       size;

        public Background(Texture2D picture, float velocity, float size)
        {
            this.picture  = picture;
            this.velocity = velocity;
            this.position = Vector2.Zero;
            this.size     = size;
        }

        public void Update()
        {
            position.X += this.velocity;
            if (position.X <= 0 - this.size)
            {
                position.X = 0;
            }
        }

        public Vector2 Position
        {
            get
            {
                return position;
            }
        }

        public Texture2D Picture
        {
            get
            {
                return picture;
            }
        }
    }
}
