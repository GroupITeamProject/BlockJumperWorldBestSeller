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
    public class Player : GameEntity
    {
        public override void LoadContent()//override of abstract void loadcontent in GameEntity
        {

            Position = new Vector2(100, 340);//position is equal to (100,340)

            Look = new Vector2(0, -1);//look is equal to (0,-1) 

            Sprite = Game1.Instance.Content.Load<Texture2D>("man");//load image man.png
        }
        public override void Update(GameTime gameTime)//override of abstract void update in GameEntity
        {

            KeyboardState kState = Keyboard.GetState();

            float timeDelta = (float)gameTime.ElapsedGameTime.TotalSeconds;//timedelta is equal to one second of the game
            float speed = 100.0f;//speed initilized as 100
            float speed2 = 400.0f;//speed initilized as 400
            if (kState.IsKeyDown(Keys.Space))//if space bar is pressed
            {
                Position += speed2 * timeDelta * Look;//the player goes up
                if (Position.Y < 230)//if position.y is less than 230 
                {
                    Position -= speed2 * timeDelta * Look;//player stops

                }
            }

            else if (kState.IsKeyDown(Keys.Up))//if up button pressed
            {

                Position += speed * timeDelta * Look; //the player goes up
                if (Position.Y < 300)//if position is less than 230
                {
                    Position -= speed * timeDelta * Look;//player stops

                }


            }
            else if (Position.Y <= 340)//if position is lees than 340
            {
                Position -= speed * timeDelta * Look;//player goes down

            }


        }
        public override void Draw(GameTime gameTime)//override of abstract void draw in GameEntity        
        {

            Game1.Instance.spriteBatch.Draw(Sprite, Position, null, Color.White); //draw with parameters sprite, position and color.white
        }
    }
}
