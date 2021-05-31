using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Week2Lab1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SoundEffect collisionNoise;

        // Variables for the Dot
        Texture2D dot;
        Color dotColor; //Used for transperancy
        Rectangle dotRect;
        int dotSize;

        // Variables for the Dot
        Texture2D dotTwo;
        Color dotTwoColor; //Used for transperancy
        Rectangle dotTwoRect;
        int dotTwoSize;

        // Variables for the Background 
        Texture2D background;
        Rectangle backgroundRect;

        // Variables to hold the display properties
        int displayWidth;
        int displayHeight;

        // Variables to hold the color change   [byte 0 - 255 (wraps around 255 + 1 => 0)]
        byte redComponent = 255;
        byte blueComponent = 0;
        byte greenComponent = 0;
        byte alphaComponent = 255;

        byte redTwoComponent = 0;
        byte blueTwoComponent = 255;
        byte greenTwoComponent = 0;
        byte alphaTwoComponent = 255;

        // Vars to draw message
        SpriteFont font;
        string message = "";
        string messageTwo = "";

        //Counter
        int counter = 0;
        bool soundPlayed = false;

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
            displayWidth = GraphicsDevice.Viewport.Width;
            displayHeight = GraphicsDevice.Viewport.Height;

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
            collisionNoise = Content.Load<SoundEffect>("2d");

            // TODO: use this.Content to load your game content here
            dot = Content.Load<Texture2D>("WhiteDot");
            dotColor = Color.White;
            dotSize = 40;

            dotTwo = Content.Load<Texture2D>("WhiteDot");
            dotTwoColor = Color.White;
            dotTwoSize = 40;

            dotRect = new Rectangle(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2, dotSize, dotSize);
            dotTwoRect = new Rectangle((graphics.GraphicsDevice.Viewport.Width / 2) - 40, graphics.GraphicsDevice.Viewport.Height / 2, dotTwoSize, dotTwoSize);

            background = Content.Load<Texture2D>("background");
            backgroundRect = new Rectangle(0, 0, displayWidth, displayHeight);
            font = Content.Load<SpriteFont>("MessageFont");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                this.Exit();
            }    
            if (GamePad.GetState(PlayerIndex.One).Buttons.X == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.B))
            {
                blueComponent++;
                blueTwoComponent++;
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.G))
            {
                greenComponent++;
                greenTwoComponent++;
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.B == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.R))
            {
                redComponent++;
                redTwoComponent++;
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.Y == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.T))
            {
                alphaComponent++;
                alphaTwoComponent++;
            }
               

            
            
            GamePadState gpState = GamePad.GetState(PlayerIndex.One);
            KeyboardState kbState = Keyboard.GetState();
            if (gpState.IsConnected)
            {
                if (gpState.ThumbSticks.Left.X != 0 ||
                    gpState.ThumbSticks.Left.Y != 0)
                {
                    if(dotRect.X + (int)(gpState.ThumbSticks.Left.X * 5) //scale up factor for speed - *5
                        < graphics.GraphicsDevice.Viewport.Width - dotRect.Width &&
                        dotRect.X + (int)(gpState.ThumbSticks.Left.X * 5) > 0)
                                dotRect.X += (int)(gpState.ThumbSticks.Left.X * 5);

                    if (dotRect.Y - (int)(gpState.ThumbSticks.Left.Y * 5)
                        < graphics.GraphicsDevice.Viewport.Height - dotRect.Height &&
                        dotRect.Y - (int)(gpState.ThumbSticks.Left.Y * 5) > 0)
                    
                                dotRect.Y -= (int)(gpState.ThumbSticks.Left.Y * 5);
                }
            }

            //ARROW KEYS
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                if (dotRect.X + (-1 * 5) < graphics.GraphicsDevice.Viewport.Width - dotRect.Width && dotRect.X + (-1 * 5) > 0)
                {
                    dotRect.X += (-1 * 5);
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                if (dotRect.X + (1 * 5) < graphics.GraphicsDevice.Viewport.Width - dotRect.Width && dotRect.X + (1 * 5) > 0)
                {
                    dotRect.X += (1 * 5);
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                if (dotRect.Y + (-1 * 5) < graphics.GraphicsDevice.Viewport.Height - dotRect.Height && dotRect.Y + (-1 * 5) > 0)
                {
                    dotRect.Y += (-1 * 5);
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                if (dotRect.Y + (1 * 5) < graphics.GraphicsDevice.Viewport.Height - dotRect.Height && dotRect.Y + (1 * 5) > 0)
                {
                    dotRect.Y += (1 * 5);
                }
            }

            //WASD
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                if (dotTwoRect.X + (-1 * 5) < graphics.GraphicsDevice.Viewport.Width - dotTwoRect.Width && dotTwoRect.X + (-1 * 5) > 0)
                    dotTwoRect.X += (-1 * 5);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                if (dotTwoRect.X + (1 * 5) < graphics.GraphicsDevice.Viewport.Width - dotTwoRect.Width && dotTwoRect.X + (1 * 5) > 0)
                    dotTwoRect.X += (1 * 5);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                if (dotTwoRect.Y + (-1 * 5) < graphics.GraphicsDevice.Viewport.Height - dotTwoRect.Height && dotTwoRect.Y + (-1 * 5) > 0)
                    dotTwoRect.Y += (-1 * 5);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                if (dotTwoRect.Y + (1 * 5) < graphics.GraphicsDevice.Viewport.Height - dotTwoRect.Height && dotTwoRect.Y + (1 * 5) > 0)
                    dotTwoRect.Y += (1 * 5);
            }

            //Collision
            if (dotRect.Intersects(dotTwoRect))
            {
                if (!soundPlayed)
                {
                    collisionNoise.Play();
                    soundPlayed = true;
                }
                
                messageTwo = "Collision";
                counter = 0;
            }
            else
            {
                counter++;
                

                if(counter > 60)
                {
                    messageTwo = "";
                    counter = 0;
                    soundPlayed = false;
                }
            }

            // TODO: Add your update logic here
            dotColor = new Color(redComponent, greenComponent, blueComponent, alphaComponent);
            dotTwoColor = new Color(redTwoComponent, greenTwoComponent, blueTwoComponent, alphaTwoComponent);

            message = "Red: " + redComponent.ToString() +
                            " Green: " + greenComponent.ToString() +
                            " Blue: " + blueComponent.ToString() +
                            " Alpha: " + alphaComponent.ToString();

            


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            spriteBatch.Draw(background, backgroundRect, Color.White);

            spriteBatch.Draw(dot, dotRect, dotColor);
            spriteBatch.Draw(dotTwo, dotTwoRect, dotTwoColor);

            // Work out Centre the text to draw
            int stringWidth = (int)font.MeasureString(message).X;
            int stringCollisionWidth = (int)font.MeasureString(messageTwo).X;

            spriteBatch.DrawString(font, message, new Vector2((displayWidth - stringWidth) / 2, 0), Color.White);
            spriteBatch.DrawString(font, messageTwo, new Vector2((displayWidth - stringCollisionWidth) / 2, (displayHeight - 50)), Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
