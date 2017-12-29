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
    class Block : Parallelepiped
    {
        private int x;
        private int y;
        private int z;
        public const int size = 5;
        public Block(int x, int y, int z, Texture2D texture, int chunkX, int chunkY) : this(x, y, z, new Texture2D[] { texture, texture, texture, texture, texture, texture }, chunkX, chunkY) { }
        public Block(int x, int y, int z, Texture2D[] textures, int chunkX, int chunkY) : base(new Vector3((x + Chunk.xSize * chunkX) * size, (y + Chunk.ySize * chunkY) * size, z * size), size, size, size, textures)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public VertexRectangle[] GetAbsorbed(Chunk chunk)
        {
            List<VertexRectangle> rec = new List<VertexRectangle>();
            if (z != 0 && chunk[x, y, z - 1] == null)
                rec.Add(vertexRectangles[0]);
            if (chunk[x - 1, y, z] == null)
                rec.Add(vertexRectangles[1]);
            if (chunk[x, y + 1, z] == null)
                rec.Add(vertexRectangles[2]);
            if (chunk[x + 1, y, z] == null)
                rec.Add(vertexRectangles[3]);
            if (chunk[x, y - 1, z] == null)
                rec.Add(vertexRectangles[4]);
            if (z != Chunk.zSize - 1 && chunk[x, y, z + 1] == null)
                rec.Add(vertexRectangles[5]);
            return rec.ToArray();
        }
    }
}