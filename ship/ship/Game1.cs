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

namespace ship
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        //States
        public enum State
        { 
            Menu,
            Playing,
            Gameover            
        }
        //set states(first state to set)
        State gameState = State.Menu;
        public Texture2D menuImage;
        public Texture2D gameOverImage;
        public Texture2D scoreImage;


        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public int Delay;

        Player ship = new Player();
        Background space = new Background();
        HUD hud = new HUD();
        Explosion explosion = new Explosion();
        SoundManager sm = new SoundManager();
        
        

        List<Asteroids> asteroids = new List<Asteroids>();
        List<AsteroidLeft> asteroidList = new List<AsteroidLeft>();
        List<AsteroidRight> asteroid2List = new List<AsteroidRight>();
        






        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 400;
            graphics.PreferredBackBufferHeight = 700;
            Delay = 25;

            
        }


        protected override void Initialize()
        {


            base.Initialize();
        }


        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(GraphicsDevice);

            ship.LoadContent(Content);
            space.LoadContent(Content);
            hud.LoadContent(Content);
            explosion.LoadContent(Content);
            menuImage = Content.Load<Texture2D>("menuImage");
            gameOverImage = Content.Load<Texture2D>("gameOver");
            scoreImage = Content.Load<Texture2D>("scoreBoard");
            sm.LoadContent(Content);
            
            
        }


        protected override void UnloadContent()
        {

        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            //UPDATING PLAYING STATE
            switch (gameState)
            {
                //UPDATING MENU STATE
                case State.Menu:
                    {
                        //Get keyboard state
                        KeyboardState ks = Keyboard.GetState();
                        if (ks.IsKeyDown(Keys.Enter))
                        {
                            gameState = State.Playing;
                        }
                        space.Update(gameTime);
                        break;
                    }

                    case State.Playing:
                    {
                        space.Update(gameTime);
                        ship.Update(gameTime);
                        hud.Update(gameTime);
                        explosion.Update(gameTime);


                        foreach (AsteroidLeft asteroid in asteroidList)
                        {
                            //Check enemy bullet with player ship
                            for (int i = 0; i < asteroid.asteroidList.Count; i++)
                            {
                                if (ship.trickRec.Intersects(asteroid.asteroidList[i].boundingBox) ||
                                    ship.trickRec2.Intersects(asteroid.asteroidList[i].boundingBox) ||
                                    ship.trickRec3.Intersects(asteroid.asteroidList[i].boundingBox) ||
                                    ship.trickRec4.Intersects(asteroid.asteroidList[i].boundingBox) ||
                                    ship.trickRec5.Intersects(asteroid.asteroidList[i].boundingBox))
                                {
                                    ship.isVisible = false;
                                    explosion.isVisible = true;                                  
                                }


                                if (ship.ScoretrickRec.Intersects(asteroid.asteroidList[i].boundingBox))
                                {
                                    hud.counter += 1;
                                    if (hud.counter == 149)
                                    {
                                        hud.counter = 0;
                                    }
                                    hud.openerMusic = true;
                                }
                            }
                            asteroid.Update(gameTime);
                        }
                        foreach (AsteroidRight asteroid2 in asteroid2List)
                        {
                            //Check enemy bullet with player ship
                            for (int i = 0; i < asteroid2.asteroidList.Count; i++)
                            {
                                if (ship.trickRec.Intersects(asteroid2.asteroidList[i].boundingBox) ||
                                    ship.trickRec2.Intersects(asteroid2.asteroidList[i].boundingBox) ||
                                    ship.trickRec3.Intersects(asteroid2.asteroidList[i].boundingBox) ||
                                    ship.trickRec4.Intersects(asteroid2.asteroidList[i].boundingBox) ||
                                    ship.trickRec5.Intersects(asteroid2.asteroidList[i].boundingBox))
                                {
                                    ship.isVisible = false;
                                    explosion.isVisible = true;
                                }
                            }
                            asteroid2.Update(gameTime);
                        }

                        
                        if (!ship.isVisible)
                        {
                            explosion.isVisible = true;
                            // sürekli yeni asteroid göndermesin diye update metodunu kapatarak hem pozisyonunu deðiþtirmiyor ve durmuþ gibi efekt veriyor
                            foreach (AsteroidLeft asteroid in asteroidList)
                            {
                                asteroid.updateBool = false;
                            }

                            foreach (AsteroidRight asteroid2 in asteroid2List)
                            {
                                asteroid2.updateBool = false;
                            }
                            ship.ScoretrickPos = new Vector2(401, 0);// sayaç için konulan rectangle ý kesiþmicek þekile dýþarý gönderiyor.
                            space.speed = 0; //arka planý durduruyor
                            
                            explosion.keyboardBool = false;
                            hud.openerMusic = false;
                           

                            Delay--;
                            
                            if (Delay == 0)
                            {
                                explosion.drawVisible = false;
                                gameState = State.Gameover;
                                Delay = 25;                                
                            }
                            explosion.explosionMusic = true;
                            hud.shipSound = false;
                        }

                        LoadAsteroids();
                        LoadAsteroids2();
                        break;
                    }
                    
                    //UPDATING GAMEOVER STATE
                case State.Gameover:
                    {

                        break;
                        
                    }
            }

             

            


            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            switch (gameState)
            {
                case State.Menu:
                    {
                        space.Draw(spriteBatch);
                        spriteBatch.Draw(menuImage, new Vector2(0, 0), Color.White);
                        break;
                    }

                case State.Playing:
                    {
                        space.Draw(spriteBatch);
                        
                        ship.Draw(spriteBatch);

                        foreach (AsteroidLeft asteroid in asteroidList)
                        {
                            asteroid.Draw(spriteBatch);
                        }
                        foreach (AsteroidRight asteroid2 in asteroid2List)
                        {
                            asteroid2.Draw(spriteBatch);
                        }

                        explosion.Draw(spriteBatch);
                        hud.Draw(spriteBatch);
                        break;
                    }
                
                case State.Gameover:
                    {
                        
                        space.Draw(spriteBatch);
                        ship.Draw(spriteBatch);

                        foreach (AsteroidLeft asteroid in asteroidList)
                        {
                            asteroid.Draw(spriteBatch);
                        }
                        foreach (AsteroidRight asteroid2 in asteroid2List)
                        {
                            asteroid2.Draw(spriteBatch);
                        }

                        explosion.Draw(spriteBatch);
                        hud.Draw(spriteBatch);

                        spriteBatch.Draw(gameOverImage, new Vector2(0, 0), Color.White);
                        break;
                    }
            }

            

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void LoadAsteroids()
        {
            if (asteroidList.Count() < 1)
            {
                asteroidList.Add(new AsteroidLeft(Content.Load<Texture2D>("astroid12")));
            }
        }
        public void LoadAsteroids2()
        {
            if (asteroid2List.Count() < 1)
            {
                asteroid2List.Add(new AsteroidRight(Content.Load<Texture2D>("astroid12")));
            }
        }


    }
}