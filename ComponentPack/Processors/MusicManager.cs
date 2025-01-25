using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Drawing;



namespace ComponentPack
{
    public class MusicManager
    {
        private Dictionary<string, SoundEffectInstance> music;
        private string currentSong;
        private float volume;
        public float Volume
        {
            get => volume;
            set => volume = Math.Clamp(value, 0f, 1f);
        }


        public MusicManager(Dictionary<string, SoundEffect> songList)
        {
            music = new Dictionary<string, SoundEffectInstance>();

            foreach (var song in songList)
            {
                //fill the dictionary with instances (easier to work with instances)
                music.Add(song.Key, song.Value.CreateInstance());
            }

            //default volume
            Volume = 1f;
            foreach (var song in music)
            {
                song.Value.IsLooped = true;
                song.Value.Volume = Volume;
            }
        }

        public void Play(string name)
        {
            if (currentSong != name && currentSong != null)
            {
                music[currentSong].Stop();
            }
            currentSong = name;
            music[currentSong].Play();
        }

        public void Stop()
        {
            if (currentSong != null)
            {
                music[currentSong].Stop();
            }
        }

        public void Pause()
        {
            if (currentSong != null)
            {
                music[currentSong].Pause();
                currentSong = null;
            }
        }

        public void SetMusicVolume(float newVolume)
        {
            foreach (var song in music)
            {
                song.Value.Volume = newVolume;
            }
        }



    }
}