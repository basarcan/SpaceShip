using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Text;

namespace ship
{
    class Background
    {
        public Texture2D texture;
        public Vector2 spacePos1, spacePos2;
        public int speed;
        public Vector2 position;
        

        public Background()
        {
            texture = null;
            spacePos1 = new Vector2(0, 0);
            spacePos2 = new Vector2(0, -700);
            speed = 5;
        }
        public void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("background");
            position = new Vector2(0, 0);
            
           
        }

        public void Update(GameTime gameTime)
        {
            spacePos1.Y += speed;
            spacePos2.Y += speed;
            


            if (spacePos1.Y >= 700)
            {
                spacePos1.Y = 0;
                spacePos2.Y = -700;
            }
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, spacePos1, Color.White);
            spriteBatch.Draw(texture, spacePos2, Color.White);
        }       

    }
}