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
    class Explosion
    {
        public KeyboardState ks;
        SoundManager sm = new SoundManager();

        public Texture2D texture;
        public Vector2 position;
        public Rectangle rectangle;
        public bool isVisible, drawVisible, keyboardBool;
        public int Frame;

        public float timer;
        public float interval;
        public int currentFrame;

        public Vector2 spriteVelocity;
        public const float tangentialVelocity = 2.5f;
        public float friction = 0.05f;

        public bool explosionMusic;
        public int explosionDelay;


        public Explosion()
        { 
            texture = null;
            isVisible = false;
            keyboardBool = true;
            drawVisible = true;
            timer = 0f;
            interval = 20f;
            currentFrame = 0;
            position = new Vector2(170, 465);

            explosionMusic = false;
            explosionDelay = 20;
            Frame = 0;

        }


        public void LoadContent(ContentManager Content)
        { 
            texture = Content.Load<Texture2D>("explode");
            sm.LoadContent(Content);

        }

        public void Update(GameTime gameTime)
        {
            ks = Keyboard.GetState();




            if (explosionMusic)
            {

                explosionDelay--;
                if (explosionDelay <= 0)
                {
                    sm.explosion.Play();

                }
                if (explosionDelay == 0)
                {
                    explosionDelay = 20;
                    explosionMusic = false;
                }

            }

           
            if (isVisible)
            {
                Frame = (int)(gameTime.TotalGameTime.TotalSeconds * 15) % 9;

                if (Frame == 8)
                {
                    isVisible = false;
                }
            }
            

            rectangle = new Rectangle(Frame * 60, 0, 60, 60);



            if (keyboardBool)
            {
                if (ks.IsKeyDown(Keys.A))
                {
                    position.X -= 1;

                }
                else if (ks.IsKeyDown(Keys.D))
                {
                    position.X += 1;
                }




                position = spriteVelocity + position;

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
            }

            ////Bounds
            //if (position.X <= 0) position.X = 0;
            //if (position.X >= 475 - texture.Width) position.X = 475 - texture.Width;
            //if (position.Y <= 0) position.Y = 0;
            //if (position.Y >= 720 - texture.Height) position.Y = 720 - texture.Height;



        }


        

        public void Draw(SpriteBatch spriteBatch)
        {
            if (drawVisible)
            {
                if (isVisible)
                {
                    spriteBatch.Draw(texture, position, rectangle, Color.White);
                }
            }
        }
    }
}