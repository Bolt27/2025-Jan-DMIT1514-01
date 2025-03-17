using System.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace lesson15_MosquitoAttack_Cannon;

public class Cannon 
{
    private const float _Speed = 250;
    private const int _NumCannonBalls = 10;
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

    CannonBall[] _cannonBalls;

    public Cannon()
    {
        _cannonBalls = new CannonBall[_NumCannonBalls];
        for(int c = 0; c < _NumCannonBalls; c++)
        {
            _cannonBalls[c] = new CannonBall();
        }
    }

    internal void Initialize(Vector2 initialPosition, Rectangle gameBoundingBox)
    {
        _position = initialPosition;
        _animationPlayer = new CelAnimationPlayer();
        _animationPlayer.Play(_animationSequence);
        _speed = _Speed;
        _gameBoundingBox = gameBoundingBox;
        foreach(CannonBall cBall in _cannonBalls)
        {
            cBall.Initialize(gameBoundingBox);
        }
    }
    internal void LoadContent(ContentManager content)
    {
        Texture2D cannonTexture = content.Load<Texture2D>("Cannon");
        _animationSequence = new CelAnimationSequence(cannonTexture, 40, 1 / 8.0f);
        foreach(CannonBall cBall in _cannonBalls)
        {
            cBall.LoadContent(content);
        }
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
        foreach(CannonBall cBall in _cannonBalls)
        {
            cBall.Update(gameTime);
        }
    }
    internal void Draw(SpriteBatch spriteBatch)
    {
        _animationPlayer.Draw(spriteBatch, _position, SpriteEffects.None);
        foreach(CannonBall cBall in _cannonBalls)
        {
            cBall.Draw(spriteBatch);
        }
    }

    internal void Shoot()
    {
        Vector2 positionOfCannonBall = new Vector2(BoundingBox.Center.X, BoundingBox.Top);
        
        //loop through the array until we find a cannonBall that is not flying
        //shoot it
        //only one return statement

        int c = 0;
        bool shot = false;
        while(c < _NumCannonBalls && !shot)
        {
            shot = _cannonBalls[c].Shoot(positionOfCannonBall, new Vector2(0, -1), 50);
            c++;
        }


        //_cannonBall.Shoot(positionOfCannonBall, new Vector2(0, -1), 50);
    }
}