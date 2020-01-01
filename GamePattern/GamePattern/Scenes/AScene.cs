using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePattern
{
    abstract public class AScene
    {
        protected Main main;
        protected List<IActor> listActors;

        public AScene(Main main)
        {
            this.main = main;
            this.listActors = new List<IActor>();
        }

        public void Clean()
        {
            listActors.RemoveAll(item => item.ToRemove == true);
        }

        public virtual void Load()
        {

        }

        public virtual void Unload()
        {

        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (IActor actor in listActors)
            {
                actor.Update(gameTime);
            }
        }

        public virtual void Draw(GameTime gameTime)
        {
            foreach (IActor actor in listActors)
            {
                actor.Draw(main.spriteBatch);
            }
        }
    }
}
