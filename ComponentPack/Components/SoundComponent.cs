using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;



namespace ComponentPack
{
    public class SoundComponent : IComponent
    {

        //parent
        private GameObject parent;
        public GameObject Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        //sound volume
        private float volume;
        public float Volume
        {
            get => volume;
            set
            {
                volume = Math.Clamp(value, 0, 1);
                if (currentSoundInstance != null)
                    currentSoundInstance.Volume = volume;
            }
        }

        //Sound Dictionary
        private Dictionary<string, SoundEffect> soundDictionary;

        //Current sound effect instance for once playable sounds
        private SoundEffectInstance currentSoundInstance;




        public SoundComponent(GameObject parent, Dictionary<string, SoundEffect> soundDictionary) 
        {
            Parent = parent;
            this.soundDictionary = soundDictionary;
            SoundManager.AddComponent(this);
        }


        /// <summary>
        /// Sound will play only if the previous sound hsa played already 
        /// </summary>
        /// <param name="soundName"></param>
        public void Play(string soundName)
        {
            if (currentSoundInstance == null)
            {
                currentSoundInstance = soundDictionary[soundName].CreateInstance();
                currentSoundInstance.IsLooped = true;
                currentSoundInstance.Play();
                currentSoundInstance.Volume = volume;
            }
            else
            {
                currentSoundInstance.Play();
            }
        }

        public void Stop()
        {
            if (currentSoundInstance != null)
            {
                currentSoundInstance.Stop();

            }
        }

        /// <summary>
        /// Will play ney instance every call
        /// </summary>
        /// <param name="soundName"></param>
        public void PlayNow(string soundName)
        {
            SoundEffectInstance instance = soundDictionary[soundName].CreateInstance();
            instance.Volume = volume;
            instance.IsLooped = false;
            instance.Play();
        }

        public void DeleteMe()
        {   
            SoundManager.RemoveComponent(this);
        }

        public void UpdateMe()
        {

        }

        public void DrawMe(SpriteBatch spriteBatch)
        {

        }

        public GameObject GetParent()
        {
            return Parent;
        }

    }
}