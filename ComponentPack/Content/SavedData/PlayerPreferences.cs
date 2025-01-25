using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ComponentPack;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System;


namespace ComponentPack
{
    /// <summary>
    /// settings
    /// </summary>
    internal class PlayerPreferences
    {
        //music and sound
        private float musicVolume;
        public float MusicVolume { get => musicVolume; set => musicVolume = Math.Clamp(value, 0, 1); }

        private float soundVolume;
        public float SoundVolume { get => soundVolume; set => soundVolume = Math.Clamp(value, 0, 1); }

    }
}