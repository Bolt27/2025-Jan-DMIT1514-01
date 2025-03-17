using System.Collections;
using System.ComponentModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace lesson15_MosquitoAttack_Cannon;

public class CannonBall
{
    private Vector2 _position, _direction;
    private float _speed;
    private Rectangle _gameBoundingBox;
    private Texture2D _texture;

    private enum State { Flying, NotFlying}
    private State _state;
    internal Rectangle BoundingBox
    {
        get
        {
            return new Rectangle(_position.ToPoint(), new Point(_texture.Width, _texture.Height));
        }
    }
    internal void Initialize(Rectangle gameBoundingBox)
    {
        _gameBoundingBox = gameBoundingBox;
        _state = State.NotFlying;
    }

    internal void LoadContent(ContentManager content)
    {
        _texture = content.Load<Texture2D>("CannonBall");
    }
    internal void Update(GameTime gameTime)
    {
        switch(_state)
        {
            case State.Flying:
                _position += _direction * _speed * (float) gameTime.ElapsedGameTime.TotalSeconds;
                if(!BoundingBox.Intersects(_gameBoundingBox))
                {
                    //I am outside of the game play area, so reload
                    _state = State.NotFlying;
                }
                break;
            case State.NotFlying:
                break;
        }
    }
    internal void Draw(SpriteBatch spriteBatch)
    {
        switch(_state)
        {
            case State.Flying:
                spriteBatch.Draw(_texture, _position, Color.White);
                break;
            case State.NotFlying:
                break;
        }
    }
    internal void Shoot(Vector2 position, Vector2 direction, float speed)
    {
        if(_state != State.Flying)
        {
            _state = State.Flying;
            _position = position;
            //adjust the position so that we are centered upon the position parameter
            _position.X -= BoundingBox.Width / 2;
            _direction = direction;
            _speed = speed;
        }
    }
}