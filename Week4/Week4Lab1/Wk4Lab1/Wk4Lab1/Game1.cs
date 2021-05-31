using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprites;

namespace Wk4Lab1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        SimpleSprite background;
        SimpleSprite character1;
        SimpleSprite character2;

        SpriteFont collisionFont;

        string collisionMessage = "";
        string characterSpeech = "";

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

            Texture2D _txBackground = Content.Load<Texture2D>(@"Textures/background");
            Texture2D _txBlueGhost = Content.Load<Texture2D>(@"Textures/blueghost");
            Texture2D _txPurpleGhost = Content.Load<Texture2D>(@"Textures/purpleghost");

            collisionFont = Content.Load<SpriteFont>(@"Font/collisionFont");

            background = new SimpleSprite(_txBackground, Vector2.Zero);
            character1 = new SimpleSprite(_txBlueGhost, new Vector2(30,30));
            character2 = new SimpleSprite(_txPurpleGhost, Vector2.Zero);

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
            float speed = 5.0f;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Vector2 previousPos = character1.Position;
            Vector2 previousPosTwo = character2.Position;

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

           

            //Character Two

            if (Keyboard.GetState().IsKeyDown(Keys.Left)) //&& character2.Position.X > 0
            {
                character2.Move(new Vector2(-1, 0) * speed);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right)) //&& character2.Position.X <= GraphicsDevice.Viewport.Bounds.Width - character2.Image.Width
            {
                character2.Move(new Vector2(1, 0) * speed);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up)) //&& character2.Position.Y > 0
            {
                character2.Move(new Vector2(0, -1) * speed);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down)) //&& character2.Position.Y <= GraphicsDevice.Viewport.Bounds.Height - character2.Image.Height
            {
                character2.Move(new Vector2(0, 1) * speed);
            }

            if (!GraphicsDevice.Viewport.Bounds.Contains(character1.BoundingRect))
            {
                character1.Move(previousPos - character1.Position);
            }
            if (!GraphicsDevice.Viewport.Bounds.Contains(character2.BoundingRect))
            {
                character2.Move(previousPosTwo - character2.Position);
            }


            if (character1.IsIntersecting(character2))
            {
                collisionMessage = "We are in collision";
            }
            else
            {
                collisionMessage = "We are not in collision";
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
           
            background.draw(spriteBatch);
            character1.draw(spriteBatch);
            character2.draw(spriteBatch);

            spriteBatch.DrawString(collisionFont, collisionMessage, new Vector2(150, 150), Color.White);
            character1.drawString(spriteBatch, collisionFont);
            character2.drawString(spriteBatch, collisionFont);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
