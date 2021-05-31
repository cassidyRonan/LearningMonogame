using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Management;
using CameraNS;
using Sprites;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Camera cam;

        Sprite ghost;
        Texture2D _txGhost;

        Sprite background;
        Texture2D _txBackground;

        Vector2 PlayerPos;
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
            cam = new Camera(new Vector2(GraphicsDevice.Viewport.Bounds.Width /2,GraphicsDevice.Viewport.Bounds.Height /2), new Vector2(2000,2000));
            PlayerPos = new Vector2(GraphicsDevice.Viewport.Bounds.Width / 2, GraphicsDevice.Viewport.Bounds.Height / 2);
            cam.Pos = new Vector2(0, 0);
            cam.Rotation = 0;
            cam.Zoom = 1;
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

            _txBackground = Content.Load<Texture2D>("Textures/gameboard");
            background = new Sprite(_txBackground, Vector2.Zero, Color.White);

            _txGhost = Content.Load<Texture2D>("Textures/blueghost");
            ghost = new Sprite(_txGhost, PlayerPos, Color.White);

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

            Vector2 previousGhostPos = ghost.Position;
            Vector2 previousCamPos = cam.CamPos;

            #region Controls
            if (Keyboard.GetState().IsKeyDown(Keys.W)) //&& ghost.Position.Y <= GraphicsDevice.Viewport.Bounds.Height - ghost.Image.Height)
            {
                ghost.Move(new Vector2(0, -1) * speed);
                cam.Move((new Vector2(0, -1) * speed), GraphicsDevice.Viewport);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S))// && ghost.Position.Y > 0)
            {
                ghost.Move(new Vector2(0, 1) * speed);
                cam.Move((new Vector2(0, 1) * speed), GraphicsDevice.Viewport);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))// && ghost.Position.X > 0)
            {
                ghost.Move(new Vector2(-1, 0) * speed);
                cam.Move((new Vector2(-1, 0) * speed), GraphicsDevice.Viewport);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D))// && ghost.Position.X <= GraphicsDevice.Viewport.Bounds.Width - ghost.Image.Width)
            {
                ghost.Move(new Vector2(1, 0) * speed);
                cam.Move((new Vector2(1, 0) * speed), GraphicsDevice.Viewport);
            }
            #endregion

            if (!GraphicsDevice.Viewport.Bounds.Contains(ghost.Bounds))
            {
                ghost.Move(previousGhostPos - ghost.Position);
                cam.Move(previousCamPos - cam.CamPos, GraphicsDevice.Viewport);
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
            spriteBatch.Begin(SpriteSortMode.BackToFront,BlendState.AlphaBlend,null, null, null, null, cam.get_transformation(GraphicsDevice));

            background.Draw(spriteBatch);
            ghost.Draw(spriteBatch);

            spriteBatch.End();
            
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
