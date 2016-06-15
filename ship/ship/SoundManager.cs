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
    public class SoundManager
    {
        public SoundEffect shipSoundeffect;
        public Song bgMusic;
        public SoundEffect scoreSound;
        public SoundEffect movementSound;
        public SoundEffect explosion;

        public SoundManager()
        {
            shipSoundeffect = null;
            bgMusic = null;
            scoreSound = null;
            explosion = null;
        }

        public void LoadContent(ContentManager Content)
        {
            shipSoundeffect = Content.Load<SoundEffect>("shipsound");
            bgMusic = Content.Load<Song>("shipsounds");
            scoreSound = Content.Load<SoundEffect>("pointNum");
            movementSound = Content.Load<SoundEffect>("movements");
            explosion = Content.Load<SoundEffect>("explode1");
        }
    }
}