using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace ComponentPack
{
    public static class Extentions
    {

        static public float ModulasClamp(float value, float min, float max)
        {
            float ret;
            if (value >= max)
            {
                ret = min + value % (max);
                return ret;
            }
            else if (value < min)
            {
                ret = (max) - Math.Abs(value % (max));
                return ret;
            }
            return value;
        }


        public enum DrawLayers
        {
            background,
            ground,
            foreground,
            maxLayer 
        }

        public static double TotalSeconds;
    }
}
