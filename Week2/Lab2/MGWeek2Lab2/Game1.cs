using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MGWeek2Lab2
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Textures
        Texture2D _downArrow;
        Texture2D _rightArrow;
        Texture2D _circleTransparent;
        Texture2D _magentaBox;
        Texture2D _yellowBox;

        //Flip
        bool directionLeft = false;

        //Rotation
        float rotate = 0; 

        
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

            _downArrow = Content.Load<Texture2D>("Down Arrow");
            _rightArrow = Content.Load<Texture2D>("Right Arrow");
            _magentaBox = Content.Load<Texture2D>("Magenta Box");
            _yellowBox = Content.Load<Texture2D>("Yellow Box");
            _circleTransparent= Content.Load<Texture2D>("see through circle");
            
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                directionLeft = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                directionLeft = false;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                rotate -= 0.1f;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.E))
            {
                rotate += 0.1f;
            }

            if(rotate > 360)
            {
                rotate = 0;
            }
            else if (rotate < 0)
            {
                rotate = 360;
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            //Slide Nine
            spriteBatch.Draw(_magentaBox, new Vector2((Window.ClientBounds.Width / 2) - (_magentaBox.Width / 2), (Window.ClientBounds.Height / 2) - (_magentaBox.Height / 2)), null, Color.Wheat, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            spriteBatch.Draw(_yellowBox, new Vector2((Window.ClientBounds.Width / 2) - (_yellowBox.Width / 2), (Window.ClientBounds.Height / 2) - (_yellowBox.Height / 2)),null, Color.White, 0, Vector2.Zero,1,SpriteEffects.None,1);
            spriteBatch.Draw(_circleTransparent, new Vector2((Window.ClientBounds.Width / 2) - (_circleTransparent.Width / 2) - 30, (Window.ClientBounds.Height / 2) - (_circleTransparent.Height / 2) - 30),null,Color.White,0,Vector2.Zero,1,SpriteEffects.None,0.5f);

            //Slide Ten
            //spriteBatch.Draw(_rightArrow, new Vector2((Window.ClientBounds.Width / 2) + 300, (Window.ClientBounds.Height / 2)), null, Color.White, 0, new Vector2((_downArrow.Width / 2), (_downArrow.Height / 2)),1, SpriteEffects.FlipHorizontally, 0);

            if (directionLeft)
            {
                spriteBatch.Draw(_rightArrow, new Vector2((Window.ClientBounds.Width / 2) + 300, (Window.ClientBounds.Height / 2)), null, Color.White, rotate, new Vector2((_downArrow.Width / 2), (_downArrow.Height / 2)), 1, SpriteEffects.FlipHorizontally, 0);
                spriteBatch.Draw(_downArrow, new Vector2((Window.ClientBounds.Width / 2) - 300, (Window.ClientBounds.Height / 2)), null, Color.White, 0, new Vector2((_downArrow.Width / 2), (_downArrow.Height / 2)), 1, SpriteEffects.FlipVertically, 0);
            }
            else if (!directionLeft)
            {
                spriteBatch.Draw(_rightArrow, new Vector2((Window.ClientBounds.Width / 2) + 300, (Window.ClientBounds.Height / 2)), null, Color.White, rotate, new Vector2((_downArrow.Width / 2), (_downArrow.Height / 2)), 1, SpriteEffects.None, 0);
                spriteBatch.Draw(_downArrow, new Vector2((Window.ClientBounds.Width / 2) - 300, (Window.ClientBounds.Height / 2)), null, Color.White, 0, new Vector2((_downArrow.Width / 2), (_downArrow.Height / 2)), 1, SpriteEffects.None, 0);
            }

            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
