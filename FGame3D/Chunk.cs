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
    class Chunk
    {
        public int x;
        public int y;
        public const int xSize = 16;
        public const int ySize = 16;
        public const int zSize = 256;
        public List<VertexRectangle> rectangles = new List<VertexRectangle>();
        private Block[,,] blocks = new Block[xSize, ySize, zSize];
        public Block this[int x, int y, int z]
        {
            get
            {
                if (x < 0 || x >= xSize || y < 0 || y >= ySize || z < 0 || z >= zSize)
                {
                    return null;
                }
                else
                {
                    return blocks[x, y, z];
                }
            }
            /*private
            set
            {
                blocks[x, y, z]  = value;
            }
            */
        }
        public Chunk(Block[,,] blocks, int x, int y)
        {
            this.x = x;
            this.y = y;
            this.blocks = blocks;
        }
        public void Absorbe()
        {
            foreach (var b in blocks)
            {
                if (b != null)
                {
                    rectangles.AddRange(b.GetAbsorbed(this));
                }
            }
        }
    }
}

