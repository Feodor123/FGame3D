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
    class GameMap
    {
        private Chunk[,] chunks;
        public Block this[int x, int y, int z]
        {
            get
            {
                return chunks[x / Chunk.xSize, y / Chunk.ySize][x % Chunk.xSize, y / Chunk.ySize, z];
            }
            /*private
            set
            {
                chunks[x / Chunk.xSize, y / Chunk.ySize][x % Chunk.xSize, y / Chunk.ySize, z] = value;
            }
            */
        }
        public GameMap(Game1 game)
        {
            Generate(5, 5, game);
        }
        public void Draw(BasicEffect effect, GraphicsDeviceManager graphics, Player player)
        {
            effect.TextureEnabled = true;
            foreach (var ch in GetVisible(player, player.visibleDistance))
            {
                foreach (var v in ch.rectangles)
                {
                    v.Draw(effect, graphics);
                }
            }
        }
        public void Generate(int width, int height, Game1 game)//many code!!!
        {
            chunks = new Chunk[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Block[,,] blocks = new Block[Chunk.xSize, Chunk.ySize, Chunk.zSize];
                    for (int xx = 0; xx < Chunk.xSize; xx++)
                    {
                        for (int yy = 0; yy < Chunk.ySize; yy++)
                        {
                            int hh = game.rnd.Next(1, 4);
                            for (int h = 0; h < x + y + 1; h++)
                            {
                                blocks[xx, yy, h] = new Block(xx, yy, h, game.textures["sand"], x, y);
                            }
                        }
                    }
                    chunks[x, y] = new Chunk(blocks, x, y);
                }
            }
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    chunks[x, y].Absorbe();
                }
            }
        }
        public Chunk[] GetVisible(Player player, int distance)
        {
            List<Chunk> ch = new List<Chunk>();
            int x0 = MathHelper.Max(0, (player.X - distance) / Chunk.xSize);
            int y0 = MathHelper.Max(0, (player.Y - distance) / Chunk.ySize);
            int x1 = MathHelper.Min(chunks.GetLength(0) - 1, (player.X + distance) / Chunk.xSize);
            int y1 = MathHelper.Min(chunks.GetLength(1) - 1, (player.Y + distance) / Chunk.ySize);
            for (int x = x0; x <= x1; x++)
            {
                for (int y = y0; y <= y1; y++)
                {
                    ch.Add(chunks[x, y]);
                }
            }
            return ch.ToArray();
        }
    }
}
