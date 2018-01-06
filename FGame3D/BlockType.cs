using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FGame3D
{
    class BlockType
    {
        public enum IdName
        {
            None = 0,
            Sand = 1,
        };
        public static Dictionary<int, BlockType> blockTypes = new Dictionary<int, BlockType>()
        {
            {0 ,new BlockType(0 ,new Rectangle(),false)},
            {1 ,new BlockType(1 ,new Rectangle(0,0,1,1),true)},
            {2 ,new BlockType(2 ,new Rectangle(),false)},
            {3 ,new BlockType(3 ,new Rectangle(),false)},
            {4 ,new BlockType(4 ,new Rectangle(),false)},
            {5 ,new BlockType(5 ,new Rectangle(),false)},
            {6 ,new BlockType(6 ,new Rectangle(),false)},
            {7 ,new BlockType(7 ,new Rectangle(),false)},
            {8 ,new BlockType(8 ,new Rectangle(),false)},
            {9 ,new BlockType(9 ,new Rectangle(),false)},
            {10,new BlockType(10,new Rectangle(),false)},
            {11,new BlockType(11,new Rectangle(),false)},
            {12,new BlockType(12,new Rectangle(),false)},
            {13,new BlockType(13,new Rectangle(),false)},
            {14,new BlockType(14,new Rectangle(),false)},
            {15,new BlockType(15,new Rectangle(),false)},
            {16,new BlockType(16,new Rectangle(),false)},
            {17,new BlockType(17,new Rectangle(),false)},
            {18,new BlockType(18,new Rectangle(),false)},
            {19,new BlockType(19,new Rectangle(),false)},
            {20,new BlockType(20,new Rectangle(),false)},
            {21,new BlockType(21,new Rectangle(),false)},
            {22,new BlockType(22,new Rectangle(),false)},
            {23,new BlockType(23,new Rectangle(),false)},
            {24,new BlockType(24,new Rectangle(),false)},
            {25,new BlockType(25,new Rectangle(),false)},
            {26,new BlockType(26,new Rectangle(),false)},
            {27,new BlockType(27,new Rectangle(),false)},
            {28,new BlockType(28,new Rectangle(),false)},
            {29,new BlockType(29,new Rectangle(),false)},
            {30,new BlockType(30,new Rectangle(),false)},
            {31,new BlockType(31,new Rectangle(),false)},
        };
        public Rectangle[] atlasRectangles;
        public bool isTransparent = false;//прозрачный
        public int id;
        public bool isObstacle;
        public bool isLightSourse;
        public int lightPower;
        public Color[] rectanglesColors;

        private BlockType(int id, Rectangle[] atlasRectangles,bool isObstacle, bool isLightSourse, int lightPower,Color[] rectanglesColors)
        {
            this.id = id;
            this.rectanglesColors = rectanglesColors;
            this.atlasRectangles = atlasRectangles;
            this.isObstacle = isObstacle;
            this.isLightSourse = isLightSourse;
            this.lightPower = lightPower;
        }
        private BlockType(int id, Rectangle[] atlasRectangles, bool isObstacle, Color[] rectanglesColors) : this(id,atlasRectangles,isObstacle,false,0, rectanglesColors) { }

        private BlockType(int id, Rectangle atlasRectangles, bool isObstacle, bool isLightSourse, int lightPower, Color[] rectanglesColors) : this(id, new Rectangle[] { atlasRectangles, atlasRectangles, atlasRectangles, atlasRectangles, atlasRectangles, atlasRectangles }, isObstacle, isLightSourse, lightPower, rectanglesColors) { }
        private BlockType(int id, Rectangle atlasRectangles, bool isObstacle,Color[] rectanglesColors) : this(id, atlasRectangles, isObstacle, false, 0, rectanglesColors) { }


        private BlockType(int id, Rectangle[] atlasRectangles, bool isObstacle, bool isLightSourse, int lightPower) : this(id, atlasRectangles, isObstacle, isLightSourse, lightPower, new Color[] { Color.White, Color.White, Color.White, Color.White, Color.White, Color.White }) { }
        private BlockType(int id, Rectangle[] atlasRectangles, bool isObstacle) : this(id, atlasRectangles, isObstacle, false, 0, new Color[] { Color.White, Color.White, Color.White, Color.White, Color.White, Color.White }) { }

        private BlockType(int id, Rectangle atlasRectangles, bool isObstacle, bool isLightSourse, int lightPower) : this(id, new Rectangle[] { atlasRectangles, atlasRectangles, atlasRectangles, atlasRectangles, atlasRectangles, atlasRectangles }, isObstacle, isLightSourse, lightPower, new Color[] { Color.White, Color.White, Color.White, Color.White, Color.White, Color.White }) { }
        private BlockType(int id, Rectangle atlasRectangles, bool isObstacle) : this(id, atlasRectangles, isObstacle, false, 0, new Color[] { Color.White, Color.White, Color.White, Color.White, Color.White, Color.White }) { }
    }
}
