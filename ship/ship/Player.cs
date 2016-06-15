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
    class Player
    {

        KeyboardState ks;
        public Rectangle rectangle;
        public Texture2D rightTex, leftTex, midTex, currentTex;


        public Texture2D trickTex;
        public Rectangle trickRec, trickRec2, trickRec3, trickRec4, trickRec5;
        public Vector2 trickPos, trickPos2, trickPos3, trickPos4, trickPos5;

        //scoreTrick
        public Texture2D ScoretrickTex;
        public Rectangle ScoretrickRec;
        public Vector2 ScoretrickPos;


        public Vector2 position;
        public bool isVisible;
        int frame;

        Vector2 spriteVelocity;
        const float tangentialVelocity = 2.5f;
        float friction = 0.05f;

        SoundManager sm = new SoundManager();
         

        public Player()
        {            
            rightTex = null;
            leftTex = null;
            midTex = null;
            currentTex = null;
            isVisible = true;
            frame = 0;
        }
        public void Initialize()
        {


        }

        public void LoadContent(ContentManager Content)
        {
            leftTex = Content.Load<Texture2D>("leftAnimation");
            rightTex = Content.Load<Texture2D>("rightAnimation");
            midTex = Content.Load<Texture2D>("middleAnimation");
            trickTex = Content.Load<Texture2D>("trick1");
            ScoretrickTex = Content.Load<Texture2D>("ScoreTrick");

            currentTex = midTex;
            position = new Vector2(180 , 475);
            trickPos = new Vector2(195, 475);
            trickPos2 = new Vector2(185, 486);
            trickPos3 = new Vector2(204, 486);
            trickPos4 = new Vector2(180, 500);
            trickPos5 = new Vector2(214, 500);

            ScoretrickPos = new Vector2(0, 475);
            sm.LoadContent(Content);

        }

        public void Update(GameTime gameTime)
        {
            frame = (int)(gameTime.TotalGameTime.TotalSeconds * 15) % 3;
            rectangle = new Rectangle(40 * frame, 0, 40, 50);
            trickRec = new Rectangle((int)trickPos.X, (int)trickPos.Y, 9, 31);
            trickRec2 = new Rectangle((int)trickPos2.X, (int)trickPos2.Y, 10, 19);////
            trickRec3 = new Rectangle((int)trickPos3.X, (int)trickPos3.Y, 10, 19);////
            trickRec4 = new Rectangle((int)trickPos4.X, (int)trickPos4.Y, 5, 7);////
            trickRec5 = new Rectangle((int)trickPos5.X, (int)trickPos5.Y, 5, 7);////

            ScoretrickRec = new Rectangle((int)ScoretrickPos.X, (int)ScoretrickPos.Y, 400, 1);
            ks = Keyboard.GetState();        

            //Bounds
            if (position.X <= 0) position.X = 0;
            if (position.X >= 475 - currentTex.Width) position.X = 475 - currentTex.Width;

            //TrickBounds
            if (trickPos.X <= 15) trickPos.X = 15;
            if (trickPos.X >= 370) trickPos.X = 370;

            //Trick2Bounds
            if (trickPos2.X <= 5) trickPos2.X = 5;
            if (trickPos2.X >= 360) trickPos2.X = 360;

            //Trick3Bounds
            if (trickPos3.X <= 25) trickPos3.X = 25;
            if (trickPos3.X >= 378) trickPos3.X = 378;

            //Trick4Bounds
            if (trickPos4.X <= 0) trickPos4.X = 0;
            if (trickPos4.X >= 355) trickPos4.X = 355;

            //Trick5Bounds
            if (trickPos5.X <= 35) trickPos5.X = 35;
            if (trickPos5.X >= 390) trickPos5.X = 390;


            
            if (ks.IsKeyDown(Keys.A))
            {
                position.X -= 1;
                currentTex = leftTex;
                trickPos.X -= 1;
                trickPos2.X -= 1;
                trickPos3.X -= 1;
                trickPos4.X -= 1;
                trickPos5.X -= 1;
               
        
            }
            else if (ks.IsKeyDown(Keys.D))
            {
                position.X += 1;
                currentTex = rightTex;
                trickPos.X += 1;
                trickPos2.X += 1;
                trickPos3.X += 1;
                trickPos4.X += 1;
                trickPos5.X += 1;
                
            }
            else
            {
                currentTex = midTex;
            }

            ///// YAVAŞLATMA EFECT İ ///////////////////////////////////////////////////////////////
            //
            position = spriteVelocity + position;                                                 //
            trickPos = spriteVelocity + trickPos;
            trickPos2 = spriteVelocity + trickPos2;
            trickPos3 = spriteVelocity + trickPos3;
            trickPos4 = spriteVelocity + trickPos4;
            trickPos5 = spriteVelocity + trickPos5;


            if (ks.IsKeyDown(Keys.A))
            {
                spriteVelocity.X -= tangentialVelocity / 5;
            }
            else if (spriteVelocity != Vector2.Zero)
            {
                float i = spriteVelocity.X;
                spriteVelocity.X = i -= friction * i;

            }
            if (ks.IsKeyDown(Keys.D))
            {
                spriteVelocity.X += tangentialVelocity / 5;
            }
            else if (spriteVelocity != Vector2.Zero)
            {
                float j = spriteVelocity.X;
                spriteVelocity.X = j -= friction * j;

            }
                                                                //
            ///////////////////////////////////////////////////////////////////////////////



        }
        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(ScoretrickTex, ScoretrickRec, Color.White);
            //spriteBatch.Draw(trickTex, trickRec, Color.White);
            //spriteBatch.Draw(trickTex, trickRec2, Color.White);
            //spriteBatch.Draw(trickTex, trickRec3, Color.White);
            //spriteBatch.Draw(trickTex, trickRec4, Color.White);
            //spriteBatch.Draw(trickTex, trickRec5, Color.White);
            

            if (isVisible)
            {
                spriteBatch.Draw(currentTex, position, rectangle, Color.White);
            }                    

        }
    }
}