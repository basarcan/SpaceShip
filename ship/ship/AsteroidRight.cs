using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace ship
{
    public class AsteroidRight
    {
        Random random = new Random();
        public int randX;

        public Rectangle boundingBox;
        public Texture2D  asteroidTex;
        public Vector2 position;
        public int  asteroidDelay;
        public bool isVisible, updateBool;
        public List<Asteroids> asteroidList;

        public AsteroidRight(Texture2D newAsteroidTex)
        {
            asteroidList = new List<Asteroids>();
            asteroidTex = newAsteroidTex;           
            asteroidDelay = 80;
            isVisible = true;
            updateBool = true;
        }

        public void LoadContent(ContentManager Content)
        {

        }

        public void Update(GameTime gameTime)
        {
            SpawnAsteroids();
            if (updateBool)
            {
                UpdateAsteroids();
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {




            foreach (Asteroids asteroid in asteroidList)
            {
                asteroid.Draw(spriteBatch);
            }

        }

        public void SpawnAsteroids()
        {

            int randX = random.Next(0, 160);
            //
            if (asteroidDelay >= 0)
                asteroidDelay--;
            //
            if (asteroidDelay <= 0)
            {
                Asteroids newAsteroid = new Asteroids(asteroidTex);
                newAsteroid.position = new Vector2(253 - randX, -45);

                //making astroid visible
                newAsteroid.isVisible = true;


                asteroidList.Add(newAsteroid);


            }

            //reset asteroid delay
            if (asteroidDelay == 0)
            {
                asteroidDelay = 80;
            }
        }

        public void UpdateAsteroids()
        {
            //for each bullet in our bullet list update the movement if the bullet hits the top of the screen remove it from the list
            foreach (Asteroids asteroid in asteroidList)
            {
                //boundingBox for every bullets
                asteroid.boundingBox = new Rectangle((int)asteroid.position.X, (int)asteroid.position.Y, asteroid.texture.Width, asteroid.texture.Height);

                asteroid.position.Y += asteroid.speed;

                if (asteroid.position.Y <= 700) // if the bullet reachs the bound of the left it will be
                    asteroid.isVisible = false; // dissepear
                randX = random.Next(0, 160);
            }




            //if any of the bullets are not visible, if they aren't then remove that bullet from our bullet list
            for (int i = 0; i < asteroidList.Count; i++)
            {
                if (asteroidList[i].isVisible)
                {
                    asteroidList.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
