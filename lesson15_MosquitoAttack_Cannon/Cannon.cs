using System.Data;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace lesson15_MosquitoAttack_Cannon;

public class Cannon 
{
    private const float _Speed = 250;
    private const int _NumProjectiles = 5;
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

    Projectile[] _projectiles;

    public Cannon()
    {
        _projectiles = new Projectile[_NumProjectiles];
        
        _projectiles[0] = new CannonBall();
        _projectiles[1] = new FireBall();
        _projectiles[2] = new FireBall();
        _projectiles[3] = new CannonBall();
        _projectiles[4] = new FireBall();
        
    }

    internal void Initialize(Vector2 initialPosition, Rectangle gameBoundingBox)
    {
        _position = initialPosition;
        _animationPlayer = new CelAnimationPlayer();
        _animationPlayer.Play(_animationSequence);
        _speed = _Speed;
        _gameBoundingBox = gameBoundingBox;
        foreach(Projectile p in _projectiles)
        {
            p.Initialize(gameBoundingBox);
        }
    }
    internal void LoadContent(ContentManager content)
    {
        Texture2D cannonTexture = content.Load<Texture2D>("Cannon");
        _animationSequence = new CelAnimationSequence(cannonTexture, 40, 1 / 8.0f);
        foreach(Projectile p in _projectiles)
        {
            p.LoadContent(content);
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
        foreach(Projectile p in _projectiles)
        {
            p.Update(gameTime);
        }
    }
    internal void Draw(SpriteBatch spriteBatch)
    {
        _animationPlayer.Draw(spriteBatch, _position, SpriteEffects.None);
        foreach(Projectile p in _projectiles)
        {
            p.Draw(spriteBatch);
        }
    }

    internal void Shoot()
    {
        int c = 0;
        bool shot = false;
        while(c < _NumProjectiles && !shot)
        {
            Vector2 positionOfCannonBall = new Vector2(BoundingBox.Center.X, BoundingBox.Top);
            shot = _projectiles[c].Shoot(positionOfCannonBall , new Vector2(0, -1), 50);
            c++;
        }
    }
    internal bool ProcessCollision(Rectangle boundingBox)
    {
        bool hit = false;
        int c = 0;
        while(!hit && c < _projectiles.Length)
        {
            hit = _projectiles[c].ProcessCollision(boundingBox);
            c++;
        }

        return hit;
    }
}