using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CameraNS
{
    class Camera
    {
        Vector2 _camPos = Vector2.Zero;
        Vector2 _worldBound;

        //Matrix
        private float _zoom;
        private Matrix _transform;
        private Vector2 _pos;
        private float _rotation;

        public float Rotation
        {
            get { return _rotation; }
            set { _rotation = value; }
        }

        public float Zoom
        {
            get { return _zoom; }
            set { _zoom = value; }
        }

        public Matrix get_transformation(GraphicsDevice graphicsDevice)
        {
            _transform = Matrix.CreateTranslation(new Vector3(-_camPos.X, -_camPos.Y, 0)) *
                                Matrix.CreateRotationZ(_rotation) *
                                Matrix.CreateScale(new Vector3(_zoom, _zoom, 1)) *
                                Matrix.CreateTranslation(new Vector3(graphicsDevice.Viewport.Width / 2,
                                graphicsDevice.Viewport.Height / 2, 0));

            return _transform;
        }


        public Matrix CurrentCameraTranslation { get
            {
                return Matrix.CreateTranslation(new Vector3(
                    -CamPos,
                    0));
            } }

        public Vector2 CamPos
        {
            get
            {
                return _camPos;
            }

            set
            {
                _camPos = value;
            }
        }

        public Camera(Vector2 startPos, Vector2 bound)
        {
            CamPos = startPos;
            _worldBound = bound;
        }

        public void Move(Vector2 delta, Viewport v)
        {
            CamPos += delta;
            CamPos = Vector2.Clamp(CamPos, Vector2.Zero, _worldBound - new Vector2(v.Width, v.Height));
        }

        public void Follow(Vector2 followPos, Viewport v)
        {
            _camPos = followPos - new Vector2(v.Width / 2, v.Height / 2);
            _camPos = Vector2.Clamp(_camPos, Vector2.Zero, _worldBound - new Vector2(v.Width, v.Height));
        }

    }
}
