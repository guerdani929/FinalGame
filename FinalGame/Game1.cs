using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FinalGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private Texture2D background;
        private Texture2D character;
        private KeyboardState oldState;

        
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


            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            currentLevel = Screen.Intro;
            character = Content.Load<Texture2D>("PrisonnerB");


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
                

            if (kstate.IsKeyDown(Keys.Right))
                characterPosition.X += characterSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                

            if (characterPosition.X > graphics.PreferredBackBufferWidth - character.Width) 
                characterPosition.X = graphics.PreferredBackBufferWidth - character.Width;
            else if (characterPosition.X < character.Width)
                characterPosition.X = character.Width ;

            if (characterPosition.Y > graphics.PreferredBackBufferHeight - character.Height)
                characterPosition.Y = graphics.PreferredBackBufferHeight - character.Height;
            else if (characterPosition.Y < character.Height)
                characterPosition.Y = character.Height;

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


                spriteBatch.End();

            }



            base.Draw(gameTime);
        }
    }
}
