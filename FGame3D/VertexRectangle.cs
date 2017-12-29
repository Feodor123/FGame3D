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
    class VertexRectangle
    {
        public int type;
        public Vector3[] points = new Vector3[4];
        public Texture2D texture;
        public VertexPositionTexture[] vertexes = new VertexPositionTexture[6];

        public VertexRectangle(Vector3 v0, Vector3 v1, Texture2D rectangleTexture)
        {
            this.texture = rectangleTexture;
            int i = 0;
            if (v0.X == v1.X)
            {
                points[0] = v0;
                points[1] = new Vector3(v0.X, v0.Y, v1.Z);
                points[2] = v1;
                points[3] = new Vector3(v0.X, v1.Y, v0.Z);
                i++;
                type = 0;
            }
            if (v0.Y == v1.Y)
            {
                points[0] = v0;
                points[1] = new Vector3(v0.X, v0.Y, v1.Z);
                points[2] = v1;
                points[3] = new Vector3(v1.X, v0.Y, v0.Z);
                i++;
                type = 1;
            }
            if (v0.Z == v1.Z)
            {
                points[0] = v0;
                points[1] = new Vector3(v1.X, v0.Y, v0.Z);
                points[2] = v1;
                points[3] = new Vector3(v0.X, v1.Y, v0.Z);
                i++;
                type = 2;
            }
            if (i != 1)
            {
                throw new System.Exception();
            }
            vertexes[0].Position = points[2];
            vertexes[0].TextureCoordinate = new Vector2(1, 0);
            vertexes[1].Position = points[1];
            vertexes[1].TextureCoordinate = new Vector2(0, 0);
            vertexes[2].Position = points[0];
            vertexes[2].TextureCoordinate = new Vector2(0, 1);
            vertexes[3].Position = points[2];
            vertexes[3].TextureCoordinate = new Vector2(1, 0);
            vertexes[4].Position = points[0];
            vertexes[4].TextureCoordinate = new Vector2(0, 1);
            vertexes[5].Position = points[3];
            vertexes[5].TextureCoordinate = new Vector2(1, 1);
        }
        public static bool PointBelong(Vector3 p, VertexRectangle rec)
        {
            if (rec.points[0].X == p.X && rec.points[2].X == p.X)
            {
                if ((rec.points[0].Y - p.Y) * (rec.points[2].Y - p.Y) <= 0 && (rec.points[0].Z - p.Z) * (rec.points[2].Z - p.Z) <= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            if (rec.points[0].Y == p.Y && rec.points[2].Y == p.Y)
            {
                if ((rec.points[0].X - p.X) * (rec.points[2].X - p.X) <= 0 && (rec.points[0].Z - p.Z) * (rec.points[2].Z - p.Z) <= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            if (rec.points[0].Z == p.Z && rec.points[2].Z == p.Z)
            {
                if ((rec.points[0].Y - p.Y) * (rec.points[2].Y - p.Y) <= 0 && (rec.points[0].X - p.X) * (rec.points[2].X - p.X) <= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        public static VertexRectangle[] Absorb(VertexRectangle rec1, VertexRectangle rec2)
        {
            List<VertexRectangle> rec = new List<VertexRectangle> { rec1, rec2 };
            if (rec1.type != rec2.type)
            {
                return rec.ToArray();
            }
            if (PointBelong(rec2.points[0], rec1) && PointBelong(rec2.points[2], rec1))
            {
                rec.Remove(rec2);
            }
            if (PointBelong(rec1.points[0], rec2) && PointBelong(rec1.points[2], rec2))
            {
                rec.Remove(rec1);
            }
            return rec.ToArray();
        }
        public void Draw(BasicEffect effect, GraphicsDeviceManager graphics)
        {
            effect.Texture = texture;
            foreach (var pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, vertexes, 0, 2);
            }
        }
    }
}