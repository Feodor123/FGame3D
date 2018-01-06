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
        public const float lightReduse = 0.9f;
        public const int maxLightPower = 20;
        public const int LIGHTREDUSE = 1;
        public static int sunLightPower = maxLightPower;
        public Chunk[,] chunks;
        public Block this[int x, int y, int z]
        {
            get
            {
                if (x < 0 || x >= chunks.GetLength(0) * Chunk.xSize || y < 0 || y >= chunks.GetLength(1) * Chunk.ySize)
                {
                    return null;
                }
                else
                {
                    return chunks[x / Chunk.xSize, y / Chunk.ySize][x % Chunk.xSize, y % Chunk.ySize, z];
                }
            }
        }
        public Block this[Vec3Int v]
        {
            get
            {
                if (v.X < 0 || v.X >= chunks.GetLength(0) * Chunk.xSize || v.Y < 0 || v.Y >= chunks.GetLength(1) * Chunk.ySize)
                {
                    return null;
                }
                else
                {
                    return chunks[v.X / Chunk.xSize, v.Y / Chunk.ySize][v.X % Chunk.xSize, v.Y % Chunk.ySize, v.Z];
                }
            }
        }
        public GameMap(Game1 game)
        {
            Generate(7, 7, game);
            for(int x = 2;x <= 4; x++)
            {
                for (int y = 2; y <= 4; y++)
                {
                    chunks[x,y].FindSunlightBlocks();
                }
            }                
            for (int i = maxLightPower; i > 1; i--)
            {
                foreach (var chunk in chunks)
                {
                    chunk.FindAllLightBlocks(i);
                }
            }
            foreach (var chunk in chunks)
            {
                for (int lightPower = maxLightPower; lightPower > 1; lightPower--)
                {
                    for (int i = 0;i < chunk.lightSourcesMap.GetLightSourceCount(lightPower); i++)
                    {
                        LightSource ls = chunk.lightSourcesMap[lightPower,i];
                        foreach(var v in Block.arrayMarkup)
                        {
                            if (this[ls.position + v.Key] != null)
                            {
                                if (this[ls.position + v.Key].rectangles[Block.arrayMarkup[-v.Key]] != null)
                                {
                                    this[ls.position + v.Key].rectangles[Block.arrayMarkup[-v.Key]].lightPower = ls.lightPower;
                                    this[ls.position + v.Key].rectangles[Block.arrayMarkup[-v.Key]].UpdateLight();
                                }
                            }
                        }
                    }
                }
            }
        }
        public void Draw(BasicEffect effect, GraphicsDeviceManager graphics, Player player)
        {
            effect.TextureEnabled = true;
            effect.VertexColorEnabled = true;
            foreach (var ch in GetVisible(player, Player.visibleDistance))
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
                    chunks[x, y] = new Chunk(x, y, this);
                }
            }
            foreach(var ch in chunks)
            {
                ch.GetRectangles();
            }
        }
        public Chunk[] GetVisible(Player player, int distance)
        {
            List<Chunk> ch = new List<Chunk>();
            int x0 = MathHelper.Max(0, (player.X - distance/Block.SIZE) / Chunk.xSize);
            int y0 = MathHelper.Max(0, (player.Y - distance / Block.SIZE) / Chunk.ySize);
            int x1 = MathHelper.Min(chunks.GetLength(0) - 1, (player.X + distance / Block.SIZE) / Chunk.xSize);
            int y1 = MathHelper.Min(chunks.GetLength(1) - 1, (player.Y + distance / Block.SIZE) / Chunk.ySize);
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
