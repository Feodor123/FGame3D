using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGame3D
{
    class LightSourcesContainer
    {
        public const int xSize = Chunk.xSize;
        public const int ySize = Chunk.ySize;
        public const int zSize = Chunk.zSize;
        private Chunk chunk;
        private GameMap map;
        private List<LightSource>[] allLightSources = new List<LightSource>[GameMap.maxLightPower + 1];
        public int GetLightSourceCount(int lightPower)
        {
            return allLightSources[lightPower].Count;
        }
        private LightSource[,,] lightSources = new LightSource[xSize,ySize,zSize];
        public LightSource this[int lightPower, int i]
        {
            get
            {
                return allLightSources[lightPower][i];
            }
            set
            {
                Vec3Int v = GetChunkPos(value.position.X, value.position.Y);
                if (v.X != chunk.X || v.Y != chunk.Y)
                {
                    map.chunks[v.X, v.Y].lightSourcesMap[value.lightPower,i] = value;
                }
                else
                {
                    allLightSources[value.lightPower][i] = value;
                }
            }
        }
        public LightSource this[int lightPower]
        {
            set
            {
                Vec3Int v = GetChunkPos(value.position.X,value.position.Y);
                if (v.X != chunk.X || v.Y != chunk.Y)
                {
                    map.chunks[v.X, v.Y].lightSourcesMap[value.lightPower] = value;
                }
                else
                {
                    allLightSources[value.lightPower].Add(value);
                }
            }
        }
        public LightSource this[int mapX,int mapY,int mapZ]
        {
            get
            {
                if (mapZ < 0 || mapZ >= zSize)
                {
                    return null;
                }
                else
                {
                    Vec3Int v = GetChunkPos(mapX, mapY);
                    if (v.X != chunk.X || v.Y != chunk.Y)
                    {
                        return map.chunks[v.X,v.Y].lightSourcesMap[mapX,mapY,mapZ];
                    }
                    else
                    {
                        Normalise(ref mapX, ref mapY);
                        return lightSources[mapX, mapY ,mapZ];
                    }
                }
            }
            set
            {
                if (mapZ >= 0 && mapZ < zSize)
                {
                    Vec3Int v = GetChunkPos(mapX, mapY);
                    if (v.X != chunk.X || v.Y != chunk.Y)
                    {
                        map.chunks[v.X, v.Y].lightSourcesMap[mapX, mapY, mapZ] = value;
                    }
                    else
                    {
                        Normalise(ref mapX, ref mapY);
                        lightSources[mapX, mapY, mapZ] = value;
                        allLightSources[value.lightPower].Add(value);
                    }
                }
            }
        }
        public LightSource this[Vec3Int mapPos]
        {
            get
            {
                if (mapPos.Z < 0 || mapPos.Z >= zSize)
                {
                    return null;
                }
                else
                {
                    Vec3Int v = GetChunkPos(mapPos.X, mapPos.Y);
                    if (v.X != chunk.X || v.Y != chunk.Y)
                    {
                        return map.chunks[v.X, v.Y].lightSourcesMap[mapPos];
                    }
                    else
                    {
                        Normalise(ref mapPos.X,ref mapPos.Y);
                        return lightSources[mapPos.X, mapPos.Y, mapPos.Z];
                    }
                }
            }
            set
            {
                if (mapPos.Z >= 0 && mapPos.Z < zSize)
                {
                    Vec3Int v = GetChunkPos(mapPos.X, mapPos.Y);
                    if (v.X != chunk.X || v.Y != chunk.Y)
                    {
                        map.chunks[v.X, v.Y].lightSourcesMap[mapPos] = value;
                    }
                    else
                    {
                        Normalise(ref mapPos.X, ref mapPos.Y);
                        lightSources[mapPos.X, mapPos.Y, mapPos.Z] = value;
                        allLightSources[value.lightPower].Add(value);
                    }
                }
            }
        }

        public LightSourcesContainer(GameMap map,Chunk chunk)
        {
            this.chunk = chunk;
            this.map = map;
            for (int i = 1;i < allLightSources.Length; i++)
            {
                allLightSources[i] = new List<LightSource>(); 
            }
        }

        private Vec3Int Normalise(ref int x,ref int y)
        {
            Vec3Int v = new Vec3Int();
            v.X = (int)Math.Floor((float)x / xSize);
            v.Y = (int)Math.Floor((float)y / ySize);
            x = x - (int)Math.Floor((float)x / xSize) * xSize;
            y = y - (int)Math.Floor((float)y / ySize) * ySize;
            return v;
        }
        private Vec3Int GetChunkPos(int x,int y)
        {
            Vec3Int v = new Vec3Int();
            v.X = (int)Math.Floor((float)x / xSize);
            v.Y = (int)Math.Floor((float)y / ySize);
            x = x - (int)Math.Floor((float)x / xSize) * xSize;
            y = y - (int)Math.Floor((float)y / ySize) * ySize;
            return v;
        }

        public void Remove(LightSource ls)
        {
            Vec3Int position = ls.position;
            Vec3Int v = Normalise(ref position.X, ref position.Y);
            map.chunks[v.X, v.Y].lightSourcesMap.RemoveThere(position);
        }
        public void Remove(Vec3Int mapPos)
        {
            Vec3Int v = Normalise(ref mapPos.X, ref mapPos.Y);
            map.chunks[v.X, v.Y].lightSourcesMap.RemoveThere(mapPos);
        }
        public void Remove(int mapX, int mapY, int mapZ) { Remove(new Vec3Int(mapX, mapY, mapZ)); }
        public void RemoveThere(Vec3Int chunkPos)
        {
            allLightSources[lightSources[chunkPos.X, chunkPos.Y, chunkPos.Z].lightPower].Remove(lightSources[chunkPos.X, chunkPos.Y, chunkPos.Z]);
            lightSources[chunkPos.X, chunkPos.Y, chunkPos.Z] = null;
        }//private!!!
        public void RemoveThere(int chunkX, int chunkY, int chunkZ) { RemoveThere(new Vec3Int(chunkX, chunkY, chunkZ)); }//private!!!
    }
}
