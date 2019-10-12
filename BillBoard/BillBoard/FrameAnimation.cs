using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillBoard
{
    class FrameAnimation
    {
        private List<Texture2D> frameTextures;
        private int currentFrame;
        private double frameTime;
        private double timer;

        public FrameAnimation(double frameTime)
        {
            timer = 0;
            currentFrame = 0;
            this.frameTime = frameTime;
            this.frameTextures = new List<Texture2D>();
        }

        public void AddTexture(Texture2D texture)
        {
            this.frameTextures.Add(texture);
        }

        public void Update(GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (timer >= frameTime)
            {
                currentFrame++;
                if (currentFrame > frameTextures.Count - 1)
                    currentFrame = 0;
                timer = 0;
            }
        }

        public Texture2D GetTexture()
        {
            return frameTextures[currentFrame];
        }
    }
}
