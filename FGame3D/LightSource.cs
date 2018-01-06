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
    class LightSource
    {       
        public Vec3Int position;
        public int lightPower;
        public LightSource(Vec3Int position, int lightPower)
        {
            this.position = position;
            this.lightPower = lightPower;
        }
    }
}
