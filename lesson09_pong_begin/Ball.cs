using System;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace lesson09_pong_begin;

public class Ball
{
    private const int _WidthAndHeight = 7;
    private const int _Speed = 175;
    private Texture2D _texture;
    private Vector2 _dimensions, _position, _direction;
    private float _speed;

    private int _gameScale;
    private Rectangle _playAreaBoundingBox;

    internal void Initialize(Vector2 initialPosition, Vector2 initialDirection, int gameScale, Rectangle playAreaBoundingBox)
    {
        _position = initialPosition;
        _direction = initialDirection;
        _gameScale = gameScale;
        _playAreaBoundingBox = playAreaBoundingBox;

        _speed = _Speed * _gameScale;
        _dimensions = new Vector2(_WidthAndHeight * _gameScale); // If only 1 value give, x and y will use the same value.

    }
    internal void LoadContent(ContentManager content)
    {
        _texture = content.Load<Texture2D>("Ball");
    }

    internal void Update(GameTime gameTime)
    {
        _position += _direction * _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

        if(_position.X <= _playAreaBoundingBox.Left || (_position.X + (_dimensions.X * _gameScale)) >= _playAreaBoundingBox.Right)
        {
            _direction.X *= -1;
        }

        if (_position.Y <= _playAreaBoundingBox.Top
        || (_position.Y + (_dimensions.Y * _gameScale)) >= _playAreaBoundingBox.Bottom)
        {
            _direction.Y *= -1;
        }
    }

    internal void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, _position, null, Color.White, 0, Vector2.Zero, _gameScale, SpriteEffects.None, 0);
    }
}