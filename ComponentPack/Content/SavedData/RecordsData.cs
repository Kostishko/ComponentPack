using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ComponentPack;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System;

namespace ComponentPack
{
    internal class RecordsData
    {

        public List<ScoreRecord> ScoreRecords;

    }

    internal struct ScoreRecord
    {
        public int Score;
        public string Name;
        public int Level;
    }
}