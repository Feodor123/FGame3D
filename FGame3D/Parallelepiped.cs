using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FGame3D
{
    class Parallelepiped
    {
        public Vector3[] points = new Vector3[8];
        public VertexRectangle[] vertexRectangles = new VertexRectangle[6];
        public Parallelepiped(Vector3 v0, Vector3 v1, Texture2D[] textures)
        {
            points[0] = v0;
            points[1] = new Vector3(v0.X, v1.Y, v0.Z);
            points[2] = new Vector3(v1.X, v1.Y, v0.Z);
            points[3] = new Vector3(v1.X, v0.Y, v0.Z);
            points[4] = new Vector3(v0.X, v0.Y, v1.Z);
            points[5] = new Vector3(v0.X, v1.Y, v1.Z);
            points[6] = v1;
            points[7] = new Vector3(v1.X, v0.Y, v1.Z);
            GetVertexRectangles(textures);
        }
        public Parallelepiped(Vector3 v, float x, float y, float z, Texture2D[] textures) : this(v, new Vector3(x, y, z) + v, textures) { }
        public Parallelepiped(Vector3 v0, Vector3 v1, Texture2D texture) : this(v0, v1, new Texture2D[] { texture, texture, texture, texture, texture, texture }) { }
        public Parallelepiped(Vector3 v, float x, float y, float z, Texture2D texture) : this(v, x, y, z, new Texture2D[] { texture, texture, texture, texture, texture, texture }) { }
        public virtual void GetVertexRectangles(Texture2D[] textures)
        {
            vertexRectangles[0] = new VertexRectangle(points[3], points[1], textures[0]);
            vertexRectangles[1] = new VertexRectangle(points[0], points[5], textures[1]);
            vertexRectangles[2] = new VertexRectangle(points[1], points[6], textures[2]);
            vertexRectangles[3] = new VertexRectangle(points[2], points[7], textures[3]);
            vertexRectangles[4] = new VertexRectangle(points[3], points[4], textures[4]);
            vertexRectangles[5] = new VertexRectangle(points[4], points[6], textures[5]);
        }
        public void GetVertexRectangles(Texture2D texture)
        {
            GetVertexRectangles(new Texture2D[] { texture, texture, texture, texture, texture, texture });
        }
    }
}
