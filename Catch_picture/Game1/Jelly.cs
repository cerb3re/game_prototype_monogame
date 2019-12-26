using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Game1
{
    class Jelly
    {
        public int speed;
        public int oldSpeed;
        public Texture2D img;
        public Vector2 position;
        public float alphaBlending;
        public Color color;
        public bool thisPictureIsClicked;
        public bool thisPictureIsAllreadyClicked;

        public Jelly(int x, int y, Texture2D img, int speed, float alphaBlending, Color color)
        {
            this.img = img;

            this.position = new Vector2();
            this.position.X = x;
            this.position.Y = y;
            this.speed = speed;
            this.oldSpeed = speed;
            this.alphaBlending = alphaBlending;
            this.color = color;
            this.thisPictureIsAllreadyClicked = false;
            this.thisPictureIsClicked = false;
        }

     
        
    }
}
