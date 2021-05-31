using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprites;

namespace Week4Lab1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Viewport mapViewport;
        Viewport originalViewPort;

        private Texture2D _txCharacter;
        private Texture2D _txBackGround;

        Vector2 CharacterPosition = new Vector2(10, 10);

        private Texture2D _txDot;

        SimpleSprite character1;
        SimpleSprite background;
        SimpleSprite dot;


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
            // Sample the original Viewport
            originalViewPort = GraphicsDevice.Viewport;
            GraphicsDevice.Viewport = originalViewPort;

          
            // Create the map viewport
            mapViewport.Bounds = new Rectangle(0, 0,
                originalViewPort.Bounds.Width / 10,
                originalViewPort.Bounds.Height / 10);
            mapViewport.X = 0;
            mapViewport.Y = 0;
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
            _txBackGround = Content.Load<Texture2D>(@"Textures\backgroundImage");
            _txCharacter = Content.Load<Texture2D>(@"Textures\body2");
            _txDot = Content.Load<Texture2D>(@"Textures\body");

            character1 = new SimpleSprite(_txCharacter, Vector2.Zero);
            background = new SimpleSprite(_txBackGround, Vector2.Zero);
            dot = new SimpleSprite(_txDot, Vector2.Zero);
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

            float speed = 5;
            CharacterPosition = character1.Position;

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                character1.Move(new Vector2(-1, 0) * speed);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                character1.Move(new Vector2(1, 0) * speed);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                character1.Move(new Vector2(0, -1) * speed);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                character1.Move(new Vector2(0, 1) * speed);
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
            GraphicsDevice.Viewport = originalViewPort;
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            //spriteBatch.Draw(_txBackGround, originalViewPort.Bounds, Color.White);
            //spriteBatch.Draw(_txCharacter, CharacterPosition, Color.White);
            background.drawVP(spriteBatch,originalViewPort.Bounds);
            character1.draw(spriteBatch);
            spriteBatch.End();

            GraphicsDevice.Viewport = mapViewport;

            //SpriteBatch
            spriteBatch.Begin();

            //spriteBatch.Draw(_txDot, CharacterPosition * 0.1f, null, Color.White, 0f, Vector2.Zero, 0.1f, SpriteEffects.None, 0);
            //spriteBatch.Draw(_txBackGround, mapViewport.Bounds, Color.White);

            dot.draw(spriteBatch);
            background.drawVP(spriteBatch, mapViewport.Bounds);
            //dot.draw(spriteBatch);

            spriteBatch.End();


            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
