using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace ComponentPack
{
    public static class SoundManager
    {

        static List<SoundComponent> soundComponents = new List<SoundComponent>();

        public static void AddComponent(SoundComponent newSoundComponent)
        {
            soundComponents.Add(newSoundComponent);
        }

        public static void RemoveComponent(SoundComponent soundComponent)
        {
            soundComponents.Remove(soundComponent);
        }

        public static void SetMasterVolume(float volume)
        {
            foreach (var soundComponent in soundComponents)
            {
                soundComponent.Volume = volume;
            }
        }

    }
}