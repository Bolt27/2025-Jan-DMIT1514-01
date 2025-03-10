using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace lesson14_Pong_Final;

public class Paddle
{
    private const int _Width = 2, _Height = 18, _Speed = 200;

    private Texture2D _texture;
    private Vector2 _dimensions, _position, _direction;
    private float _speed;

    internal Vector2 Direction 
    { 
        get => _direction; 
        set => _direction = value; 
    }
    internal Rectangle BoundingBox
    {
        get
        {
            return new Rectangle(_position.ToPoint(), _dimensions.ToPoint());
        }
    }

    private int _gameScale;
    private Rectangle _playAreaBoundingBox;

    internal void Initialize(Vector2 initialPosition, int gameScale, Rectangle playAreaBoundingBox)
    {
        _position = initialPosition;
        _gameScale = gameScale;
        _playAreaBoundingBox = playAreaBoundingBox;

        _speed = _Speed * _gameScale;
        _dimensions = new Vector2(_Width * _gameScale, _Height * _gameScale);
    }
    internal void LoadContent(ContentManager content)
    {
        _texture = content.Load<Texture2D>("Paddle");
    }

    internal void Update(GameTime gameTime)
    {
        _position += _direction * _speed * (float) gameTime.ElapsedGameTime.TotalSeconds;

        if(_position.Y <= _playAreaBoundingBox.Top)
        {
            _position.Y = _playAreaBoundingBox.Top;
        }
        else if((_position.Y + _dimensions.Y) >= _playAreaBoundingBox.Bottom)
        {
            _position.Y = _playAreaBoundingBox.Bottom - _dimensions.Y;
        }
    }

    internal void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, _position, null, Color.DarkRed, 0, Vector2.Zero, _gameScale, SpriteEffects.None, 0);
        
    }
}