using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace FinalGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private Texture2D background;
        private Texture2D character;
        private KeyboardState oldState;
        private Texture2D upwards;
        private Texture2D sideways;
        List<Rectangle> walls;

        
        Vector2 characterPosition;
        float characterSpeed;



        Screen currentLevel;

       



        enum Screen
        {
            Intro,
            Level1,
            Level2,
            End,
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            characterPosition = new Vector2(graphics.PreferredBackBufferWidth / 2,
            graphics.PreferredBackBufferHeight / 2);
            characterSpeed = 220f;
            walls = new List<Rectangle>();

            walls.Add( new Rectangle(0, 0, 25, 480));
            walls.Add( new Rectangle(0, 0, 665, 25));
            walls.Add( new Rectangle(775, 40, 25, 450));
            walls.Add( new Rectangle(165, 160, 25, 220));
            walls.Add(new Rectangle(165, 140, 285, 25));
            walls.Add(new Rectangle(620, 100, 25, 295));
            base.Initialize();

            


            
           

        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            currentLevel = Screen.Intro;
            character = Content.Load<Texture2D>("PrisonnerB");

            upwards = Content.Load<Texture2D>("UpWall");
            sideways = Content.Load<Texture2D>("SideWall");
            
            


        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            KeyboardState newState = Keyboard.GetState();  // get the newest state

            // handle the input
            if (oldState.IsKeyUp(Keys.Enter) && newState.IsKeyDown(Keys.Enter))
            {
                currentLevel = Screen.Level1;
            }

            oldState = newState;  // set the new state as the old state for next time

            var kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(Keys.Up))
                characterPosition.Y -= characterSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                
             




            if (kstate.IsKeyDown(Keys.Down))
                characterPosition.Y += characterSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                

            if (kstate.IsKeyDown(Keys.Left))
                characterPosition.X -= characterSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                //foreach (Rectangle wall in walls)
                //    if (characterPosition.Intersects(walls))
                //    {
                //        characterPosition.X = wall.Right;
                //    }




            if (kstate.IsKeyDown(Keys.Right))
                characterPosition.X += characterSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                


            if (characterPosition.X > graphics.PreferredBackBufferWidth - character.Width/3) 
                characterPosition.X = graphics.PreferredBackBufferWidth - character.Width/3;
            else if (characterPosition.X < character.Width/3)
                characterPosition.X = character.Width/3 ;

            if (characterPosition.Y > graphics.PreferredBackBufferHeight - character.Height/3)
                characterPosition.Y = graphics.PreferredBackBufferHeight - character.Height/3;
            else if (characterPosition.Y < character.Height/3)
                characterPosition.Y = character.Height/3;


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            if(currentLevel == Screen.Intro)
            {
                background = Content.Load<Texture2D>("TitleScreen");
            }
            else if (currentLevel == Screen.Level1)
            {
                background = Content.Load<Texture2D>("BaseBoard");
            }
            

            spriteBatch.Begin();

            spriteBatch.Draw(background, new Rectangle(0, 0, 800, 480), Color.White);
            




            spriteBatch.End();

            var kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(Keys.Up))
                
                character = Content.Load<Texture2D>("PrisonnerB");


            if (kstate.IsKeyDown(Keys.Down))
               
                character = Content.Load<Texture2D>("PrisonnerF");

            if (kstate.IsKeyDown(Keys.Left))
               
                character = Content.Load<Texture2D>("PrisonnerL");

            if (kstate.IsKeyDown(Keys.Right))
                
                character = Content.Load<Texture2D>("PrisonnerR");

            if (currentLevel == Screen.Level1)
            {

                spriteBatch.Begin();

                
                spriteBatch.Draw(character, characterPosition, Color.White);
                
                //spriteBatch.Draw(upwards, new Rectangle(0, 0, 25, 480), Color.White);
                //spriteBatch.Draw(sideways, new Rectangle(0, 0, 665, 25), Color.White);
                //spriteBatch.Draw(upwards, new Rectangle(775, 40, 25, 450), Color.White);

                ////Level Design
                //spriteBatch.Draw(upwards, new Rectangle(165, 160, 25, 220), Color.White);
                //spriteBatch.Draw(sideways, new Rectangle(165, 140, 285, 25), Color.White);
                //spriteBatch.Draw(upwards, new Rectangle(620, 100, 25, 295), Color.White);

                foreach (Rectangle wall in walls)
                {
                    if (wall.Width > wall.Height)
                        spriteBatch.Draw(sideways, wall, Color.White);
                    else
                        spriteBatch.Draw(upwards, wall, Color.White);


                }

                spriteBatch.End();

     
            }
            //Finish game!!!


            base.Draw(gameTime);
        }
    }
}
