using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace lesson15_MosquitoAttack_Cannon;

public abstract class Projectile
{
    protected Vector2 _position, _direction, _dimensions;
    protected float _speed;
    protected Rectangle _gameBoundingBox;
    protected enum State { Flying, NotFlying}
    protected State _state;
    internal Rectangle BoundingBox
    {
        get
        {
            return new Rectangle(_position.ToPoint(), _dimensions.ToPoint());
        }
    }
    //"virtual" means "my children may override this method, but it's not required"
    internal virtual void Initialize(Rectangle gameBoundingBox)
    {
        _gameBoundingBox = gameBoundingBox;
        _state = State.NotFlying;
    }

    internal abstract void LoadContent(ContentManager content);
    internal virtual void Update(GameTime gameTime)
    {
        switch(_state)
        {
            case State.Flying:
                _position += _direction * _speed * (float) gameTime.ElapsedGameTime.TotalSeconds;
                if(!BoundingBox.Intersects(_gameBoundingBox))
                {
                    _state = State.NotFlying;
                }
                break;
            case State.NotFlying:
                break;
        }
    }

    /*
        "abstract" forces the child class to define a method with this signature
        it allows:
            Projectile projectile01 = new FireBall();
            Projectile projectile02 = new CannonBall();
        but does not allow:
            Projectile projectile = new Projectile();

    */
    internal abstract void Draw(SpriteBatch spriteBatch);
    internal bool Shoot(Vector2 position, Vector2 direction, float speed)
    {
        bool shot = false;
        if(_state != State.Flying)
        {
            _state = State.Flying;
            _position = position;
            //adjust the position so that we are centered upon the position parameter
            _position.X -= BoundingBox.Width / 2;
            _direction = direction;
            _speed = speed;
            shot = true;
        }
        return shot;
    }
    internal bool ProcessCollision(Rectangle boundingBox)
    {
        bool didHit = false;
        if(_state == State.Flying && BoundingBox.Intersects(boundingBox))
        {
            didHit = true;
            _state = State.NotFlying;
        }
        return didHit;
    }
}