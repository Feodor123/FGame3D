using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGame3D
{
    public struct Vec3Int
    {
        public int X;
        public int Y;
        public int Z;
        public Vec3Int(int X = 0,int Y = 0,int Z = 0)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }
        public static Vec3Int operator +(Vec3Int v1,Vec3Int v2)
        {
            return new Vec3Int(v1.X + v2.X,v1.Y + v2.Y,v1.Z + v2.Z);
        }
        public static Vec3Int operator -(Vec3Int v1, Vec3Int v2)
        {
            return new Vec3Int(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }
        public static Vec3Int operator -(Vec3Int v)
        {
            return new Vec3Int(-v.X,-v.Y,-v.Z);
        }
        public static Vec3Int operator *(Vec3Int v1, Vec3Int v2)
        {
            return new Vec3Int(v1.X * v2.X, v1.Y * v2.Y, v1.Z * v2.Z);
        }
        public static Vec3Int operator *(Vec3Int v1, int i2)
        {
            return new Vec3Int(v1.X * i2, v1.Y * i2, v1.Z * i2);
        }
    }
}
