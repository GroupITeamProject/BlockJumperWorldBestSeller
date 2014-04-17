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
    public enum GameState//gamestates
    {
        Menu,
        Game,
        Exit
    }

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;//initialize spritebatch
        private static Game1 instance;
        private int count_til_box = 0;//count_till_box is equal to 0
        private double score;//double score is Initialized
        private double highscore = 0;//double highscore is Initialized as 0
        SpriteFont title;//sprite font title
        SpriteFont scorefont;//sprite font scorefont
        bool music_play = false;//bool music_play Initialized as false
        SoundEffect soundEffect;//initialize soundEffect of type SoundEfect
        SoundEffectInstance soundEffectInstance;//initialize soundEffectInstance of type SoundEfectIntance

        private List<GameEntity> entities = new List<GameEntity>();//new list of type GameEntity called entities

        public List<GameEntity> Entities
        {
            get { return entities; }
        }

        GameEntity player, box;//player and box of type GameEntity
        GameState gameState = new GameState();//gamestate equals new Gamestate

        public static Game1 Instance//method instance
        {
            get
            {
                return instance;
            }
        }

        public Game1()//constructor of game1
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";//making content useable
            instance = this;
            score = 0.0f;//score equals 0

        }


        protected override void Initialize()
        {
            gameState = GameState.Menu;//gamestate equals gamestate.Menu
            player = new Player();//player equal new player
            entities.Add(player);//add player to entities
            Addbox();//call addbox function
            soundEffect = Content.Load<SoundEffect>("Bass");//load soundeffect Bass.wav
            soundEffectInstance = soundEffect.CreateInstance();//create sound effect instance called soundEffectInstance
            base.Initialize();
        }

        private void Addbox()
        {
            box = new Box();//box equals new
            box.LoadContent();//load box content
            entities.Add(box);//add box to entities
        }

        protected override void LoadContent()
        {


            scorefont = Content.Load<SpriteFont>("font");//font named scorefont created
            title = Content.Load<SpriteFont>("Title");//font named title created
            spriteBatch = new SpriteBatch(GraphicsDevice);

            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].LoadContent();//calls LoadContent from entities[i]
            }

        }


        protected override void UnloadContent()
        {

        }
        bool IsOverLapping(GameEntity boxover, Vector2 pos)//function IsOverLapping of type bool with parameters boxover and pos
        {
            Vector2 boxpos = boxover.Position;//boxpos is equal to boxover.position
            if ((boxpos.X >= pos.X + 26) && (boxpos.X <= pos.X + 54))//if boxpos.x is in between pos.x +26 and pos.x+54
            {
                if ((boxpos.Y >= pos.Y - 11) && (boxpos.Y <= pos.Y + 111))//if boxpos.y is in between pos.y-11 and pos.y+111
                {
                    return true;//return true
                }
            }
            return false;//return false
        }
        void AddBoxAfterCount()//void funtion addboxaftercount
        {
            count_til_box++;//count_til_box incremented by 1
            if (count_til_box == 100)//if count_till_box is 100
            {
                Addbox();//call addbox function
                count_til_box = 0;//reset count_till_box to 0
            }

        }


        protected override void Update(GameTime gameTime)
        {

            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Escape))//if escape key is pressed 
            {
                Exit();//exit program
            }

            if (gameState == GameState.Menu)//if gamestate equals menu
            {
                if (state.IsKeyDown(Keys.Enter))//if enter is pressed
                {
                    gameState = GameState.Game;//gamestate equals game
                }

            }
            else if (gameState == GameState.Game)//else if gamestate equals game
            {

                if (music_play == false)//if music_play is false
                {
                    soundEffectInstance.Play(); //play music
                    music_play = true;//set music_play to true
                }
                AddBoxAfterCount();//call AddBoxAfterCount function


                Vector2 playpos = entities[0].Position;//playpos is equal to first memeber of entities' position 

                for (int i = 1; i < entities.Count; i++)
                {
                    if (IsOverLapping(entities[i], playpos))//if overlapping is true
                    {
                        if (score > highscore)//if score is greater than highscore
                        {
                            highscore = score;//highscore equals score
                        }

                        if (music_play)//if music_play is true
                        {
                            soundEffectInstance.Stop();//stop music
                            music_play = false;//set music_play as false
                        }

                        gameState = GameState.Exit;//gamestate equals exit
                    }
                    entities[i].Update(gameTime);//entities[i] upadate
                }
                entities[0].Update(gameTime);//entities[0] upadate

                score += gameTime.ElapsedGameTime.TotalSeconds;//score is 1 second adding up every time
            }
            if (gameState == GameState.Exit)//if gamestate equals exit
            {

                if (state.IsKeyDown(Keys.Escape))//if esc pressed
                {
                    Exit();//exit game
                }
                else if (state.IsKeyDown(Keys.Enter))
                {

                    score = 0;//set score to zero
                    entities.RemoveRange(1, entities.Count - 1);//reset blocks

                    gameState = GameState.Game;//gamestate equals game

                }
            }
            base.Update(gameTime);

        }


        protected override void Draw(GameTime gameTime)
        {
            if (gameState == GameState.Menu)//if gamestate equals menu
            {

                GraphicsDevice.Clear(Color.AliceBlue);
                spriteBatch.Begin();//begin sprite batch
                spriteBatch.Draw(Game1.Instance.Content.Load<Texture2D>("Man"), new Vector2(350, 50), Color.Black);//man.bmp loaded at position (350,50)
                spriteBatch.DrawString(title, "Block Jumper!!", new Vector2(50, 50), Color.Black);//string "block jumper" written on screen at position (50,50) of font title
                spriteBatch.DrawString(title, "Press Enter to Begin", new Vector2(50, 200), Color.Black);//string "press enter to begin"  written on screen at position (50,200) of font title
                spriteBatch.End();//end sprite batch
            }
            if (gameState == GameState.Game)//if gamestate equals game
            {
                spriteBatch.Begin();

                //FIRE LEVEL
                if (score > 100)//if score greater than 100
                {
                    GraphicsDevice.Clear(Color.Firebrick);
                    spriteBatch.Draw(Game1.Instance.Content.Load<Texture2D>("Fire"), new Vector2(0, 0), Color.White);//draws fire.jpg at(0,0)
                    for (int i = 1; i < entities.Count; i++)
                    {
                        Box temp_box = (Box)entities[i];
                        temp_box.Speed = 1500.0f;//speed equals 1500
                    }
                }
                //FOOTBALL LEVEL
                else if (score > 60)//if score greater than 60
                {
                    GraphicsDevice.Clear(Color.Chartreuse);
                    spriteBatch.Draw(Game1.Instance.Content.Load<Texture2D>("Ball"), new Vector2(0, 0), Color.White);//draws ball.png at (0,0) 
                    for (int i = 1; i < entities.Count; i++)
                    {
                        Box temp_box = (Box)entities[i];
                        temp_box.Speed = 800.0f;//speed equals 800
                    }
                }
                //NIGHT LEVEL
                else if (score > 30)//if score greater than 30
                {
                    GraphicsDevice.Clear(Color.Blue);
                    spriteBatch.Draw(Game1.Instance.Content.Load<Texture2D>("Moon1"), new Vector2(0, 0), Color.White);//draws moon1.png at(0,0)
                    for (int i = 1; i < entities.Count; i++)
                    {
                        Box temp_box = (Box)entities[i];
                        temp_box.Speed = 400.0f;//speed equals 400
                    }
                }

                //DAY LEVEL
                else
                {
                    GraphicsDevice.Clear(Color.SkyBlue);
                    spriteBatch.Draw(Game1.Instance.Content.Load<Texture2D>("Sun"), new Vector2(0, 0), Color.White);//draws sun.png at(0,0)

                }
                for (int i = 0; i < entities.Count; i++)
                {
                    entities[i].Draw(gameTime);//draws entitie[i]
                }
                spriteBatch.DrawString(scorefont, ((int)score).ToString(), new Vector2(760, 10), Color.Black);//prints score to screen as string using tostring at (760,10) using font scorefont
                spriteBatch.End();
            }
            if (gameState == GameState.Exit)//if gamestate equals exit
            {
                GraphicsDevice.Clear(Color.AliceBlue);
                spriteBatch.Begin();

                spriteBatch.DrawString(title, "SCORE=", new Vector2(50, 150), Color.Black);//string "score=" written on screen at position (50,150) of font title
                spriteBatch.DrawString(title, ((int)score).ToString(), new Vector2(190, 150), Color.Black);//int score as string  written on screen using tostring method at position (760,10) of font title
                spriteBatch.DrawString(scorefont, "HIGH SCORE=", new Vector2(630, 10), Color.Black);//string "Highscore" written on screen at position (630,10) of font scorefont
                spriteBatch.DrawString(scorefont, ((int)highscore).ToString(), new Vector2(760, 10), Color.Black);//int high score as string  written on screen using tostring method at position (760,10) of font scorefont
                spriteBatch.DrawString(title, "GAME OVER!!", new Vector2(50, 100), Color.Red);//string "Game over" written on screen at position (50,100) of font title
                spriteBatch.DrawString(title, "Press Enter to Play Again", new Vector2(50, 300), Color.Black);//string "Press Enter to Play Again" written on screen at position (50,300) of font title
                spriteBatch.DrawString(title, "Press Exit to Quit", new Vector2(50, 400), Color.Black);//string "Press Exit to Quit" written on screen at position (50,400) of font title


                spriteBatch.End();
            }

            base.Draw(gameTime);
        }
    }
}
