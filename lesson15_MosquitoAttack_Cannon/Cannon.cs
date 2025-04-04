using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace lesson15_MosquitoAttack_Cannon;

public class Cannon : GameBot
{
    private const int _NumProjectiles = 5;
    public Vector2 Direction { get => _direction; set => _direction = value; }

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

    internal override void Initialize(Vector2 initialPosition, Rectangle gameBoundingBox, float speed)
    {
        base.Initialize(initialPosition, gameBoundingBox, speed);
        foreach(Projectile p in _projectiles)
        {
            p.Initialize(gameBoundingBox);
        }
    }
    internal override void LoadContent(ContentManager content)
    {
        Texture2D cannonTexture = content.Load<Texture2D>("Cannon");
        _animationSequenceAlive = new CelAnimationSequence(cannonTexture, 40, 1 / 8.0f);
        foreach(Projectile p in _projectiles)
        {
            p.LoadContent(content);
        }
    }
    internal override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        switch(_state)
        {
            case State.Alive:
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
                break;
            case State.Dying:
                break;
            case State.Dead:
                break;
        }
        foreach(Projectile p in _projectiles)
        {
            p.Update(gameTime);
        }
    }
    internal override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
        foreach(Projectile p in _projectiles)
        {
            p.Draw(spriteBatch);
        }
    }

    internal override void Shoot()
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