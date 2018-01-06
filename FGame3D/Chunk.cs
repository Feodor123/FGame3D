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
        public int X;
        public int mapX
        {
            get
            {
                return X * xSize;
            }
        }
        public int Y;
        public int mapY
        {
            get
            {
                return Y * ySize;
            }
        }
        public Vec3Int MapAdding
        {
            get
            {
                return new Vec3Int(mapX, mapY,0);
            }
        }
        public const int xSize = 16;
        public const int ySize = 16;
        public const int zSize = 256;        
        private GameMap map;
        public List<LightSource> sunLightSources = new List<LightSource>();
        public List<LightSource> stuffLightSources = new List<LightSource>();
        public LightSourcesContainer lightSourcesMap;
        public List<VertexRectangle> rectangles;
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
        }
        public Block this[Vec3Int v]
        {
            get
            {
                if (v.X < 0 || v.X >= xSize || v.Y < 0 || v.Y >= ySize || v.Z < 0 || v.Z >= zSize)
                {
                    return null;
                }
                else
                {
                    return blocks[v.X, v.Y, v.Z];
                }
            }
        }
        public Chunk(int x, int y,GameMap map)
        {
            X = x;
            Y = y;
            this.map = map;
            lightSourcesMap = new LightSourcesContainer(map, this);
            Generate();
        }
        private void Generate()
        {
            for (int xx = 0;xx < xSize; xx++)
            {
                for (int yy = 0;yy < ySize; yy++)
                {
                    for (int h = 0;h < X % 2 + Y % 2 + 1; h++)
                    {
                        blocks[xx, yy, h] = new Block(xx,yy,h,this,map,1);
                    }
                }
            }
        }
        public void GetRectangles()
        {
            rectangles = new List<VertexRectangle>();
            foreach (var b in blocks)
            {
                if (b != null)
                {
                    b.GetAbsorbed();
                    rectangles.AddRange(Array.FindAll(b.rectangles, _ => (_ != null)));
                }
            }
        }
        public void FindSunlightBlocks()
        {
            for (int xx = 0; xx < xSize; xx++)
            {
                for (int yy = 0; yy < ySize; yy++)
                {
                    for (int z = zSize - 1;z >= 0; z--)
                    {
                        if (this[X,Y,z] != null && !this[X, Y, z].blockType.isTransparent)
                        {
                            break;
                        }
                        foreach (var v in Block.arrayMarkup)
                        {
                            if (map[xx + mapX + v.Key.X,yy + mapY + v.Key.Y,z + v.Key.Z] != null)
                            {
                                LightSource l = new LightSource(new Vec3Int(xx + mapX, yy + mapY, z), GameMap.sunLightPower);
                                sunLightSources.Add(l);
                                lightSourcesMap[xx + mapX + v.Key.X, yy + mapY + v.Key.Y, z + v.Key.Z] = l;
                                break;
                            }
                        }
                    }
                }
            }
        }
        public void FindAllLightBlocks(int lightPower)
        {
            for (int i = 0;i < lightSourcesMap.GetLightSourceCount(lightPower);i++)
            {
                LightSource ls = lightSourcesMap[lightPower, i];
                foreach (var v in Block.arrayMarkup)
                {
                    if (map[ls.position + MapAdding + v.Key] == null || map[ls.position + MapAdding + v.Key].blockType.isTransparent)
                    {
                        if (lightSourcesMap[ls.position + v.Key] == null)
                        {
                            lightSourcesMap[ls.position + v.Key] = new LightSource(ls.position + v.Key,ls.lightPower - GameMap.LIGHTREDUSE);
                        }
                        else if (lightSourcesMap[ls.position + v.Key].lightPower < ls.lightPower - GameMap.LIGHTREDUSE)
                        {
                            lightSourcesMap.Remove(ls.position + v.Key);
                            lightSourcesMap[ls.position + v.Key] = new LightSource(ls.position + v.Key, ls.lightPower - GameMap.LIGHTREDUSE);
                        }
                    }
                }
            }
        }
    }
}