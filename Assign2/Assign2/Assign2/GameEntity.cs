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

namespace Assign2
{
   
    public abstract class GameEntity
    {
        public Vector2 Position;//initialize position of type vector2
   
        public Vector2 Look;//initialize look of type vector2
        private Texture2D sprite;//initialize sprite of type texture2d
        public Texture2D Sprite//get set sprite
        {
            get { return sprite; }
            set { sprite = value; }
        }


        public abstract void LoadContent();//abstract void load content
        public abstract void Update(GameTime gameTime);//abstract void update
        public abstract void Draw(GameTime gameTime);//abstract void draw
    }
}
