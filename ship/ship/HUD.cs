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
    class HUD
    {
        public Texture2D texture;
        public Vector2 oneSplacePos, tenSplacePos, houndredSplacePos, thousandSplacePos;
        public Rectangle oneSplaceRec, tenSplaceRec, houndredSplaceRec, thousandSplaceRec;

        public bool tenIsVisible, houndredIsVisible, thousandIsVisible;

        public int counter;
        public int oneSframe, tenSframe, houndredSframe, thousandSframe;

        public bool openerMusic;
        public int musicDelay;

        public int delay;
        public bool shipSound;

        SoundManager sm = new SoundManager();
        KeyboardState ks;

        public HUD()
        {
            texture = null;
            oneSplacePos = new Vector2(220 , 20);//220,20
            tenSplacePos = new Vector2(200 , 20);
            houndredSplacePos = new Vector2(180,20);
            thousandSplacePos = new Vector2(160,20);

            oneSframe = 0;
            tenSframe = 0;
            houndredSframe = 0;
            thousandSframe = 0;
            tenIsVisible = false;
            houndredIsVisible = false;
            thousandIsVisible = false;

            openerMusic = false;
            shipSound = true;
            musicDelay = 20;
            delay = 10;

 
        }
        public void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("numbers");
            sm.LoadContent(Content);
            
        }

        public void Update(GameTime gameTime)
        {
            ks = Keyboard.GetState();
            oneSplaceRec = new Rectangle(30 * oneSframe, 0, 30, 30);
            tenSplaceRec = new Rectangle(30 * tenSframe, 0, 30, 30);
            houndredSplaceRec = new Rectangle(30 * houndredSframe, 0, 30, 30);
            thousandSplaceRec = new Rectangle(30 * thousandSframe, 0, 30, 30);




            oneSframe = counter / 15;

            if (openerMusic)
            {

                musicDelay--;
                if (musicDelay <= 0)
                {
                    sm.scoreSound.Play();
                    //sm.shipSoundeffect.Play();

                }
                if (musicDelay == 0)
                {
                    musicDelay = 20;
                    openerMusic = false;
                }
               
            }


            if (shipSound)
            {
                delay--;
                if (delay <= 0)
                {
                    sm.shipSoundeffect.Play();
                    //sm.movementSound.Play();
                }
                if (delay == 0)
                {
                    delay = 400;

                }
            }

            
            
            
            

            if (counter == 148)
            {               
                tenSframe += 1;
                tenIsVisible = true;

                if (tenSframe == 10)
                {
                    houndredIsVisible = true;
                    tenSframe = 0;
                    houndredSframe += 1;
                }
                if (houndredSframe == 10)
                {

                    houndredSframe = 0;
                    thousandSframe += 1;
                }

            }
        
            
            
            

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, oneSplacePos, oneSplaceRec, Color.White);
            if (tenIsVisible)
            {
                spriteBatch.Draw(texture, tenSplacePos, tenSplaceRec, Color.White);
            }
            if (houndredIsVisible)
            {
                spriteBatch.Draw(texture, houndredSplacePos, houndredSplaceRec, Color.White);
            }
            if (thousandIsVisible)
            {
                spriteBatch.Draw(texture, thousandSplacePos, thousandSplaceRec, Color.White);
            }
        }

        
    }
}