using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace _3D_ISO
{
    class TileMap
    {
        public int mapWidth
        {
            get;
            private set;
        }
        public int mapHeight
        {
            get;
            private set;
        }
        public int tileWidth2D
        {
            get;
            private set;
        }
        public int tileHeigth2D
        {
            get;
            private set;
        }
        public int tileWidth3D
        {
            get;
            private set;
        }
        public int tileHeigth3D
        {
            get;
            private set;
        }

        private int[,] _data;
        
        public TileMap()
        {

        }

        public void set2DSize(int pTileWidth, int pTileHeight)
        {
            tileWidth2D = pTileWidth;
            tileHeigth2D = pTileHeight;
        }

        public void set3DSize(int pTileWidth, int pTileHeight)
        {
            tileWidth3D = pTileWidth;
            tileHeigth3D = pTileHeight;
        }

        public void setData(int[,] pArray)
        {
            _data = pArray;
            mapHeight = pArray.GetLength(0);
            mapWidth = pArray.GetLength(1);
        }

        public int getId(int pLine, int pColumn)
        {
            if (pLine >= 0 && pLine < mapHeight && pColumn >= 0 && pColumn < mapWidth)
                return _data[pLine, pColumn];

            Console.WriteLine("Error: the index of the map does not exist");
            return -1;
        }

        public Vector2 to3D(Vector2 pCoord2D)
        {
            Vector2 newCoord = new Vector2();
            newCoord.X = pCoord2D.X - pCoord2D.Y;
            newCoord.Y = (pCoord2D.X + pCoord2D.Y) / 2;
            return newCoord;
        }
    }
}
