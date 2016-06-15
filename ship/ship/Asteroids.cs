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
    public class Asteroids
    {
        public Rectangle boundingBox;
        public Texture2D texture;
        public Vector2 position;
        public bool isVisible;
        public float speed;

        Player ship = new Player();
        HUD hud = new HUD();

        public Asteroids(Texture2D newTexture)
        {
            
            texture = newTexture;
            isVisible = true;
            speed = 3;

        }


        public void Update(GameTime gameTime)
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {            
            spriteBatch.Draw(texture, position, Color.White);
        }

    }
}