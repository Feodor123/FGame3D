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
    class Block
    {
        private Vec3Int position;
        public int X
        {
            get
            {
                return position.X;
            }
        }
        public int Y
        {
            get
            {
                return position.Y;
            }
        }
        public int Z
        {
            get
            {
                return position.Z;
            }
        }
        public int mapX
        {
            get
            {
                return position.X + chunk.mapX;
            }
        }
        public int mapY
        {
            get
            {
                return position.Y + chunk.mapY;
            }
        }
        private Chunk chunk;
        private GameMap map;
        public BlockType blockType;
        public VertexRectangle[] rectangles;
        public const int SIZE = 5;
        public Block(int x,int y,int z,Chunk chunk,GameMap map,int typeId)
        {
            position = new Vec3Int(x,y,z);
            this.chunk = chunk;
            this.map = map;
            blockType = BlockType.blockTypes[typeId];
        }
        public void GetAbsorbed()
        {
            List<VertexRectangle> rec = new List<VertexRectangle>();
            if (Z != 0 && map[mapX, mapY, Z - 1] == null)
                rec.Add(new VertexRectangle(SIZE * new Vector3(mapX + 1, mapY, Z), SIZE * new Vector3(mapX, mapY + 1, Z), blockType.atlasRectangles[0], blockType.rectanglesColors[0]));
            else
                rec.Add(null);
            if (map[mapX - 1,mapY, Z] == null)
                rec.Add(new VertexRectangle(SIZE * new Vector3(mapX, mapY, Z), SIZE * new Vector3(mapX, mapY + 1, Z + 1), blockType.atlasRectangles[1], blockType.rectanglesColors[1]));
            else
                rec.Add(null);
            if (map[mapX, mapY + 1, Z] == null)
                rec.Add(new VertexRectangle(SIZE * new Vector3(mapX, mapY + 1, Z), SIZE * new Vector3(mapX + 1, mapY + 1, Z + 1), blockType.atlasRectangles[2], blockType.rectanglesColors[2]));
            else
                rec.Add(null);
            if (map[mapX + 1, mapY, Z] == null)
                rec.Add(new VertexRectangle(SIZE * new Vector3(mapX + 1, mapY + 1, Z), SIZE * new Vector3(mapX + 1, mapY, Z + 1), blockType.atlasRectangles[3], blockType.rectanglesColors[3]));
            else
                rec.Add(null);
            if (map[mapX, mapY - 1, Z] == null)
                rec.Add(new VertexRectangle(SIZE * new Vector3(mapX + 1, mapY, Z),SIZE * new Vector3(mapX, mapY, Z + 1), blockType.atlasRectangles[4], blockType.rectanglesColors[4]));
            else
                rec.Add(null);
            if (Z != Chunk.zSize - 1 && map[mapX,mapY, Z + 1] == null)
                rec.Add(new VertexRectangle(SIZE * new Vector3(mapX, mapY, Z + 1),SIZE * new Vector3(mapX + 1, mapY + 1, Z + 1), blockType.atlasRectangles[5], blockType.rectanglesColors[5]));
            else
                rec.Add(null);
            rectangles = rec.ToArray();
        }
        public static Dictionary<Vec3Int, int> arrayMarkup = new Dictionary<Vec3Int, int>()
        {
            { new Vec3Int(0,0,-1),0 },
            { new Vec3Int(-1,0,0),1 },
            { new Vec3Int(0,1,0) ,2 },
            { new Vec3Int(1,0,0) ,3 },
            { new Vec3Int(0,-1,0),4 },
            { new Vec3Int(0,0,1) ,5 },
        };
    }
}