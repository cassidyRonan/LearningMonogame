using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using CameraNS;
using Sprites;

namespace CameraTest
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D TXbackground;
        Sprite SPbackground;

        Texture2D TXghost;
        Sprite SPghost;
        Vector2 V2ghost;

        Camera cam;
        Vector2 WorldBound = new Vector2(4000, 4000);
        Rectangle WorldRectangle;

        float speed = 5;

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
            V2ghost = new Vector2(0, 0);
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

            TXbackground = Content.Load<Texture2D>("background");
            SPbackground = new Sprite(TXbackground, Vector2.Zero);

            TXghost = Content.Load<Texture2D>("redghost");
            SPghost = new Sprite(TXghost, GraphicsDevice.Viewport.Bounds.Center.ToVector2());

            cam = new Camera(Vector2.Zero, WorldBound);
            WorldRectangle = new Rectangle(Point.Zero, WorldBound.ToPoint());
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

            #region Controls
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                cam.Move(new Vector2(-1, 0) * speed, GraphicsDevice.Viewport);
                SPghost.Move(new Vector2(-1, 0) * speeds);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                cam.Move(new Vector2(1, 0) * speed, GraphicsDevice.Viewport);
                SPghost.Move(new Vector2(1, 0) * speed);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                cam.Move(new Vector2(0, -1) * speed, GraphicsDevice.Viewport);
                SPghost.Move(new Vector2(0,-1) * speed);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                cam.Move(new Vector2(0, 1) * speed, GraphicsDevice.Viewport);
                SPghost.Move(new Vector2(0,1) * speed);
            }
            #endregion

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.BackToFront,
                                BlendState.AlphaBlend,
                                null, null, null, null, 
                                cam.CurrentCameraTranslation);

            SPbackground.Draw(spriteBatch,WorldRectangle);
            SPghost.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
