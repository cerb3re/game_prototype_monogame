using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePattern
{
    public class GameState
    {
        public enum SceneType
        {
            Menu,
            Gameplay,
            Gameover
        }

        protected Main main;
        public AScene CurrentScene { get; set; }

        public GameState(Main main)
        {
            this.main = main;
        }

        public void ChangeScene(SceneType sceneType)
        {
            if (CurrentScene != null)
            {
                CurrentScene.Unload();
                CurrentScene = null;
            }

            switch (sceneType)
            {
                case SceneType.Menu:
                    CurrentScene = new SceneMenu(main);
                    break;
                case SceneType.Gameplay:
                    CurrentScene = new SceneGamePlay(main);
                    break;
                case SceneType.Gameover:
                    CurrentScene = new SceneGameOver(main);
                    break;
                default:
                    break;
            }

            CurrentScene.Load();
        }
    }
}
