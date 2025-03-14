using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace lesson15_MosquitoAttack_Cannon;

public class Cannon 
{
    private const float _Speed = 250;
    private CelAnimationSequence _animationSequence;
    private CelAnimationPlayer _animationPlayer;
    private Vector2 _position, _direction;
    private float _speed; //in case we want to add variable speeds to the game

    public Vector2 Direction { get => _direction; set => _direction = value; }

    private Rectangle _gameBoundingBox;
    internal Rectangle BoundingBox
    {
        get => new Rectangle((int) _position.X, (int) _position.Y, _animationSequence.CelWidth, _animationSequence.CelHeight);
    }

    CannonBall _cannonBall;

    public Cannon()
    {
        _cannonBall = new CannonBall();
    }

    internal void Initialize(Vector2 initialPosition, Rectangle gameBoundingBox)
    {
        _position = initialPosition;
        _animationPlayer = new CelAnimationPlayer();
        _animationPlayer.Play(_animationSequence);
        _speed = _Speed;
        _gameBoundingBox = gameBoundingBox;
        _cannonBall.Initialize(new Vector2(25, 300), gameBoundingBox, 50, new Vector2(0, -1));
    }
    internal void LoadContent(ContentManager content)
    {
        Texture2D cannonTexture = content.Load<Texture2D>("Cannon");
        _animationSequence = new CelAnimationSequence(cannonTexture, 40, 1 / 8.0f);
        _cannonBall.LoadContent(content);
    }
    internal void Update(GameTime gameTime)
    {
        _position += Direction * _speed * (float) gameTime.ElapsedGameTime.TotalSeconds;

        if(BoundingBox.Left < _gameBoundingBox.Left)
        {
            _position.X = _gameBoundingBox.Left;
        }
        else if(BoundingBox.Right > _gameBoundingBox.Right)
        {
            _position.X = _gameBoundingBox.Right - BoundingBox.Width;
        }
        else if(!Direction.Equals(Vector2.Zero)) //if the cannon is moving
        {
            _animationPlayer.Update(gameTime);
        }
        _cannonBall.Update(gameTime);
    }
    internal void Draw(SpriteBatch spriteBatch)
    {
        _animationPlayer.Draw(spriteBatch, _position, SpriteEffects.None);
        _cannonBall.Draw(spriteBatch);
    }

    internal void Shoot()
    {
        Vector2 positionOfCannonBall = new Vector2(BoundingBox.Center.X, BoundingBox.Top);
        _cannonBall.Shoot(positionOfCannonBall, new Vector2(0, -1), 50);
    }
}