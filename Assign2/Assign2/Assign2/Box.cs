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

    public class Box : GameEntity
    {
        private float speed = 200.0f;//speed initilized as 200
        public float Speed//pubic speed with get and set
        {
            get { return speed; }
            set { speed = value; }

        }



        public override void LoadContent()//override of abstract void loadcontent in GameEntity
        {
            int y_pos = 362;//y_pos initalized as 362
            Random rnd = new Random();//initialize new random 
            int rand_no = rnd.Next(1, 4);//random number between 1 and 4
            if ((rand_no == 1))//if random number equals 1 
            {
                y_pos = 300;//y_pos is 300
            }
           
            else if ((rand_no == 2))//if random number equals 2
            {
                y_pos = 429;//y_pos is 429
            }
            rand_no = 0;
            Position = new Vector2(800, y_pos);//position is equal to (800,y_pos)
            Look = new Vector2(-1, 0);//look is equal to (-1,0) 
            Sprite = Game1.Instance.Content.Load<Texture2D>("Block");//load image block.bmp
        }
        public override void Update(GameTime gameTime)//override of abstract void update in GameEntity
        {
            float timeDelta = (float)gameTime.ElapsedGameTime.TotalSeconds;//timedelta is equal to one second of the game

            Position = Position + Look * speed * timeDelta;//position moves to left 


        }
        public override void Draw(GameTime gameTime)//override of abstract void draw in GameEntity
        {


            Game1.Instance.spriteBatch.Draw(Sprite, Position, Color.White); //draw with parameters sprite, position and color.white
        }
    }

}
