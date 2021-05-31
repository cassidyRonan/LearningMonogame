using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Week3Lab1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        //Variables
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;

        string Message = "Message to Fade";
        byte alpha = 255;
        string timeMessage;

        bool isFading;
        int selectedItem = 0;
        KeyboardState previousKeyState;

        string[] messages = new string[] { "Item 1", "Item 2", "Item 3", "Item 4" };
        int[] spacingArray = new int[] { 0,0,0,0 };


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //Vector2 messageSize = font.MeasureString(messages[0]);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("font");
            
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            KeyboardState CurrentKeyState = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            int seconds = gameTime.TotalGameTime.Seconds;
            timeMessage = "Time elapsed in seconds" + seconds.ToString();

            if(alpha == 0)
            {
                isFading = false;
            }
            else if(alpha == 255)
            {
                isFading = true;
            }

            if (isFading)
            {
                alpha--;
            }
            else
            {
                alpha++;
            }

            if (previousKeyState.IsKeyDown(Keys.Up))
            {
                selectedItem++;
            }
            else if (previousKeyState.IsKeyDown(Keys.Down))
            {
                selectedItem--;
            }
            
            if(selectedItem >= 4)
            {
                selectedItem = 0;
            }
            else if(selectedItem < 0)
            {
                selectedItem = 3;
            }

            previousKeyState = CurrentKeyState;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);
            Color Messagecolor = new Color((byte)255, (byte)255, (byte)255, alpha);
            spriteBatch.DrawString(font, timeMessage , new Vector2(100, 50), Color.White);
            spriteBatch.DrawString(font, Message, new Vector2(100, 100), Messagecolor);

            //Array Draw
            Vector2 startPos = GraphicsDevice.Viewport.Bounds.Center.ToVector2();

            /*foreach (var item in messages)
            {
                spriteBatch.DrawString(font, item, startPos, Color.White);
                float textHeight = font.MeasureString(item).Y;
                startPos += new Vector2(0, textHeight + 10);
            }*/

            for (int i = 0; i < messages.Length; i++)
            {
                

                if(selectedItem == i)
                {
                    spriteBatch.DrawString(font, messages[i], startPos, Messagecolor);
                    float textHeight = font.MeasureString(messages[i]).Y;
                    startPos += new Vector2(0, textHeight + 10);
                }
                else
                {
                    spriteBatch.DrawString(font, messages[i], startPos, Color.White);
                    float textHeight = font.MeasureString(messages[i]).Y;
                    startPos += new Vector2(0, textHeight + 10);
                }
            }
            

            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
