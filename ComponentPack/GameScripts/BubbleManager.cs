using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using ComponentPack.GameScripts;


namespace ComponentPack
{
    public class BubbleManager
    {
        private MainCharacter mainChar;

        public List<Bubble> bubbles;
        

        public Random rng;

        public float LastY;

        public BubbleManager(MainCharacter character)
        {
            rng = new Random();

            bubbles = new List<Bubble>();

            mainChar = character;

            LastY = mainChar.Transform.Position.Y + 500;

            for (int i = 0; i < 15; i++)
            {
                bubbles.Add(new SpeechBubble(new Vector2(0, 5000), 0, Game1.CommonContent));
            }


            for (int i = 0; i < 5; i++)
            {
                bubbles.Add(new AngryBubble(new Vector2(0, 5000), 0, Game1.CommonContent));
            }

            //for(int i =0; i<bubbles.Count;i++)
            //{
            //    bubbles[i].Exploded += (s, e) =>
            //    {                    
            //    };

            //}

            for (int i = 0; i < 10; )
            {
                var bubble = bubbles[rng.Next(bubbles.Count - 1)];
                if (bubble.IsActive != true)
                {
                    bubble.IsActive = true;
                    bubble.Transform.Position = new Vector2(rng.Next((int)mainChar.Transform.Position.X - 400,
                   (int)mainChar.Transform.Position.X + 400),
                   LastY);
                    LastY -= 150;
                    i++;
                }
        
           }

        }

        public void Update()
        {
            for(int i =0; i<bubbles.Count;i++)
            {
                if (bubbles[i].IsActive == true)
                {
                    bubbles[i].UpdateMe();
                    if(bubbles[i].Transform.Position.Y > mainChar.Transform.Position.Y + 500)
                    {
                        bubbles[i].ExploudMe();
                    }
                }
            }

            if(ActiveBubblesCounter()<=10)
            {
                
                while(ActiveBubblesCounter() < 10)
                {
                    var bubble = bubbles[rng.Next(bubbles.Count - 1)];
                    if (bubble.IsActive != true)
                    {
                        bubble.IsActive = true;
                        bubble.BubbleAnimController.Play("Idle");
                        bubble.Transform.Position = new Vector2(rng.Next((int)mainChar.Transform.Position.X - 400,
                       (int)mainChar.Transform.Position.X + 400),
                       LastY);
                        LastY -= 150;                        
                    }
                }
            }


        }

        public void DrawMe(SpriteBatch sp)
        {
            for (int i = 0; i < bubbles.Count; i++)
            {
                if (bubbles[i].IsActive == true)
                {
                    bubbles[i].DrawMe(sp);
                }
            }
        }


        public int ActiveBubblesCounter()
        {
            int count = 0;

            for (int i = 0; i < bubbles.Count; i++)
            {
                if (bubbles[i].IsActive == true)
                {
                    count++;
                }
            }
            return count;
        }
    }
}
