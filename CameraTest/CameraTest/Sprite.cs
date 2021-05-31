using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//monogame classes are in these namepspaces
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprites
{
    public class Sprite
    {
        //variables
        public Texture2D Image;
        public Vector2 Position;
        public Rectangle Bounds;
        private Vector2 _worldBound;

        //properties... later

        //constructor
        public Sprite(Texture2D image, Vector2 position)
        {
            Image = image;
            Position = position;
            Bounds = new Rectangle((int)position.X, (int)position.Y, image.Width, image.Height);
        }

        //caller has a SpriteBatch ready and has already called Begin
        public void Draw(SpriteBatch sp)
        {
            sp.Draw(Image, Position, Color.White);
        }

        public void Draw(SpriteBatch sp, Rectangle rectangle)
        {
            sp.Draw(Image, rectangle ,Color.White);
        }

        public void DrawCentre(SpriteBatch sp)
        {
            sp.Draw(Image, Position,null,Color.White,0,new Vector2(Image.Width/2,Image.Height/2),1,SpriteEffects.None,1);
        }

        //overload. Same name, different arguments
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="sfont"></param>
        public void Draw(SpriteBatch sp, SpriteFont sfont)
        {
            Draw(sp);//call the other Draw method above
            sp.DrawString(sfont, Position.ToString(), Position, Color.White);
        }

        

        //move the sprite by the given amount
        public void Move(Vector2 delta)
        {
            Position += delta;
            Bounds.X = (int)Position.X;
            Bounds.Y = (int)Position.Y;
        }

        public void Move(Vector2 delta, Viewport v)
        {
            Position += delta;
            //Position = Vector2.Clamp(Position, Vector2.Zero, _worldBound - new Vector2(v.Width, v.Height));
            Bounds.X = (int)Position.X;
            Bounds.Y = (int)Position.Y;
            
        }

        //check for collision
        public void CheckCollision(Sprite other)
        {
            if (Bounds.Intersects(other.Bounds))
            {
                
            }
            else
            {
                
            }
        }
    }
}
