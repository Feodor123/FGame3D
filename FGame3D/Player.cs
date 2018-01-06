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
    class Player
    {
        public const int visibleDistance = 500;
        public float gravitationConstant = 0.01f;
        private float jumpSpeed = 0.5f;
        public Vector3 position;
        public int X
        {
            get
            {
                return (int)position.X;
            }
        }
        public int Y
        {
            get
            {
                return (int)position.Y;
            }
        }
        public int Z
        {
            get
            {
                return (int)position.Z;
            }
        }
        public Vector3 sight;
        private Vector3 speed;
        private float resistance = 0.8f;
        public float gorisontalAngle;//degrees
        public float verticalAngle;//degrees
        public const float rotateSpeed = 1;
        public const float movementSpeed = 0.01f;
        public Player(Vector3 position, float gorisontalAngle, float verticalAngle)
        {
            speed = new Vector3(0, 0, 0);
            this.position = position;
            this.gorisontalAngle = gorisontalAngle;
            this.verticalAngle = verticalAngle;
            UpdateSight();
        }
        public void UpdateSight()
        {
            sight = new Vector3();
            sight.Z = 1000 * (float)Math.Sin(verticalAngle * Math.PI / 180);
            float l = 1000 * (float)Math.Cos(verticalAngle * Math.PI / 180);
            sight.X = l * (float)Math.Cos(gorisontalAngle * Math.PI / 180);
            sight.Y = l * (float)Math.Sin(gorisontalAngle * Math.PI / 180);
            sight.Normalize();
        }
        public void Update(KeyboardState keyboardState, GameMap map,Game1 game)
        {
            int centerX = game.GraphicsDevice.Viewport.Width / 2;
            int centerY = game.GraphicsDevice.Viewport.Height / 2;
            MouseState mouse = Mouse.GetState();
            Mouse.SetPosition(centerX, centerY);
            gorisontalAngle -= (mouse.X - centerX) * rotateSpeed;
            verticalAngle -= (mouse.Y - centerY) * rotateSpeed;
            Vector2 v1 = new Vector2(sight.X, sight.Y);
            v1.Normalize();
            Vector2 v2 = new Vector2(-v1.Y, v1.X);
            v2.Normalize();
            Vector2 gorisontalAcceleration = new Vector2(0, 0);
            if (keyboardState.GetPressedKeys().Contains(Keys.Space) && speed.Z == 0)
            {
                speed.Z += jumpSpeed;
            }
            if (keyboardState.IsKeyDown(Keys.W))
            {
                gorisontalAcceleration += v1;
            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                gorisontalAcceleration -= v1;
            }
            if (keyboardState.IsKeyDown(Keys.A))
            {
                gorisontalAcceleration += v2;
            }
            if (keyboardState.IsKeyDown(Keys.D))
            {
                gorisontalAcceleration -= v2;
            }
            gorisontalAcceleration *= movementSpeed;
            speed = new Vector3(speed.X + gorisontalAcceleration.X, speed.Y + gorisontalAcceleration.Y, speed.Z - gravitationConstant) * resistance;
            if (getIntersect(new Vector3(position.X + speed.X, position.Y, position.Z), map).Length != 0)
            {
                speed.X = 0;
            }
            if (getIntersect(new Vector3(position.X, position.Y + speed.Y, position.Z), map).Length != 0)
            {
                speed.Y = 0;
            }
            if (getIntersect(new Vector3(position.X, position.Y, position.Z + speed.Z), map).Length != 0)
            {
                speed.Z = 0;
            }
            if (getIntersect(new Vector3(position.X, position.Y + speed.Y, position.Z + speed.Z), map).Length != 0)
            {
                speed.Y = 0;
                speed.Z = 0;
            }
            if (getIntersect(new Vector3(position.X + speed.X, position.Y + speed.Y, position.Z), map).Length != 0)
            {
                speed.Y = 0;
                speed.X = 0;
            }
            if (getIntersect(new Vector3(position.X + speed.X, position.Y, position.Z + speed.Z), map).Length != 0)
            {
                speed.X = 0;
                speed.Z = 0;
            }
            if (getIntersect(position + speed, map).Length != 0)
            {
                speed = new Vector3(0, 0, 0);
            }
            position += speed;
            verticalAngle = MathHelper.Min(verticalAngle, 89);
            verticalAngle = MathHelper.Max(verticalAngle, -89);
            UpdateSight();
        }
        public Block[] getIntersect(Vector3 pos, GameMap map)
        {
            List<Block> b = new List<Block>();
            int x0 = (int)(pos.X - 0.5f);
            int y0 = (int)(pos.Y - 0.5f);
            int z0 = (int)(pos.Z - 1.5f);
            int x1 = (int)(pos.X + 0.5f);
            int y1 = (int)(pos.Y + 0.5f);
            int z1 = (int)(pos.Z + 0.5f);
            for (int x = x0; x <= x1; x++)
            {
                for (int y = y0; y <= y1; y++)
                {
                    for (int z = z0; z <= z1; z++)
                    {
                        if (map[x, y, z] != null && map[x, y, z].blockType.isObstacle)
                        {
                            b.Add(map[x, y, z]);
                        }
                    }
                }
            }
            return b.ToArray();
        }
    }
}
